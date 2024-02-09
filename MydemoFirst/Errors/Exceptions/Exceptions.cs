namespace MydemoFirst.Errors.Exceptions
{
    public abstract class Exceptions : Exception
    {


        public Exceptions(string message) : base(message)
        {


        }



    }


    public class ExceptionNotFound : Exceptions
    {
        public ExceptionNotFound(string message) : base(message)
        {


        }
    }

    public class ExceptionBadRequest : Exception
    {
        public ExceptionBadRequest(string message) : base(message)
        {
        }
    }

    public class ExceptionUnauthorized : Exception
    {
        public ExceptionUnauthorized(string message) : base(message)
        {
        }
    }

    public class ExceptionForbid : Exception
    {
        public ExceptionForbid(string message) : base(message)
        {
        }
    }



}
