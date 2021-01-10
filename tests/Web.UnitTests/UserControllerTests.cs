using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using Woolworth.Application.User.Queries;
using Woolworth.WebUI.Controllers;

namespace Woolworth.Web.UnitTests
{
    public class UserControllerTests
    {
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task GetUser_ShouldInvokeGetUserQuery()
        {
            //Arrange

            _mediator
                .Setup(m => m.Send(It.IsAny<GetUserQuery>(), default))
                .ReturnsAsync(new UserDto("name", "token")); //<-- return Task to allow await to continue


            var mediatorObject = _mediator.Object;
            var controller = new UserController(mediatorObject);

            //Act
            var user = await controller.GetUser();

            //Assert
            user.Should().NotBeNull();
            user.Name.Should().Be("name");
            user.Token.Should().Be("token");
            _mediator.Verify(i => i.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Once, "Should Send Get User Query to Mediator");

        }
    }
}
