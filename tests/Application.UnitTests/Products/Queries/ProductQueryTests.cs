using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.Common.Models;
using Woolworth.Application.Products.Models;
using Woolworth.Application.Products.Queries;

namespace Woolworth.Application.UnitTests.Products.Queries
{
    public class ProductQueryTests
    {
        private Mock<IProductsService> _mockProductsService;
        private Mock<IRecommendationEngine> _mockEngine;
        
        [SetUp]
        public void Setup()
        {
            _mockProductsService = new Mock<IProductsService>();
            _mockEngine = new Mock<IRecommendationEngine>();
        }

        [Test]
        public async Task GetSortedProductsQuery_SortByDescending()
        {
            //arrange
            var sampleProducts = GetSampleProducts();
            _mockProductsService.Setup(i => i.GetProducts()).ReturnsAsync(sampleProducts);
            var service = _mockProductsService.Object;
            var engine = _mockEngine.Object;
            var handler = new GetSortedProductsQueryHandler(service, engine);

            var query = new GetSortedProductsQuery(SortOrder.Descending);

            //act
            var results = await handler.Handle(query, default(CancellationToken));

            //assert
            _mockProductsService.Verify(i => i.GetProducts(), Times.Once, "Get Products Method should be invoked in produts service");

            //Assert that first item name is "Product 3"
            Assert.AreEqual(results.First().Name, "Product 3", "Product with name product three should be the first product");
        }

        [Test]
        public async Task GetSortedProductsQuery_SortByHigh()
        {
            //arrange
            var sampleProducts = GetSampleProducts();
            _mockProductsService.Setup(i => i.GetProducts()).ReturnsAsync(sampleProducts);
            var service = _mockProductsService.Object;
            var engine = _mockEngine.Object;
            var handler = new GetSortedProductsQueryHandler(service, engine);

            var query = new GetSortedProductsQuery(SortOrder.High);

            //act
            var results = await handler.Handle(query, default(CancellationToken));

            //assert
            _mockProductsService.Verify(i => i.GetProducts(), Times.Once, "Get Products Method should be invoked in produts service");

            //Assert that first item name is "Product 2" with highest price
            Assert.AreEqual(results.First().Name, "Product 2", "Product with name Product 2 should be the first product");
        }

        [Test]
        public async Task GetSortedProductsQuery_SortByLow()
        {
            //arrange
            var sampleProducts = GetSampleProducts();
            _mockProductsService.Setup(i => i.GetProducts()).ReturnsAsync(sampleProducts);
            var service = _mockProductsService.Object;
            var engine = _mockEngine.Object;
            var handler = new GetSortedProductsQueryHandler(service, engine);

            var query = new GetSortedProductsQuery(SortOrder.Low);

            //act
            var results = await handler.Handle(query, default(CancellationToken));

            //assert
            _mockProductsService.Verify(i => i.GetProducts(), Times.Once, "Get Products Method should be invoked in produts service");

            //Assert that first item name is "Product 1" with Lowes price
            Assert.AreEqual(results.First().Name, "Product 1", "Product with name Product 1 should be the first product");
        }

        [Test]
        public async Task GetSortedProductsQuery_SortByRecommended()
        {
            //arrange
            var sampleProducts = GetSampleProducts();
            _mockProductsService.Setup(i => i.GetProducts()).ReturnsAsync(sampleProducts);

            var sampleHistory = GetSampleHistory();

            _mockProductsService.Setup(i => i.GetShopperHistory()).ReturnsAsync(sampleHistory);

            var service = _mockProductsService.Object;
            var engine = new RecommendationEngine(service);
            var handler = new GetSortedProductsQueryHandler(service, engine);

            var query = new GetSortedProductsQuery(SortOrder.Recommended);

            //act
            var results = await handler.Handle(query, default(CancellationToken));

            //assert
            _mockProductsService.Verify(i => i.GetProducts(), Times.Once, "Get Products Method should be invoked in produts service");
            _mockProductsService.Verify(i => i.GetShopperHistory(), Times.Once, "Get history method should be invoked in products service");


            //Assert that first item name is "Product 3" with most quantity
            Assert.AreEqual(results.First().Name, "Product 3", "Product with name Product 3 should be the first product");
            Assert.AreEqual(results.Last().Name, "Product 1", "Product with name Product 1 should be the last product");
        }

        #region Data Generator Methods

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

        #endregion
    }
}
