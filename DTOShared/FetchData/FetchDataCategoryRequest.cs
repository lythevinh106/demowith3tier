using DTOShared.Enums;

namespace DTOShared.FetchData
{
    public class FetchDataCategoryRequest : FetchDataRequest
    {
        public SortIdEnum? Sort { get; set; } = null;
    }
}
