using Microsoft.AspNetCore.Http.HttpResults;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.WebAPI.Configuration;
using System.Text.RegularExpressions;

namespace NZ.HRM.WebAPI.Services;

/// <summary>
/// Interface for file storage operations
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Uploads files for an employee
    /// </summary>
    /// <param name="employeeCode">Employee code to organize files</param>
    /// <param name="files">Files to upload</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of uploaded file information (filename, filepath, url)</returns>
    Task<List<EmployeeDocumentDto>> UploadFilesAsync(string employeeCode, List<IFormFile> files, Utility.Enum.DocumentType documentType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all uploaded files for an employee
    /// </summary>
    /// <param name="employeeCode">Employee code</param>
    /// <returns>List of file information</returns>
    IEnumerable<object> GetUploadedFiles(string employeeCode);

    /// <summary>
    /// Deletes a specific file for an employee
    /// </summary>
    /// <param name="employeeCode">Employee code</param>
    /// <param name="fileName">File name to delete</param>
    /// <returns>True if deleted successfully</returns>
    bool DeleteFile(string employeeCode, string fileName);

    /// <summary>
    /// Gets the download URL for a file
    /// </summary>
    /// <param name="employeeCode">Employee code</param>
    /// <param name="fileName">File name</param>
    /// <returns>Relative URL to download the file</returns>
    string GetFileUrl(string employeeCode, string fileName);
}

/// <summary>
/// DTO for uploaded file information
/// </summary>
public class UploadedFileDto
{
    public string OriginalFileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public DateTime UploadedDate { get; set; }
    public string FileUrl { get; set; } = string.Empty;
}

/// <summary>
/// Implementation of file storage service using persistent directory
/// </summary>
public class FileStorageService : IFileStorageService
{
    private readonly FileStorageConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<FileStorageService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileStorageService(
        FileStorageConfiguration configuration,
        IWebHostEnvironment environment,
        ILogger<FileStorageService> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _environment = environment;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Uploads files for an employee to persistent storage
    /// </summary>
    public async Task<List<EmployeeDocumentDto>> UploadFilesAsync(string employeeCode, List<IFormFile> files, Utility.Enum.DocumentType documentType, CancellationToken cancellationToken = default)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(employeeCode))
            throw new ArgumentException("Employee code is required", nameof(employeeCode));

        if (files == null || files.Count == 0)
            throw new ArgumentException("At least one file is required", nameof(files));

        // Sanitize employee code for use in path
        var sanitizedEmployeeCode = SanitizeFileName(employeeCode);
        var employeeUploadDir = _configuration.OrganizeByEmployeeCode
            ? Path.Combine(_configuration.UploadDirectory, sanitizedEmployeeCode)
            : _configuration.UploadDirectory;

        // Ensure directory exists
        Directory.CreateDirectory(employeeUploadDir);

        var uploadedFiles = new List<EmployeeDocumentDto>();

        foreach (var file in files)
        {
            // Validate file
            ValidateFile(file);
            var extension = Path.GetExtension(file.FileName);
            var storedFileName = $"{employeeCode}_{documentType.ToString()}{extension}";
            var filePath = Path.Combine(employeeUploadDir, storedFileName);

            try
            {
                // Save file to persistent location
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream, cancellationToken);
                }

                var fileInfo = new FileInfo(filePath);
                var uploadedFile = new EmployeeDocumentDto
                {
                    DocumentType = documentType,
                    FilePath = filePath,
                    //EmployeeId = command.EmployeeId,
                    FileName = storedFileName,
                };

                uploadedFiles.Add(uploadedFile);
                _logger.LogInformation($"File uploaded successfully: {storedFileName} for employee {employeeCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading file {file.FileName}: {ex.Message}");

                // Clean up partial uploads
                if (File.Exists(filePath))
                    File.Delete(filePath);

                throw;
            }
        }

        return uploadedFiles;
    }

    /// <summary>
    /// Gets all uploaded files for an employee
    /// </summary>
    public IEnumerable<object> GetUploadedFiles(string employeeCode)
    {
        var sanitizedEmployeeCode = SanitizeFileName(employeeCode);
        var employeeUploadDir = Path.Combine(_configuration.UploadDirectory, sanitizedEmployeeCode);

        if (!Directory.Exists(employeeUploadDir))
            return Enumerable.Empty<object>();

        var files = new List<object>();

        foreach (var filePath in Directory.GetFiles(employeeUploadDir))
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                var fileName = fileInfo.Name;

                files.Add(new
                {
                    filePath = fileInfo.FullName,
                    fileName = fileInfo.Name,
                    fileExtension = fileInfo.Extension,
                    fileSize = fileInfo.Length,
                    createdOn = fileInfo.CreationTime,
                    lastModifiedOn = fileInfo.LastWriteTime
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading file info: {ex.Message}");
            }
        }

        return files;
    }

    /// <summary>
    /// Deletes a specific file for an employee
    /// </summary>
    public bool DeleteFile(string employeeCode, string fileName)
    {
        try
        {
            var sanitizedEmployeeCode = SanitizeFileName(employeeCode);
            var sanitizedFileName = SanitizeFileName(fileName);
            var filePath = Path.Combine(_configuration.UploadDirectory, sanitizedEmployeeCode, sanitizedFileName);

            // Validate path to prevent directory traversal attacks
            var fullPath = Path.GetFullPath(filePath);
            var basePath = Path.GetFullPath(_configuration.UploadDirectory);

            if (!fullPath.StartsWith(basePath))
                throw new UnauthorizedAccessException("Invalid file path");

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                _logger.LogInformation($"File deleted: {fileName} for employee {employeeCode}");
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting file: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Gets the download URL for a file
    /// </summary>
    public string GetFileUrl(string employeeCode, string fileName)
    {
        var sanitizedEmployeeCode = SanitizeFileName(employeeCode);
        var sanitizedFileName = SanitizeFileName(fileName);
        return $"/api/employees/download-file?employeeCode={Uri.EscapeDataString(sanitizedEmployeeCode)}&fileName={Uri.EscapeDataString(sanitizedFileName)}";
    }

    /// <summary>
    /// Validates file before upload
    /// </summary>
    private void ValidateFile(IFormFile file)
    {
        if (file.Length > _configuration.MaxFileSizeBytes)
            throw new InvalidOperationException($"File size exceeds maximum allowed size of {_configuration.MaxFileSizeBytes / (1024 * 1024)}MB");

        var extension = Path.GetExtension(file.FileName).ToLower();
        var allowedExtensions = _configuration.AllowedExtensions.Split(',').Select(e => e.Trim()).ToList();

        if (!allowedExtensions.Contains(extension))
            throw new InvalidOperationException($"File type {extension} is not allowed");
    }

    /// <summary>
    /// Sanitizes a file name to prevent directory traversal attacks
    /// </summary>
    private static string SanitizeFileName(string fileName)
    {
        // Remove invalid characters
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = string.Concat(fileName.Split(invalidChars));

        // Remove path traversal characters
        sanitized = Regex.Replace(sanitized, @"[\\\/\.\.\:*?""<>|]", "_");

        // Limit length
        return sanitized.Length > 255 ? sanitized.Substring(0, 255) : sanitized;
    }

    /// <summary>
    /// Extracts original file name from stored file name (removes GUID prefix)
    /// </summary>
    private static string ExtractOriginalFileName(string storedFileName)
    {
        var parts = storedFileName.Split('_', 2);
        return parts.Length > 1 ? parts[1] : storedFileName;
    }

    /// <summary>
    /// Gets content type based on file extension
    /// </summary>
    private static string GetContentType(string extension)
    {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        return provider.TryGetContentType(extension, out var contentType) ? contentType : "application/octet-stream";
    }
}
