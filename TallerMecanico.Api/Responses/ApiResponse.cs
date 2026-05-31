using TallerMecanico.Core.Pagination;

namespace TallerMecanico.Api.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }

    public string? Message { get; set; }
    public object? Errors { get; set; }
    public PaginationMetadata? Pagination { get; set; }

    public ApiResponse(T? data, bool success = true, string? message = null, object? errors = null, PaginationMetadata? pagination = null)
    {
        Success = success;
        Data = data;
        Message = message;
        Errors = errors;
        Pagination = pagination;
    }
}