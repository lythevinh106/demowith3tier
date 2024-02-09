namespace DTOShared.Pagging
{
    public class PaggingResponse<T>
    {

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public bool hasNext { get; set; }

        public bool hasPrv { get; set; }

        public List<T> Data { get; set; }


    }
}
