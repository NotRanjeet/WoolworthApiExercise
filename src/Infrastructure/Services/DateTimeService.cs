using System;
using Woolworth.Application.Common.Interfaces;

namespace Woolworth.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
