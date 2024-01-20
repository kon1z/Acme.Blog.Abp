using System;

namespace Acme.Auditing.Indexes
{
	public class RequestElasticsearchIndex(
		string? applicationName,
		Guid? userId,
		string? userName,
		Guid? tenantId,
		string? tenantName,
		Guid? impersonatorUserId,
		Guid? impersonatorTenantId,
		string? impersonatorUserName,
		string? impersonatorTenantName,
		DateTime executionTime,
		int executionDuration,
		string? clientId,
		string? correlationId,
		string? clientIpAddress,
		string? clientName,
		string? browserInfo,
		string? httpMethod,
		int? httpStatusCode,
		string? url,
		string? httpRequestHeader,
		string? httpRequestBody)
	{
		public string? ApplicationName { get; set; } = applicationName;
		public Guid? UserId { get; set; } = userId;
		public string? UserName { get; set; } = userName;
		public Guid? TenantId { get; set; } = tenantId;
		public string? TenantName { get; set; } = tenantName;
		public Guid? ImpersonatorUserId { get; set; } = impersonatorUserId;
		public Guid? ImpersonatorTenantId { get; set; } = impersonatorTenantId;
		public string? ImpersonatorUserName { get; set; } = impersonatorUserName;
		public string? ImpersonatorTenantName { get; set; } = impersonatorTenantName;
		public DateTime ExecutionTime { get; set; } = executionTime;
		public int ExecutionDuration { get; set; } = executionDuration;
		public string? ClientId { get; set; } = clientId;
		public string? CorrelationId { get; set; } = correlationId;
		public string? ClientIpAddress { get; set; } = clientIpAddress;
		public string? ClientName { get; set; } = clientName;
		public string? BrowserInfo { get; set; } = browserInfo;
		public string? HttpMethod { get; set; } = httpMethod;
		public int? HttpStatusCode { get; set; } = httpStatusCode;
		public string? Url { get; set; } = url;
		public string? HttpRequestHeader { get; set; } = httpRequestHeader;
		public string? HttpRequestBody { get; set; } = httpRequestBody;
	}
}
