
namespace api.Helpers
{
    public class QueryObject
    {
        public bool OrderBySymbol { get; set; } = false;
        public bool OrderByName { get; set; } = false;
        public bool OrderByPrice { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

    }
}