namespace NZ.HRM.Domain.Configuration;

/// <summary>
/// Configuration for file storage settings
/// </summary>
public class FileStorageConfiguration
{
    /// <summary>
    /// Gets or sets the base upload directory path
    /// </summary>
    public string UploadDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum file size in bytes (default: 10MB)
    /// </summary>
    public long MaxFileSizeBytes { get; set; } = 10 * 1024 * 1024;

    /// <summary>
    /// Gets or sets allowed file extensions (comma-separated)
    /// </summary>
    public string AllowedExtensions { get; set; } = ".pdf,.doc,.docx,.jpg,.jpeg,.png,.xlsx,.xls,.txt,.zip";

    /// <summary>
    /// Gets or sets whether to create subdirectories by employee code
    /// </summary>
    public bool OrganizeByEmployeeCode { get; set; } = true;
}
