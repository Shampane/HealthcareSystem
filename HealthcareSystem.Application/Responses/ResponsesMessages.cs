namespace HealthcareSystem.Application.Responses;

public static class ResponsesMessages {
    public static object NotFound(string message) {
        return new { Message = $"{message}" };
    }

    public static string UpdatedIdName(string entity, string id, string name) {
        return
            $"""
             The {entity} was updated
             - Id: {id}
             - Name: {name}
             """;
    }

    public static string UpdatedIdTime(
        string entity, string id, DateTimeOffset startTime, DateTimeOffset endTime
    ) {
        return
            $"""
             The {entity} was updated
             - Id: {id}
             - StartTime: {startTime}
             - EndTime: {endTime}
             """;
    }
}