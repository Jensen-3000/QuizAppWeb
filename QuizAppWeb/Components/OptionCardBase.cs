using Microsoft.AspNetCore.Components;

namespace QuizAppWeb.Components
{
    public class OptionCardBase : ComponentBase
    {
        [Parameter]
        public string Option { get; set; }
        [Parameter]
        public EventCallback<string> OnOptionSelected { get; set; }

        protected async void SelectOption()
        {
            await OnOptionSelected.InvokeAsync(Option);
        }
    }
}
