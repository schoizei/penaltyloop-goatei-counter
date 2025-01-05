using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GoateiCounter.Extensions;

public static class JSRuntimeExtensions
{
    public static ValueTask FocusAsync(this IJSRuntime jsRuntime, ElementReference elementReference)
    {
        return jsRuntime.InvokeVoidAsync("BlazorFocusElement", elementReference);
    }
}
