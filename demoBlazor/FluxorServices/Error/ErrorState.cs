using Fluxor;

namespace demoBlazor.FluxorServices.Error
{
    [FeatureState]
    public class ErrorState
    {




        public bool _isAtive { get; }
        public string _msg { get; } = null;



        private ErrorState()
        {
            // Hàm tạo không tham số
        }

        public ErrorState(bool isActive, string msg

            )
        {
            _isAtive = isActive;
            _msg = msg;


        }



    }
}
