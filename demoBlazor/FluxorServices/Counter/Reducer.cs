using Fluxor;

namespace demoBlazor.FluxorServices.Counter
{
    public static class Reducers
    {
        [ReducerMethod]
        public static CounterState ReduceIncrementCounterAction(
            CounterState state,
            IncrementCounterAction action)
        {
            return new CounterState(clickCount: state.ClickCount + 1);
        }


        [ReducerMethod]
        public static CounterState ReduceResetCounterAction(
           CounterState state,
          ResetCounterAction action)
        {
            return new CounterState(clickCount: 0);
        }
    }


}