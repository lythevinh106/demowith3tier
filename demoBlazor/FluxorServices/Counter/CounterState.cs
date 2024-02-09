using Fluxor;

namespace demoBlazor.FluxorServices.Counter
{

    [FeatureState]
    public class CounterState
    {
        public int ClickCount { get; }

        private CounterState()
        {
            // Hàm tạo không tham số
        }

        public CounterState(int clickCount) => ClickCount = clickCount;
    }


}
