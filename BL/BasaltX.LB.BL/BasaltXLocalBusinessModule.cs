using BasaltX.Utils;
using Microsoft.Extensions.DependencyInjection;
using BasaltX.LB.BL.Features.Get.Implementation;
using BasaltX.LB.BL.Features.Get.Interfaces;

namespace BasaltX.LB.BL;

public static class BasaltXLocalBusinessModule
{
    private static bool _alreadyAdded;

    public static IServiceCollection AddLBModuleCollection(this IServiceCollection services)
    {
        if (_alreadyAdded) return services;

        //Register the utils services for consumption
        services.AddUtilitiesModuleCollection();
        services.AddSingleton<ILocalBusinessService, LocalBusinessService>();

        _alreadyAdded = true;

        return services;
    }
}
