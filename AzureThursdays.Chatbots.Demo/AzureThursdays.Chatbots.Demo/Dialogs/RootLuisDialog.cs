using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Connector;

namespace AzureThursdays.Chatbots.Demo.Dialogs
{
    [Microsoft.Bot.Builder.Luis.LuisModel("YOUR_MODEL_ID", "YOUR_SUBSCRIPTION_KEY")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, Microsoft.Bot.Builder.Luis.Models.LuisResult result)
        {
            // calculate something for us to return
            int length = (result.Query ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {result.Query} which was {length} characters");
            
            context.Wait(MessageReceived);
        }

        [LuisIntent("Pizza.Order")]
        public async Task PizzaOrder(IDialogContext context, Microsoft.Bot.Builder.Luis.Models.LuisResult result)
        {
            var pizza = new Models.Pizza();
            
            Newtonsoft.Json.Linq.JArray dateTimeEntity = result.Entities?.FirstOrDefault(e => e.Type == "builtin.datetimeV2.datetime")?.Resolution.FirstOrDefault().Value as Newtonsoft.Json.Linq.JArray;
            if(dateTimeEntity != null)
            {
                DateTime dateTime = dateTimeEntity.First.Value<DateTime>("value");

                pizza.Time = dateTime.TimeOfDay.ToString(@"hh\:mm");
            }

            var formDialog = new Microsoft.Bot.Builder.FormFlow.FormDialog<Models.Pizza>(pizza);

            await context.PostAsync($"Please tell me how you would like your pizza.");
            await context.Forward(formDialog, PizzaFormComplete, context.MakeMessage());
        }

        private async Task PizzaFormComplete(IDialogContext context, IAwaitable<Models.Pizza> result)
        {
            var activity = await result as Models.Pizza;

            await context.PostAsync($"Your choice: {activity}.");

            context.Wait(MessageReceived);
        }
    }
}