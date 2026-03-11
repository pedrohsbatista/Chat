using Chat.Web.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Chat.Web.Pages
{
    public partial class Index
    {
        [Inject]
        public IJSRuntime JsRuntime {  get; set; }

        [Inject]
        public AppSettings AppSettings { get; set; }

        public string Message { get; set;  }

        public List<string> Messages { get; set; } = new List<string>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var reference = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync("chat.startConnection", $"{AppSettings.ApiUrl}/chat", reference);
            }
            
            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task SendMessage()
        {
           await JsRuntime.InvokeVoidAsync("chat.sendMessage", Message);
        }

        [JSInvokable("ReceiveMessage")]
        public void ReceiveMessage(string message)
        {
            Messages.Add(message);
        }
    }
}