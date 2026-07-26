# File Upload Solution - Persistent Storage Implementation

## Problem

The original file upload implementation saved files to the project's `UploadedFiles` directory:
```csharp
var targetFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", employeeCode);
```

When deploying to the server, this directory would be deleted because:
1. It's not part of the deployed application artifacts
2. It's typically in `.gitignore` and not committed to version control
3. New deployments replace the entire application directory

## Solution Overview

The solution implements **persistent file storage** outside the application directory structure using:

1. **Configuration-based storage path** - Read from `appsettings.json`
2. **File storage service** - Centralized file operations management
3. **Secure file handling** - Input validation, path sanitization, and security checks
4. **Multiple endpoints** - Upload, retrieve, delete, and download files

## Implementation Details

### 1. Configuration (`appsettings.json`)

```json
"FileStorage": {
  "UploadDirectory": "C:\\Uploads\\NZ.HRM",
  "MaxFileSizeBytes": 10485760,
  "AllowedExtensions": ".pdf,.doc,.docx,.jpg,.jpeg,.png,.xlsx,.xls,.txt,.zip,.ppt,.pptx",
  "OrganizeByEmployeeCode": true
}
```

**Key Points:**
- `UploadDirectory`: External persistent path (not in project folder)
- `MaxFileSizeBytes`: 10MB default limit
- `AllowedExtensions`: Whitelist of allowed file types
- `OrganizeByEmployeeCode`: Organize files in employee-code subdirectories

### 2. File Storage Service

Located at: `NZHRM\NZ.HRM.WebAPI\Services\FileStorageService.cs`

**Features:**
- **Validation**: File size, type, and upload restrictions
- **Sanitization**: Prevents path traversal and directory traversal attacks
- **Organization**: Files stored in `UploadDirectory/{employeeCode}/` structure
- **Metadata**: Tracks original filename, size, content-type, upload date
- **Download URLs**: Generates secure relative URLs for file access

**Key Methods:**
```csharp
Task<List<UploadedFileDto>> UploadFilesAsync(string employeeCode, List<IFormFile> files)
IEnumerable<UploadedFileDto> GetUploadedFiles(string employeeCode)
bool DeleteFile(string employeeCode, string fileName)
string GetFileUrl(string employeeCode, string fileName)
```

### 3. Updated Controller Endpoints

#### Upload Files
```
POST /api/employees/upload-files?employeeCode=EMP001
Content-Type: multipart/form-data

Response:
{
  "message": "Upload successful",
  "filesCount": 2,
  "files": [
	{
	  "originalFileName": "resume.pdf",
	  "storedFileName": "550e8400-e29b-41d4-a716-446655440000_resume.pdf",
	  "fileSizeBytes": 102400,
	  "contentType": "application/pdf",
	  "uploadedDate": "2025-05-15T10:30:00Z",
	  "fileUrl": "/api/employees/download-file?employeeCode=EMP001&fileName=550e8400..."
	}
  ]
}
```

#### View Files
```
GET /api/employees/view-files/EMP001

Response:
{
  "employeeCode": "EMP001",
  "totalFiles": 2,
  "files": [
	{
	  "originalFileName": "resume.pdf",
	  "storedFileName": "550e8400-e29b-41d4-a716-446655440000_resume.pdf",
	  "fileSizeBytes": 102400,
	  "contentType": "application/pdf",
	  "uploadedDate": "2025-05-15T10:30:00Z",
	  "fileUrl": "/api/employees/download-file?employeeCode=EMP001&fileName=..."
	}
  ]
}
```

#### Download File
```
GET /api/employees/download-file?employeeCode=EMP001&fileName=550e8400-e29b-41d4-a716-446655440000_resume.pdf

Response: File binary content with appropriate Content-Type header
```

#### Delete File
```
DELETE /api/employees/delete-file?employeeCode=EMP001&fileName=550e8400-e29b-41d4-a716-446655440000_resume.pdf

Response:
{
  "message": "File deleted successfully"
}
```

## Security Features

1. **Path Sanitization**
   - Removes invalid path characters
   - Prevents directory traversal attacks (../, .\, etc.)
   - Validates file paths against base directory

2. **File Validation**
   - File size limit enforcement
   - Extension whitelist validation
   - Null/empty file checks

3. **Access Control**
   - Files organized by employee code
   - Guid-prefixed filenames prevent guessing
   - Path validation prevents unauthorized access

4. **Logging**
   - All operations logged (upload, download, delete)
   - Error tracking for debugging

## Deployment Configuration

### For Development (Windows)
```json
"FileStorage": {
  "UploadDirectory": "C:\\Uploads\\NZ.HRM"
}
```

### For Production (Linux Server)
```json
"FileStorage": {
  "UploadDirectory": "/var/uploads/nz-hrm"
}
```

### Important: Create Persistent Directory
Ensure the upload directory exists and has appropriate permissions:

**Windows:**
```powershell
New-Item -ItemType Directory -Force -Path "C:\Uploads\NZ.HRM"
```

**Linux:**
```bash
sudo mkdir -p /var/uploads/nz-hrm
sudo chmod 755 /var/uploads/nz-hrm
sudo chown www-data:www-data /var/uploads/nz-hrm  # Adjust user as needed
```

## Migration from Old to New System

If you have existing files in the project's `UploadedFiles` directory, move them to the persistent location:

**Windows:**
```powershell
Copy-Item -Path "C:\path\to\project\UploadedFiles\*" -Destination "C:\Uploads\NZ.HRM\" -Recurse -Force
```

**Linux:**
```bash
sudo cp -r /path/to/project/UploadedFiles/* /var/uploads/nz-hrm/
```

## Benefits

✅ **Persistent Storage**: Files survive application deployments  
✅ **Scalable**: Can use network shares or external storage  
✅ **Secure**: Input validation and path sanitization  
✅ **Organized**: Files grouped by employee code  
✅ **Maintainable**: Centralized file service for future enhancements  
✅ **Configurable**: Easy to change storage location per environment  

## Troubleshooting

### Issue: "Upload directory does not exist"
**Solution**: Manually create the directory configured in `appsettings.json`

### Issue: "Access denied when uploading"
**Solution**: Check directory permissions. The IIS application pool user needs read/write/modify permissions

### Issue: Files disappear after deployment
**Solution**: Verify the `UploadDirectory` in `appsettings.json` is set to a path outside the project directory

### Issue: "File type not allowed"
**Solution**: Add the file extension to `AllowedExtensions` in `appsettings.json`

## Configuration Changes Summary

| File | Change |
|------|--------|
| `appsettings.json` | Added `FileStorage` configuration section |
| `Program.cs` | Registered `IFileStorageService` and `FileStorageConfiguration` |
| `EmployeesController.cs` | Updated endpoints to use new file service |
| `FileStorageService.cs` | New service for file operations |
| `FileStorageConfiguration.cs` | New configuration class |

## Next Steps

1. Update `appsettings.json` for your server environment
2. Create the upload directory on your server
3. Grant appropriate permissions to the directory
4. Test file upload/download functionality
5. (Optional) Migrate existing files from the old location
