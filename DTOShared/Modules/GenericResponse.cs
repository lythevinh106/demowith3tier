
namespace DTOShared.Modules
{
    public class GenericResponse<T>
    {


        public string Msg { get; set; }

        public T? Values { get; set; }


    }
}
