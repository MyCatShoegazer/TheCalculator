using AutoMapper;
using TheCalculator.Core.Options;
using TheCalculator.ViewModels;

namespace TheCalculator.Core.Maps;

/// <summary>
///     Contains mappings for <see cref="VersionOptions" />.
/// </summary>
public class VersionOptionsMappingProfile : Profile
{
    public VersionOptionsMappingProfile()
    {
        this.CreateMap<VersionOptions, VersionVm>()
            .ConstructUsing((options, _) => new VersionVm
            {
                Version =
                    $"{options.Number},{options.Build} [{options.Branch}]"
            });
    }
}
