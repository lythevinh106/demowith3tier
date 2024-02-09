using Fluxor;

namespace demoBlazor.FluxorServices.Other
{

    [FeatureState]
    public class OtherState
    {
        private bool isLoading;
        private bool isToast;
        private string msg;

        public bool _isLoading { get; }

        public bool _isToast { get; }

        public string _msg { get; }

        private OtherState()
        {
            // Hàm tạo không tham số
        }

        public OtherState(bool isLoading, bool isToast, string msg

            )
        {
            _isLoading = isLoading;
            _isToast = isToast;

            _msg = msg;
        }


    }


}
