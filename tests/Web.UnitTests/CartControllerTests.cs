using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using Woolworth.Application.Trolley.Models;
using Woolworth.Application.Trolley.Queries;
using Woolworth.WebUI.Controllers;

namespace Woolworth.Web.UnitTests
{
    public class CartControllerTests
    {
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task CalculateTotal_ShouldInvokeCalculateQuery()
        {
            //Arrange

            var query = new GetTrolleyTotalQuery(new TrolleyDto());

            _mediator
                .Setup(m => m.Send(It.IsAny<GetTrolleyTotalQuery>(), default(CancellationToken))).ReturnsAsync(100);

            var mediatorObject = _mediator.Object;
            var controller = new CartController(mediatorObject);
            var trolley = GetSampleTrolly();

            //Act
            var total = await controller.CalculateTotal(trolley);

            //Assert
            total.Should().Be(100);
            _mediator.Verify(
                i => i.Send(It.Is<GetTrolleyTotalQuery>(args =>
                args.Trolley.Products.Count() == trolley.Products.Count() &&
                args.Trolley.Quantities.Count() == trolley.Quantities.Count() &&
                args.Trolley.Specials.Count() == trolley.Specials.Count())
                , It.IsAny<CancellationToken>()), Times.Once, "Should Send Get Trolley Total Query to Mediator");
        }


        public TrolleyDto GetSampleTrolly()
        {
            return new TrolleyDto
            {
                Products = GetProducts(),
                Quantities = GetQuantities(),
                Specials = GetSpecialDtos()
            };
        }

        private List<TrolleyProductDto> GetProducts()
        {
            return new List<TrolleyProductDto> {
                new TrolleyProductDto
                {
                    Name ="Product One",
                    Price = 10
                },
                new TrolleyProductDto
                {
                    Name ="Product Two",
                    Price = 20
                },
            };
        }

        private List<TrolleyQuantityDto> GetQuantities()
        {
            return new List<TrolleyQuantityDto>
            {
                new TrolleyQuantityDto
                {
                    Name = "ProductOne",
                    Quantity = 20,
                },
                new TrolleyQuantityDto
                {
                    Name = "ProductTwo",
                    Quantity = 30
                }
            };
        }

        private List<TrolleySpecialDto> GetSpecialDtos()
        {
            return new List<TrolleySpecialDto>
            {
                new TrolleySpecialDto
                {
                    Quantities=GetQuantities(),
                    Total =10
                }
            };

        }
    }
}
