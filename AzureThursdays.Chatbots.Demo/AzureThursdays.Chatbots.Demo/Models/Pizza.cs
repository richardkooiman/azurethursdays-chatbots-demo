using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureThursdays.Chatbots.Demo.Models
{
    [Serializable]
    public class Pizza
    {
        public PizzaSize Size { get; set; }

        public string FirstIngredient { get; set; }

        public string SecondIngredient { get; set; }

        public string Time { get; set; }

        public override string ToString()
        {
            return $"{Size} pizza with {FirstIngredient} and {SecondIngredient} delivered at {Time}";
        }
    }
}