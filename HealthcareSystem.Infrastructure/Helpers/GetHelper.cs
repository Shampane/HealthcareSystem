namespace HealthcareSystem.Infrastructure.Helpers;

internal class GetHelper {
    public IQueryable<T> AddPagination<T>(
        IQueryable<T> query, int? pageSize, int? pageIndex
    ) {
        IQueryable<T>? newQuery = pageSize.HasValue && pageIndex.HasValue
            ? query.Skip((pageIndex.Value - 1) * pageSize.Value)
            : query;
        return pageSize.HasValue ? newQuery.Take(pageSize.Value) : newQuery;
    }
}