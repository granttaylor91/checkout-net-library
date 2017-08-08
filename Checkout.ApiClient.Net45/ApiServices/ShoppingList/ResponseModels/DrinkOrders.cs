using Checkout.ApiServices.ShoppingList.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.ShoppingList.ResponseModels
{
    public class DrinkOrders
    {
        public IEnumerable<BaseDrinkOrder> ShoppingList { get; set; }
    }
}
