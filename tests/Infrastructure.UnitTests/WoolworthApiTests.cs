using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.Products.Models;
using Woolworth.Application.User.Queries;
using Woolworth.Infrastructure.Api;

namespace Woolworth.Infrastructure.UnitTests
{
    public class WoolworthApiTests
    {
        private const string UrlForGetProducts = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/products";
        private const string BaseUrl = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/";
        private Mock<IUserService> _userService;
        private MockHttpMessageHandler _mockHttp;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _mockHttp = new MockHttpMessageHandler();
        }

        [Test]
        public async Task GetProducts_ShouldCallCorrectUri()
        {
            //Arrange
            var dummyUser = GetDummyUser();
            var sampleProducts = GetSampleProducts();
            _userService.Setup(i => i.GetCurrentUser()).Returns(dummyUser);

            _mockHttp.When($"{UrlForGetProducts}?token={dummyUser.Token}").Respond("application/json", GetSampleProductsSerialized());

            var client = new HttpClient(_mockHttp);
            client.BaseAddress = new Uri(BaseUrl);

            //Act
            var api = new WoolworthApi(client, _userService.Object);
            var products = await api.GetProducts();

            //Assert
            _userService.Verify(i => i.GetCurrentUser(), Times.Once, "Get Current user on user service should be invoked");
            Assert.AreEqual(sampleProducts.Count, products.Count(), "Api should call correct url and return correct number of products");

        }

        public UserDto GetDummyUser()
        {
            return new UserDto("test", "abc-123");
        }

        private List<ProductDto> GetSampleProducts()
        {
            var sampleProducts = new List<ProductDto>
            {
                new ProductDto
                {
                    Name = "Product 1",
                    Price = 10,
                    Quantity = 1,

                },
                new ProductDto
                {
                    Name = "Product 2",
                    Price = 15,
                    Quantity = 2,
                },
                new ProductDto
                {
                    Name = "Product 3",
                    Price = 11,
                    Quantity = 3,
                }
            };
            return sampleProducts;
        }

        private string GetSampleProductsSerialized()
        {
            var sampleProducts = GetSampleProducts();
            var serializerSetting = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(sampleProducts, serializerSetting);
        }

    }
}
