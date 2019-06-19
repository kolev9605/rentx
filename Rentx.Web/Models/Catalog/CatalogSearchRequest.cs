namespace Rentx.Web.Models.Catalog
{
    public class CatalogSearchRequest
    {
        public int? CategoryId { get; set; }

        public string SearchTerm { get; set; }

        public string OrderBy { get; set; }
    }
}
