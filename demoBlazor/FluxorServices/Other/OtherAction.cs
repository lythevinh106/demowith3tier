namespace demoBlazor.FluxorServices.Other
{


    public class ActiveLoading
    {
        public bool _active { get; }


        public ActiveLoading(bool active)
        {
            _active = active;

        }

    }

    public class ActiveToast
    {
        public bool _isToast { get; }

        public string _msg { get; }


        public ActiveToast(bool isToast, string msg)
        {
            _isToast = isToast;
            _msg = msg;

        }

    }

    public class HideToast
    {

    }
}
