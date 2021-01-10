using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using Woolworth.Application.Common.Models;
using Woolworth.Application.Products.Models;
using Woolworth.Application.Products.Queries;
using Woolworth.WebUI.Controllers;

namespace Woolworth.Web.UnitTests
{
    public class ProductsControllerTests
    {
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task GetProducts_ShouldInvokeGetProductsQuery()
        {
            //Arrange

            var query = new GetSortedProductsQuery(SortOrder.High);
            var testProducts = GetTestProducts();
            _mediator
                .Setup(m => m.Send(It.IsAny<GetSortedProductsQuery>(), default)).ReturnsAsync(testProducts);

            var mediatorObject = _mediator.Object;
            var controller = new ProductsController(mediatorObject);

            //Act
            var products = await controller.GetProducts(SortOrder.High);

            //Assert
            products.Should().NotBeNull();
            products.Should().HaveCount(2);
            _mediator.Verify(i => i.Send(It.Is<GetSortedProductsQuery>(arg => arg.SortOption == SortOrder.High), It.IsAny<CancellationToken>()), Times.Once, "Should Send Get Sorted Products Query to Mediator");
        }

        //Could use some nuget package like Bogus to create test objects 
        public IList<ProductDto> GetTestProducts()
        {
            return new List<ProductDto>
            {
                new ProductDto
                {
                    Quantity = 1,
                    Price = 10,
                    Name = "TestProductOne"
                },
                new ProductDto
                {
                    Quantity = 2,
                    Price = 15,
                    Name = "TestProductTwo"
                },
            };
        }
    }
}
