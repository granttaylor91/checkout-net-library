using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.ShoppingList.ResponseModels;
using Checkout.ApiServices.ShoppingList.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.ShoppingList
{
    public class ShoppingListService
    {

    
        public HttpResponse<OkResponse> AddDrinkOrder(BaseDrinkOrder request)
        {
            return new ApiHttpClient().PostRequest<OkResponse>(ApiUrls.ShoppingList, AppSettings.SecretKey, request);
        }

        public HttpResponse<DrinkOrders> GetDrinks()
        {
            return new ApiHttpClient().GetRequest<DrinkOrders>(ApiUrls.ShoppingList, AppSettings.SecretKey);
        }

        public HttpResponse<BaseDrinkOrder> GetDrink(string drinkName)
        {
            return new ApiHttpClient().GetRequest<BaseDrinkOrder>(string.Format("{0}{1}", ApiUrls.ShoppingList, drinkName), AppSettings.SecretKey);
        }

        public HttpResponse<OkResponse> UpdateDrink(BaseDrinkOrder request)
        {
            return new ApiHttpClient().PutRequest<OkResponse>(ApiUrls.ShoppingList, AppSettings.SecretKey, request);
        }

        public HttpResponse<OkResponse> DeleteDrink(string drinkName)
        {
            return new ApiHttpClient().DeleteRequest<OkResponse>(string.Format("{0}{1}", ApiUrls.ShoppingList, drinkName), AppSettings.SecretKey);
        }

    }
}
