using System.Text.Json;

namespace MydemoFirst.Errors
{
    public class ResponseModelErrors
    {

        public int StatusCode { get; set; }


        public Dictionary<string, List<string>> Message { get; set; }

        public override string ToString()
        {

            return JsonSerializer.Serialize(this);
        }
    }
}
