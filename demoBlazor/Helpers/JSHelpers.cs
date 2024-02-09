


using Microsoft.JSInterop;

namespace demoBlazor.Helpers
{

    public class JsHelper
    {
        private readonly IJSRuntime _jsRuntime;

        public JsHelper(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Log(string message)
        {
            await _jsRuntime.InvokeVoidAsync("console.log", message);
        }
        public async Task LogObject(object message)
        {
            await _jsRuntime.InvokeVoidAsync("console.log", message);
        }
    }
}
