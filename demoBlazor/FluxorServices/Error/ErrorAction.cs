namespace demoBlazor.FluxorServices.Error
{
    public class ActiveError
    {


        public bool _isActive { get; }
        public string _msg { get; } = null;


        public ActiveError(bool isActive, string msg)
        {
            _isActive = isActive;
            _msg = msg;



        }



    }
}
