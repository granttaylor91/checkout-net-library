using Checkout.ApiServices.ShoppingList;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Checkout.ApiServices.ShoppingList.ResponseModels;
using Checkout.ApiServices.ShoppingList.SharedModels;

namespace Tests.ShoppingListService
{
    [TestFixture]
    public class ShoppingListServiceTests : BaseServiceTests
    {


        private BaseDrinkOrder drinkOrder;

        [TestFixtureSetUp]
        public void Setup()
        {
            //Data is persistant for lifespan of service, use new values each time.
            drinkOrder = TestHelper.GetBaseDrinkOrder();
        }



        [Test]
        public void AddDrinkOrder()
        {
          
            var response = CheckoutClient.ShoppingListService.AddDrinkOrder(drinkOrder);
            var duplicateResponse = CheckoutClient.ShoppingListService.AddDrinkOrder(drinkOrder);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);

            duplicateResponse.Should().NotBeNull();
            duplicateResponse.HttpStatusCode.Should().Be(HttpStatusCode.Conflict);

        }



        /// <summary>
        /// This is broken, ran out of time to make changes to fix it.
        /// </summary>
        [Test]
        public void GetDrinks()
        {
            CheckoutClient.ShoppingListService.AddDrinkOrder(drinkOrder);

            var response = CheckoutClient.ShoppingListService.GetDrinks();

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            var responseModel = response.Model;

            response.Model.ShoppingList.Count().Should().Be(1);
            response.Model.ShoppingList.FirstOrDefault().Name.Should().BeEquivalentTo(drinkOrder.Name);
            response.Model.ShoppingList.FirstOrDefault().Quantity.Should().Be(drinkOrder.Quantity);
        }

        [Test]
        public void GetDrink()
        {
            CheckoutClient.ShoppingListService.AddDrinkOrder(drinkOrder);

            var response = CheckoutClient.ShoppingListService.GetDrink(drinkOrder.Name);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().BeEquivalentTo(drinkOrder.Name);
            response.Model.Quantity.Should().Be(drinkOrder.Quantity);
        }

        [Test]
        public void UpdateDrink()
        {
            //Arrange
            var initialDrinkOrder = drinkOrder;

            var updateDrinkOrder = new BaseDrinkOrder
            {
                Name = drinkOrder.Name,
                Quantity = 20
            };
          


            //add initial drink was added and exists
            CheckoutClient.ShoppingListService.AddDrinkOrder(initialDrinkOrder);
            var checkDrink = CheckoutClient.ShoppingListService.GetDrink(initialDrinkOrder.Name);

            checkDrink.Should().NotBeNull();
            checkDrink.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            checkDrink.Model.Name.Should().BeEquivalentTo(initialDrinkOrder.Name);
            checkDrink.Model.Quantity.Should().Be(initialDrinkOrder.Quantity);

            var updateDrinkResponse = CheckoutClient.ShoppingListService.UpdateDrink(updateDrinkOrder);

            updateDrinkResponse.Should().NotBeNull();
            updateDrinkResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            //check drink was updated.
            var updatedDrink = CheckoutClient.ShoppingListService.GetDrink(updateDrinkOrder.Name);

            updatedDrink.Should().NotBeNull();
            updatedDrink.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            updatedDrink.Model.Name.Should().BeEquivalentTo(updateDrinkOrder.Name);
            updatedDrink.Model.Quantity.Should().Be(updateDrinkOrder.Quantity);
        }


        /// <summary>
        /// This is broken, ran out of time to make changes to fix it.
        /// </summary>
        [Test]
        public void DeleteDrink()
        {
         
            CheckoutClient.ShoppingListService.AddDrinkOrder(drinkOrder);

            var checkDrink = CheckoutClient.ShoppingListService.GetDrink(drinkOrder.Name);

            checkDrink.Should().NotBeNull();
            checkDrink.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            checkDrink.Model.Name.Should().BeEquivalentTo(drinkOrder.Name);

            var deleteResponse = CheckoutClient.ShoppingListService.DeleteDrink(drinkOrder.Name);

            //TODO: check against response.

            //Check it is really deleted
            var checkDrinkAgain = CheckoutClient.ShoppingListService.GetDrink(drinkOrder.Name);

            checkDrink.Should().NotBeNull();
            checkDrink.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);

        }
    }


}

