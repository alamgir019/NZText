# Fingerprint Device Integration API Documentation

## Overview

The Fingerprint Device Integration API provides endpoints to retrieve employee codes from a fingerprint device using the device ID. The API communicates with an external fingerprint device endpoint and includes retry logic, timeout handling, and comprehensive error management.

## Configuration

### appsettings.json

Add the following configuration to your `appsettings.json` file:

```json
"FingerprintDevice": {
  "BaseUrl": "http://175.29.147.115:8000/virdi/finger-print.php",
  "TimeoutSeconds": 30,
  "RetryAttempts": 3,
  "RetryDelayMs": 500
}
```

### Configuration Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `BaseUrl` | string | - | **Required**. Base URL of the fingerprint device API |
| `TimeoutSeconds` | int | 30 | HTTP request timeout in seconds |
| `RetryAttempts` | int | 3 | Number of retry attempts for failed requests |
| `RetryDelayMs` | int | 500 | Delay between retry attempts in milliseconds (exponential backoff) |

## API Endpoints

### 1. Get Employee Code (Simple)

Retrieves the employee code from the fingerprint device.

**Endpoint:**
```
GET /api/fingerprint/get-employee-code?deviceId={deviceId}
```

**Parameters:**
- `deviceId` (query, required): Device identifier (e.g., "device2")

**Request Example:**
```bash
curl -X GET "http://localhost:5000/api/fingerprint/get-employee-code?deviceId=device2" \
  -H "Accept: application/json"
```

**Successful Response (200 OK):**
```json
{
  "success": true,
  "employeeCode": "EMP001",
  "deviceId": "device2",
  "timestamp": "2025-05-15T10:30:45.123Z",
  "message": "Employee code retrieved successfully"
}
```

**Error Responses:**

**400 Bad Request** - Missing or invalid device ID:
```json
{
  "message": "deviceId parameter is required"
}
```

**404 Not Found** - No employee code found for the device:
```json
{
  "message": "No employee code found for device: device2"
}
```

**500 Internal Server Error** - Device communication error:
```json
{
  "message": "Error communicating with fingerprint device",
  "details": "Connection timeout after 30 seconds"
}
```

---

### 2. Get Employee Code (Detailed)

Retrieves the employee code with performance metrics and detailed error information.

**Endpoint:**
```
GET /api/fingerprint/get-employee-code-detailed?deviceId={deviceId}
```

**Parameters:**
- `deviceId` (query, required): Device identifier

**Request Example:**
```bash
curl -X GET "http://localhost:5000/api/fingerprint/get-employee-code-detailed?deviceId=device2" \
  -H "Accept: application/json"
```

**Successful Response (200 OK):**
```json
{
  "success": true,
  "employeeCode": "EMP001",
  "deviceId": "device2",
  "requestStartTime": "2025-05-15T10:30:40.000Z",
  "requestEndTime": "2025-05-15T10:30:45.123Z",
  "elapsedMilliseconds": 5123,
  "message": "Employee code retrieved successfully",
  "errorDetails": null
}
```

**Error Response (500 Internal Server Error):**
```json
{
  "success": false,
  "employeeCode": null,
  "deviceId": "device2",
  "requestStartTime": "2025-05-15T10:30:40.000Z",
  "requestEndTime": "2025-05-15T10:30:45.123Z",
  "elapsedMilliseconds": 5123,
  "message": "Failed to retrieve employee code",
  "errorDetails": "Connection timeout after 3 retry attempts"
}
```

---

## Service Implementation

### IFingerprintDeviceService Interface

```csharp
public interface IFingerprintDeviceService
{
	/// <summary>
	/// Gets the employee code from fingerprint device using device ID
	/// </summary>
	/// <param name="deviceId">Device ID (e.g., "device2")</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Employee code as string</returns>
	Task<string?> GetEmployeeCodeAsync(string deviceId, CancellationToken cancellationToken = default);
}
```

### Usage Example in Your Code

```csharp
// Inject the service
private readonly IFingerprintDeviceService _fingerprintService;

public MyController(IFingerprintDeviceService fingerprintService)
{
	_fingerprintService = fingerprintService;
}

// Use it
var employeeCode = await _fingerprintService.GetEmployeeCodeAsync("device2");
```

---

## Features

### 1. **Retry Logic with Exponential Backoff**
- Automatically retries failed requests
- Exponential backoff strategy: delay increases with each attempt
- Configurable retry attempts and delay

### 2. **Timeout Handling**
- Individual request timeout
- Linked cancellation tokens for proper cleanup
- Prevents hanging requests

### 3. **Smart Error Handling**
- Distinguishes between client errors (4xx) and server errors (5xx)
- Doesn't retry on client errors (400, 404, etc.)
- Retries on server errors (500, 503) and network issues

### 4. **URL Construction**
- Automatically handles query parameters
- URL-encodes device ID to handle special characters
- Supports URLs that already contain query parameters

### 5. **Comprehensive Logging**
- Debug logs for each retry attempt
- Warning logs for failed attempts
- Error logs for exceptions
- Information logs for successful requests

---

## URL Building Example

The service constructs URLs as follows:

```csharp
// Input
BaseUrl: "http://175.29.147.115:8000/virdi/finger-print.php"
DeviceId: "device2"

// Output
"http://175.29.147.115:8000/virdi/finger-print.php?deviceId=device2"

// With special characters
DeviceId: "device@2#test"
Output: "http://175.29.147.115:8000/virdi/finger-print.php?deviceId=device%402%23test"
```

---

## Configuration Setup in Program.cs

The service is automatically registered in `Program.cs`:

```csharp
// Register fingerprint device configuration and service
var fingerprintConfig = new FingerprintDeviceConfiguration();
builder.Configuration.GetSection("FingerprintDevice").Bind(fingerprintConfig);
builder.Services.AddSingleton(fingerprintConfig);
builder.Services.AddHttpClient<IFingerprintDeviceService, FingerprintDeviceService>();
```

---

## Retry Strategy

The service implements exponential backoff retry strategy:

**Example with default config:**
- Initial attempt: Immediate
- Attempt 2 (fail): Wait 500ms, retry
- Attempt 3 (fail): Wait 1000ms (500ms × 2), retry
- Attempt 4 (fail): Wait 1500ms (500ms × 3), retry
- If still failing: Throw exception

**Total max wait time with defaults:** 500ms + 1000ms + 1500ms = 3000ms (3 seconds)

---

## Environment-Specific Configuration

### Development
```json
"FingerprintDevice": {
  "BaseUrl": "http://175.29.147.115:8000/virdi/finger-print.php",
  "TimeoutSeconds": 30,
  "RetryAttempts": 3,
  "RetryDelayMs": 500
}
```

### Production
```json
"FingerprintDevice": {
  "BaseUrl": "http://175.29.147.115:8000/virdi/finger-print.php",
  "TimeoutSeconds": 10,
  "RetryAttempts": 2,
  "RetryDelayMs": 1000
}
```

---

## Error Handling Flow

```
Request to /api/fingerprint/get-employee-code?deviceId=device2
		↓
Validate deviceId (not empty)
		↓
Build URL with deviceId parameter
		↓
Attempt 1: HTTP GET request
		├─ Success → Return employee code (200 OK)
		├─ 4xx Error → Throw exception immediately
		└─ 5xx Error or Timeout → Proceed to retry
		↓
Attempt 2: Wait 500ms, Retry
		├─ Success → Return employee code (200 OK)
		└─ Failure → Proceed to retry
		↓
Attempt 3: Wait 1000ms, Retry
		├─ Success → Return employee code (200 OK)
		└─ Failure → Throw exception
		↓
Return 500 Internal Server Error response
```

---

## Testing the API

### Using Postman

1. **Create a new GET request:**
   - URL: `http://localhost:5000/api/fingerprint/get-employee-code`
   - Query Parameters:
	 - Key: `deviceId`
	 - Value: `device2`

2. **Send and view the response**

### Using cURL

```bash
# Simple endpoint
curl -X GET "http://localhost:5000/api/fingerprint/get-employee-code?deviceId=device2"

# Detailed endpoint with performance metrics
curl -X GET "http://localhost:5000/api/fingerprint/get-employee-code-detailed?deviceId=device2"

# Pretty print JSON response
curl -X GET "http://localhost:5000/api/fingerprint/get-employee-code?deviceId=device2" | jq .
```

### Using JavaScript/Fetch

```javascript
async function getEmployeeCode(deviceId) {
  try {
	const response = await fetch(
	  `/api/fingerprint/get-employee-code?deviceId=${encodeURIComponent(deviceId)}`
	);

	if (!response.ok) {
	  throw new Error(`HTTP error! status: ${response.status}`);
	}

	const data = await response.json();

	if (data.success) {
	  console.log(`Employee Code: ${data.employeeCode}`);
	  return data.employeeCode;
	} else {
	  console.error(`Error: ${data.message}`);
	  return null;
	}
  } catch (error) {
	console.error('Fetch error:', error);
	return null;
  }
}

// Usage
getEmployeeCode('device2').then(code => {
  console.log('Got employee code:', code);
});
```

---

## Troubleshooting

### Issue: "No employee code found for device"

**Causes:**
- Device ID doesn't exist on the fingerprint device
- Device hasn't recorded any data for this ID
- Fingerprint device is not properly configured

**Solutions:**
- Verify the device ID is correct
- Check if the device has data for this ID
- Test the URL directly in a browser

### Issue: Connection timeout

**Causes:**
- Fingerprint device is unreachable
- Network connectivity issue
- Device is down or overloaded

**Solutions:**
- Verify the BaseUrl in appsettings.json
- Check network connectivity to the device
- Verify firewall rules allow the connection
- Increase TimeoutSeconds in configuration

### Issue: Repeated 500 errors after retries

**Causes:**
- All retry attempts failed
- Server-side error on fingerprint device
- Persistent network issue

**Solutions:**
- Check fingerprint device logs
- Verify device is running and responding
- Check network connectivity
- Contact device administrator

---

## Files Created/Modified

| File | Purpose |
|------|---------|
| `FingerprintDeviceConfiguration.cs` | Configuration class for fingerprint device settings |
| `FingerprintDeviceService.cs` | Service implementation for fingerprint device API calls |
| `FingerprintController.cs` | API endpoints for fingerprint device integration |
| `appsettings.Development.json` | Development configuration |
| `appsettings.Production.json` | Production configuration |
| `Program.cs` | Dependency injection registration |

---

## Response Status Codes

| Code | Meaning | When |
|------|---------|------|
| 200 | OK | Employee code successfully retrieved |
| 400 | Bad Request | Device ID is missing or invalid |
| 404 | Not Found | No employee code found for the device |
| 500 | Internal Server Error | Error communicating with fingerprint device or timeout after retries |

---

## Future Enhancements

Potential improvements for future versions:

1. **Caching**: Cache employee codes with TTL to reduce device calls
2. **Rate Limiting**: Implement rate limiting per device
3. **Batch Operations**: Get employee codes for multiple devices in one call
4. **Device Health Check**: Endpoint to verify fingerprint device connectivity
5. **Metrics**: Prometheus metrics for monitoring API performance
6. **Circuit Breaker**: Implement circuit breaker pattern for device failures
