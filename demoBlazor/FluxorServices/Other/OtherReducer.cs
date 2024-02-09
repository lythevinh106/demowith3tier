

using Fluxor;

namespace demoBlazor.FluxorServices.Other
{
    public static class OtherReducer
    {
        [ReducerMethod]
        public static OtherState ReduceActiveLoadingAction(
            OtherState state,
            ActiveLoading action)
        {
            return new OtherState(isLoading: action._active,

                isToast: false,

                msg: state._msg

                );
        }


        [ReducerMethod]
        public static OtherState ReduceActiveToastAction(
          OtherState state,
          ActiveToast action)
        {
            return new OtherState(
                isLoading: false,
                isToast: action._isToast,
                msg: action._msg


                );
        }



        //[ReducerMethod]
        //public static OtherState ReduceHideToastAction(
        //  OtherState state,
        //  HideToast action)
        //{
        //    return new OtherState(
        //        isLoading: false,
        //        isToast: false,
        //        toastType: state._toastType,
        //        msg: state._msg




        //        );
        //}
    }

}