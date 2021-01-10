using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Woolworth.Application.Products.Models;
using Woolworth.Application.Trolley.Models;
using Woolworth.Infrastructure.Api;
using Woolworth.Infrastructure.Services;

namespace Woolworth.Infrastructure.UnitTests
{
    public class ProductServiceTests
    {
        private Mock<IWoolworthApi> _api;

        [SetUp]
        public void Setup()
        {
            _api = new Mock<IWoolworthApi>();
        }

        [Test]
        public async Task ProductServiceGetProducts_CallApiCorrectly()
        {
            //Arrange 
            var sampleProducts = GetSampleProducts();
            _api.Setup(i => i.GetProducts()).ReturnsAsync(sampleProducts);
            
            var service = new ProductsService(_api.Object);

            //Act
            var products =await service.GetProducts();

            //Assert
            _api.Verify(i => i.GetProducts(), Times.Once, "Get Products API should be invoked");

            Assert.AreEqual(sampleProducts.Count, products.Count(), "Should return correct number of products");

        }


        [Test]
        public async Task ProductServiceGetHistory_CallApiCorrectly()
        {
            //Arrange 
            var sampleHistory = GetSampleHistory();
            _api.Setup(i => i.GetShopperHistory()).ReturnsAsync(sampleHistory);

            var service = new ProductsService(_api.Object);

            //Act
            var history = await service.GetShopperHistory();

            //Assert
            _api.Verify(i => i.GetShopperHistory(), Times.Once, "Get Shopper History API should be invoked");

            Assert.AreEqual(sampleHistory.Count, history.Count(), "Should return correct number of history items");

        }

        [Test]
        public async Task ProductServiceGetTrolleyTotal_CallApiCorrectly()
        {
            //Arrange 
            _api.Setup(i => i.GetTrolleyTotal(It.IsAny<TrolleyDto>())).ReturnsAsync(12);

            var service = new ProductsService(_api.Object);

            //Act
            var total = await service.GetTrolleyTotal(new TrolleyDto());

            //Assert
            _api.Verify(i => i.GetTrolleyTotal(It.IsAny<TrolleyDto>()), Times.Once, "Get Trolley Total API should be invoked");

            Assert.AreEqual(12, total, "Should return correct total for Trolley");

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

        private List<HistoryDto> GetSampleHistory()
        {
            var history = new List<HistoryDto>();

            var firstItem = new HistoryDto
            {
                CustomerId = 12,
                Products = GetSampleProducts()
            };


            var secondItem = new HistoryDto
            {
                CustomerId = 13,
                Products = GetSampleProducts().Skip(2),
            };

            history.Add(firstItem);
            history.Add(secondItem);
            return history;
        }
    }
}