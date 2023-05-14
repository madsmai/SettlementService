using System.Reflection;
using AutoFixture.Kernel;

namespace Settlements.Test.UnitTests;
public class BookingRequestDtoSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var pi = request as ParameterInfo;
        if (pi == null)
        {
            return new NoSpecimen();
        }

        if (pi.ParameterType != typeof(string)
            || pi.Name != "bookingTime")
        {
            return new NoSpecimen();
        }

        var hours = new Random().Next(9, 16);
        var minutes = new Random().Next(60);
        return $"{hours}:{minutes}";
    }
}
