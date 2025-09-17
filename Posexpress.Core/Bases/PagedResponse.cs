namespace OKR.Domain.Bases;

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; } = Array.Empty<T>();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }

    public PagedResponse(IEnumerable<T> data, int page, int pageSize, int totalRecords)
    {
        Data = data;
        Page = page;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        HasNextPage = page < TotalPages;
        HasPreviousPage = page > 1;
    }
}
