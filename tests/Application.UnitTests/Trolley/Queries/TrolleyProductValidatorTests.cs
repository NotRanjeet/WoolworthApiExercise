using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;
using Woolworth.Application.Products.Queries;
using Woolworth.Application.Trolley.Models;
using Woolworth.Application.Trolley.Queries;

namespace Woolworth.Application.UnitTests.Trolley.Queries
{
    //I have not attempted the full test coverage but demonstrating how we can test validators
    public class TrolleyProductValidatorTests
    {

        [Test]
        public void ValidTrolleyProductShouldPassValidation()
        {
            //Arrange

            var product = new TrolleyProductDto
            {
                Price = 10,
                Name = "TestProduct",
            };
            var validator = new TrolleyProductValidator();
            //Act
            var result = validator.TestValidate(product);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Test]
        public void InvalidProductNameShouldFailValidation()
        {
            //Arrange

            var product = new TrolleyProductDto
            {
                Price = 10,
                Name = "T",
            };
            var validator = new TrolleyProductValidator();
            //Act
            var result = validator.TestValidate(product);

            //Assert
            result.ShouldHaveValidationErrorFor(i=>i.Name);
        }

        [Test]
        public void NullProductNameShouldFailValidation()
        {
            //Arrange

            var product = new TrolleyProductDto
            {
                Price = 10,
                Name = null,
            };
            var validator = new TrolleyProductValidator();
            //Act
            var result = validator.TestValidate(product);

            //Assert
            result.ShouldHaveValidationErrorFor(i => i.Name);
        }

        [Test]
        public void EmptyProductNameShouldFailValidation()
        {
            //Arrange

            var product = new TrolleyProductDto
            {
                Price = 10,
                Name = "",
            };
            var validator = new TrolleyProductValidator();
            //Act
            var result = validator.TestValidate(product);

            //Assert
            result.ShouldHaveValidationErrorFor(i => i.Name);
        }

        [Test]
        public void InvalidProductPriceShouldFailValidation()
        {
            //Arrange

            var product = new TrolleyProductDto
            {
                Price = -10,
                Name = "Test Product",
            };
            var validator = new TrolleyProductValidator();
            //Act
            var result = validator.TestValidate(product);

            //Assert
            result.ShouldHaveValidationErrorFor(i => i.Price);
        }
    }
}
