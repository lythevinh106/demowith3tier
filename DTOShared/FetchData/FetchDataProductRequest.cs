using DTOShared.Enums;

namespace DTOShared.FetchData
{
    public class FetchDataProductRequest : FetchDataRequest
    {
        public int? Price { get; set; } = null;

        public int? CategoryId { get; set; } = null;

        public SortProductOption? Sort { get; set; } = null;
    }
}