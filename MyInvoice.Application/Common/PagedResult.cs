namespace MyInvoice.Application.Common;

public class PagedResult<T>
{
    private PagedResult() { }

    public IReadOnlyCollection<T>? Items { get; private init; }
    public int PageNumber { get; private init; }
    public int PageSize { get; private init; }
    public int TotalCount { get; private init; }
    public int TotalPages { get; private init; }

    public static PagedResult<T> Of(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
    {
        return new PagedResult<T>
        {
            Items = items as IReadOnlyCollection<T> ?? items.ToArray(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
