namespace HealthcareSystem.Application.Responses;

public static class ResponsesData {
    public record GetList<T>(int? TotalCount, IEnumerable<T> Data);
}