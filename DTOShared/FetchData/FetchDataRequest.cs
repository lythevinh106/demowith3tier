namespace DTOShared.FetchData
{
    public class FetchDataRequest
    {

        const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;

        public string? Search { get; set; } = null;


        public int PageSize
        {
            get
            {
                return _pageSize;


            }

            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }


        }
    }
}
