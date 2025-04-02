namespace Domain.ValueObjects
{
    public record PagedResult<T>(IEnumerable<T> Items, int TotalCount, int PageNumber, int PageSize)
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
    public record PaginationOptions(int PageNumber, int PageSize);
}