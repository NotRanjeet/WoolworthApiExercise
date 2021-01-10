using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Woolworth.Application.Trolley.Models;
using Woolworth.Application.Trolley.Queries;

namespace Woolworth.Application.UnitTests.Trolley.Queries
{
    public class TrolleyQuantityValidatorTests
    {
        [Test]
        public void ValidTrolleyQuantityShouldPassValidation()
        {
            //Arrange
            var quantity = new TrolleyQuantityDto
            {
                Quantity = 10,
                Name = "TestProduct"
            };
            var validator = new TrolleyQuantityValidator();
            //Act
            var result = validator.TestValidate(quantity);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Test]
        public void InValidTrolleyQuantityShouldPassValidation()
        {
            //Arrange
            var quantity = new TrolleyQuantityDto
            {
                Quantity = -1,
                Name = "TestProduct"
            };
            var validator = new TrolleyQuantityValidator();
            //Act
            var result = validator.TestValidate(quantity);

            //Assert
            result.ShouldHaveValidationErrorFor(i=>i.Quantity);
        }
    }
}
