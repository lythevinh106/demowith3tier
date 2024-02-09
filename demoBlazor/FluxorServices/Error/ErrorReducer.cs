using Fluxor;

namespace demoBlazor.FluxorServices.Error
{
    public static class ErrorReducer
    {

        [ReducerMethod]
        public static ErrorState ReduceActiveLoadingAction(
            ErrorState state,
            ActiveError action)
        {
            return new ErrorState(

                isActive: action._isActive,
                msg: action._msg

                );
        }

    }
}