using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Employees.Queries.GetEmployeeConfirmationDate;
using NZ.HRM.Application.Employees.Queries.GetEmployeeDetail;
using NZ.HRM.Application.Employees.Queries.GetEmployeesByStatus;
using NZ.HRM.Application.Employees.Queries.SearchEmployees;
using NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.WebAPI.Services;
using NZ.HRM.WebAPI.Configuration;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeCommandHandler _createCompleteEmployeeHandler;
    private readonly CompleteEmployeeQueryHandler _getCompleteEmployeeHandler;
    private readonly EmployeeQueryHandler _employeeQueryHandler;
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(
        EmployeeCommandHandler createCompleteEmployeeHandler,
        CompleteEmployeeQueryHandler getCompleteEmployeeHandler,
        EmployeeQueryHandler employeeQueryHandler,
        IFileStorageService fileStorageService,
        ILogger<EmployeesController> logger)
    {
        _createCompleteEmployeeHandler = createCompleteEmployeeHandler;
        _getCompleteEmployeeHandler = getCompleteEmployeeHandler;
        _employeeQueryHandler = employeeQueryHandler;
        _fileStorageService = fileStorageService;
        _logger = logger;
    }

    [HttpGet("employee-detail/{employeeId}")]
    [ProducesResponseType(typeof(EmployeeDetailForIT), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeDetailForIT(string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            return BadRequest(new { message = "employeeId is required" });

        var query = new Application.Employees.Queries.GetEmployeeDetailForIT.GetEmployeeDetailForITQuery { EmployeeId = employeeId };
        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);

        if (result == null)
            return NotFound(new { message = $"Employee with ID {employeeId} not found" });

        return Ok(result);
    }

    [HttpPost("candidate-entry")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployeeRecruitment([FromBody] CreateCandidateEntryCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var employeeId = await _createCompleteEmployeeHandler.Handle(command, cancellationToken: default);
            return CreatedAtAction(
                nameof(CreateEmployeeRecruitment),
                new { id = employeeId },
                new { id = employeeId, message = "Employee created successfully with personal information" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the employee", details = ex.Message });
        }
    }


    [HttpPut("candidate-entry/{employeeId}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmployeeRecruitment(string employeeId, [FromBody] UpdateCandidateEntryCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _createCompleteEmployeeHandler.Handle(employeeId, command, cancellationToken: default);
            return CreatedAtAction(
                nameof(UpdateEmployeeRecruitment),
                new { id = employeeId },
                new { id = employeeId, message = "Employee updated successfully with personal information" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the employee", details = ex.Message });
        }
    }

    /// <summary>
    /// Get complete employee information (master + personal)
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeDetail(string id)
    {
        var query = new GetEmployeeDetailQuery { EmployeeId = id };
        var employee = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);

        if (employee == null)
            return NotFound(new { message = $"Employee with ID {id} not found" });

        return Ok(employee);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(List<EmployeeDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchEmployees([FromQuery] string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return BadRequest(new { message = "searchText is required" });

        var query = new SearchEmployeesQuery { SearchText = searchText };
        var employees = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);

        return Ok(employees);
    }

    [HttpGet("confirmation-date")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetConfirmationDate([FromQuery] string employeeId, [FromQuery] DateTime joiningDate)
    {
        var query = new GetEmployeeConfirmationDateQuery
        {
            EmployeeId = employeeId,
            JoiningDate = joiningDate
        };

        var confirmationDate = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);
        if (confirmationDate == null)
            return NotFound(new { message = $"Employee with ID {employeeId} not found" });

        return Ok(new { confirmationDate = confirmationDate.Value.ToString("yyyy-MM-dd") });
    }

    [HttpGet("by-status")]
    [ProducesResponseType(typeof(List<EmployeeByStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeesByStatus([FromQuery] string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return BadRequest(new { message = "status is required" });

        var query = new GetEmployeesByStatusQuery
        {
            Status = status
        };

        var employees = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);
        return Ok(employees);
    }

    [HttpPost("hr-executive-entry")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployeeHRExecutive([FromForm] CreateEmployeeHRExecutiveCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var certificate = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "educationCertificate", StringComparison.OrdinalIgnoreCase));
            var nationalId = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "nationalId", StringComparison.OrdinalIgnoreCase));
            var policeClearance = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "policeClearance", StringComparison.OrdinalIgnoreCase));
            var experienceCertificate = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "experienceCertificate", StringComparison.OrdinalIgnoreCase));
            var passportPhoto = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "passportPhoto", StringComparison.OrdinalIgnoreCase));
            var chairmanCertificate = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "chairmanCertificate", StringComparison.OrdinalIgnoreCase));
            var signature = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "signature", StringComparison.OrdinalIgnoreCase));
            command.Documents = new List<EmployeeDocumentDto>();
            if (certificate != null && certificate.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { certificate }, Utility.Enum.DocumentType.EducationCertificate);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.EducationCertificate,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (policeClearance != null && policeClearance.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { policeClearance }, Utility.Enum.DocumentType.PoliceClearance);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.PoliceClearance,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (policeClearance != null && policeClearance.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { policeClearance }, Utility.Enum.DocumentType.PoliceClearance);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.PoliceClearance,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (experienceCertificate != null && experienceCertificate.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { experienceCertificate }, Utility.Enum.DocumentType.ExperienceCertificate);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.ExperienceCertificate,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (passportPhoto != null && passportPhoto.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { passportPhoto }, Utility.Enum.DocumentType.PassportPhoto);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.PassportPhoto,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if(chairmanCertificate != null && chairmanCertificate.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { chairmanCertificate }, Utility.Enum.DocumentType.ChairmanCertificate);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.ChairmanCertificate,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (signature != null && signature.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { signature }, Utility.Enum.DocumentType.Signature);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.Signature,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }
            if(nationalId != null && nationalId.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { nationalId }, Utility.Enum.DocumentType.NID);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.NID,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            var employeeId = await _createCompleteEmployeeHandler.Handle(command, cancellationToken: default);
            return CreatedAtAction(
                nameof(CreateEmployeeHRExecutive),
                new { id = employeeId },
                new { id = employeeId, message = "Employee created successfully with personal information" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the employee", details = ex.Message });
        }
    }

    [HttpPost("biometric-capture")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CaptureBiometricAndPhoto([FromBody] CreateBiometricCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var employeeId = await _createCompleteEmployeeHandler.Handle(command, cancellationToken: default);
            return CreatedAtAction(
                nameof(CaptureBiometricAndPhoto),
                new { id = employeeId },
                new { id = employeeId, message = "Employee created successfully with personal information" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the employee", details = ex.Message });
        }
    }

    [HttpPost("directors-review")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DirectorsReview([FromBody] List<CreateDirectorReviewCommand> commands)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var employeeId = await _createCompleteEmployeeHandler.Handle(commands, cancellationToken: default);
            return CreatedAtAction(
                nameof(DirectorsReview),
                new { id = employeeId },
                new { id = employeeId, message = "Employee created successfully with personal information" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the employee", details = ex.Message });
        }
    }

    [HttpPost("it-activation")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ITActivation([FromForm] CreateITActivationCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var appointmentLetter = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "appointmentLetter", StringComparison.OrdinalIgnoreCase));
            var joiningLetter = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "joiningLetter", StringComparison.OrdinalIgnoreCase));
            var medicalReport = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "medicalReport", StringComparison.OrdinalIgnoreCase));
            var idCardBangla = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "idCardBangla", StringComparison.OrdinalIgnoreCase));
            var idCardEnglish = Request.Form.Files.FirstOrDefault(f =>
                string.Equals(f.Name, "idCardEnglish", StringComparison.OrdinalIgnoreCase));
            command.Documents = new List<EmployeeDocumentDto>();
            if (appointmentLetter != null && appointmentLetter.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { appointmentLetter}, Utility.Enum.DocumentType.AppointmentLetter);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.AppointmentLetter,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (joiningLetter != null && joiningLetter.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { joiningLetter }, Utility.Enum.DocumentType.JoiningLetter);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.JoiningLetter,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (medicalReport != null && medicalReport.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { medicalReport }, Utility.Enum.DocumentType.MedicalReport);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.MedicalReport,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (idCardBangla != null && idCardBangla.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { idCardBangla }, Utility.Enum.DocumentType.IDCardBangla);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.IDCardBangla,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }

            if (idCardEnglish != null && idCardEnglish.Length > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadFilesAsync(command.EmployeeCode, new List<IFormFile>() { idCardEnglish }, Utility.Enum.DocumentType.IDCardEnglish);
                command.Documents.AddRange(uploadedFiles.Select(f => new EmployeeDocumentDto
                {
                    DocumentType = Utility.Enum.DocumentType.IDCardEnglish,
                    FilePath = f.FilePath,
                    EmployeeId = command.EmployeeId,
                    FileName = f.FileName,
                }));
            }
            var employeeId = await _createCompleteEmployeeHandler.Handle(command, cancellationToken: default);
            return CreatedAtAction(
                nameof(ITActivation),
                new { id = employeeId },
                new { id = employeeId, message = "Employee created successfully with personal information" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the employee", details = ex.Message });
        }
    }

    [HttpPost("upload-files")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadFile(string employeeCode, List<IFormFile> files)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(employeeCode))
            return BadRequest(new { message = "employeeCode is required" });

        if (files == null || files.Count == 0)
            return BadRequest(new { message = "No files uploaded or files are empty" });

        try
        {
            // Upload files using the persistent file storage service
            var uploadedFiles = await _fileStorageService.UploadFilesAsync(employeeCode, files, Utility.Enum.DocumentType.Photo);

            _logger.LogInformation($"Successfully uploaded {uploadedFiles.Count} files for employee {employeeCode}");

            return Ok(new 
            { 
                message = "Upload successful", 
                filesCount = uploadedFiles.Count,
                files = uploadedFiles 
            });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning($"Validation error during file upload for employee {employeeCode}: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning($"File validation error during upload for employee {employeeCode}: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error uploading files for employee {employeeCode}: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error during file upload", details = ex.Message });
        }
    }

    [HttpGet("view-files/{employee-code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetUploadedFiles(string employeeCode)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
            return BadRequest(new { message = "employeeCode is required" });

        try
        {
            var files = _fileStorageService.GetUploadedFiles(employeeCode).ToList();

            if (!files.Any())
                return NotFound(new { message = $"No files found for employee {employeeCode}" });

            return Ok(new
            {
                employeeCode,
                totalFiles = files.Count,
                files
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving files for employee {employeeCode}: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error while retrieving files", details = ex.Message });
        }
    }

    [HttpDelete("delete-file")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteFile(string employeeCode, string fileName)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
            return BadRequest(new { message = "employeeCode is required" });

        if (string.IsNullOrWhiteSpace(fileName))
            return BadRequest(new { message = "fileName is required" });

        try
        {
            var deleted = _fileStorageService.DeleteFile(employeeCode, fileName);

            if (!deleted)
                return NotFound(new { message = $"File {fileName} not found for employee {employeeCode}" });

            _logger.LogInformation($"File {fileName} deleted for employee {employeeCode}");
            return Ok(new { message = "File deleted successfully" });
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning($"Unauthorized file deletion attempt: {ex.Message}");
            return BadRequest(new { message = "Invalid file path" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting file {fileName} for employee {employeeCode}: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error while deleting file", details = ex.Message });
        }
    }

    [HttpGet("download-file")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DownloadFile(string employeeCode, string fileName)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
            return BadRequest(new { message = "employeeCode is required" });

        if (string.IsNullOrWhiteSpace(fileName))
            return BadRequest(new { message = "fileName is required" });

        try
        {
            // Sanitize inputs to prevent directory traversal
            var sanitizedEmployeeCode = new string(employeeCode.Where(c => !char.IsControl(c)).ToArray());
            var sanitizedFileName = new string(fileName.Where(c => !char.IsControl(c)).ToArray());

            var fileStorageConfig = new FileStorageConfiguration();
            if (HttpContext.RequestServices.GetService(typeof(FileStorageConfiguration)) is FileStorageConfiguration config)
                fileStorageConfig = config;

            var filePath = Path.Combine(fileStorageConfig.UploadDirectory, sanitizedEmployeeCode, sanitizedFileName);
            var fullPath = Path.GetFullPath(filePath);
            var basePath = Path.GetFullPath(fileStorageConfig.UploadDirectory);

            // Prevent directory traversal attacks
            if (!fullPath.StartsWith(basePath))
                return BadRequest(new { message = "Invalid file path" });

            if (!System.IO.File.Exists(fullPath))
                return NotFound(new { message = $"File not found" });

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            var contentType = GetContentType(Path.GetExtension(fullPath));

            _logger.LogInformation($"File {fileName} downloaded for employee {employeeCode}");

            return File(fileBytes, contentType, Path.GetFileName(fullPath));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error downloading file {fileName} for employee {employeeCode}: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error while downloading file", details = ex.Message });
        }
    }

    /// <summary>
    /// Gets content type based on file extension
    /// </summary>
    private static string GetContentType(string extension)
    {
        var provider = new FileExtensionContentTypeProvider();
        return provider.TryGetContentType(extension, out var contentType) ? contentType : "application/octet-stream";
    }

    [HttpGet("uploaded-documents/{employeeId}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUploadedDocumentsByEmployee(string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            return BadRequest(new { message = "employeeId is required" });
        var query = new Application.Employees.Queries.GetEmployeeDocuments.GetEmployeeDocumentsQuery { EmployeeId = employeeId };
        var documents = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);
        if (documents == null || !documents.Any())
            return NotFound(new { message = "No documents found for this employee." });

        return Ok(new
        {
            employeeId,
            totalFiles = documents.Count,
            files = documents
        });
    }

    /// <summary>
    /// Return an image file given a full file system path.
    /// Example: GET /api/EmployeeMasters/image?path=C:\images\photo.jpg
    /// </summary>
    [HttpGet("image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImage([FromQuery] string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            return BadRequest(new { message = "Employee ID is required" });

        var photoDocument = await _getCompleteEmployeeHandler.Handle(employeeId, cancellationToken: default);

        if (photoDocument == null)
            return NotFound(new { message = $"Employee with ID {employeeId} not found" });

        string path = photoDocument.FilePath ?? string.Empty;
        if (!System.IO.File.Exists(path))
            return NotFound(new { message = $"File not found: {path}" });

        try
        {       
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
                contentType = "application/octet-stream";

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, contentType);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Return an image file given a full file system path.
    /// Example: GET /api/EmployeeMasters/image-by-path?path=C:\images\photo.jpg
    /// </summary>
    [HttpGet("image-by-path")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImageByPath([FromQuery] string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return BadRequest(new { message = "File path is required" });

        if (!System.IO.File.Exists(path))
            return NotFound(new { message = $"File not found: {path}" });

        try
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
                contentType = "application/octet-stream";

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, contentType);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
