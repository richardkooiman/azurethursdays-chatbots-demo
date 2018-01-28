using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace AzureThursdays.Chatbots.Demo.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text == "pizza")
            {
                var formDialog = new Microsoft.Bot.Builder.FormFlow.FormDialog<Models.Pizza>(new Models.Pizza());

                await context.PostAsync($"Please tell me how you would like your pizza.");
                await context.Forward(formDialog, PizzaFormComplete, activity);
            }
            else
            {
                await DefaultMessageReceivedAsync(context, result);
            }
        }

        private async Task PizzaFormComplete(IDialogContext context, IAwaitable<Models.Pizza> result)
        {
            var activity = await result as Models.Pizza;

            await context.PostAsync($"Your choice: {activity}.");

            context.Wait(MessageReceivedAsync);
        }

        private async Task DefaultMessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }
    }
}