namespace VenomHubPhotoWebSiteAdminPanel.Models
{
    public class PaginationModel
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int PageCount { get; set; } = 0;
        public string SortField { get; set; } = "Id";
        public string SortOrder { get; set; } = "asc";

        public bool IsEndOfPage => PageNo >= PageCount;
    }

    public class PaginatedResponseModel<T>
    {
        public PaginationModel Pagination { get; set; } = new PaginationModel();
        public List<T> Data { get; set; } = new List<T>();
    }

    public class AlbumResponseModel : PaginatedResponseModel<AlbumModel> { }
    public class CategoryResponseModel : PaginatedResponseModel<CategoryModel> { }
    public class PhotoResponseModel : PaginatedResponseModel<PhotoModel> { }
}
