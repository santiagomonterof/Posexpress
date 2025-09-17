namespace OKR.Domain.Bases;

public class PagedRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;

    public string? FilterColumn = string.Empty;
    public string? FilterValue = string.Empty;

    public int ValidatedPage => Page < 1 ? 1 : Page;
    public int ValidatedPageSize => PageSize < 1 ? 50 : PageSize > 100 ? 100 : PageSize;

    public int Skip => (ValidatedPage - 1) * ValidatedPageSize;
    public int Take => ValidatedPageSize;
}
