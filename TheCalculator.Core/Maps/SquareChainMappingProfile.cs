using System.Globalization;
using AutoMapper;
using TheCalculator.Domain.Dtos;
using TheCalculator.Domain.ViewModels;

namespace TheCalculator.Core.Maps;

/// <summary>
///     Mapping profiles for <see cref="SquareChainDto" />
///     and <see cref="SquareChainVm" />.
/// </summary>
public class SquareChainMappingProfile : Profile
{
    public SquareChainMappingProfile()
    {
        this.CreateMap<SquareChainDto, SquareChainVm>()
            .ForMember(dst => dst.Origin,
                opt => opt.MapFrom(src => ConvertOrigin(src.Origin)));
    }

    /// <summary>
    ///     Converts provided <paramref name="values" />
    ///     to string representation where each value is delimited by ','.
    /// </summary>
    /// <param name="values">Value array to make a string.</param>
    /// <returns>Returns string representation of provided array.</returns>
    private static string ConvertOrigin(double[] values)
    {
        if (!values.Any()) return string.Empty;

        return values.Select(x => x.ToString(CultureInfo.InvariantCulture))
            .Aggregate((l, r) => $"{l}, {r}");
    }
}
