using Xmu.Crms.Services.ViceVersa;
using Xmu.Crms.Shared.Service;

namespace Microsoft.Extensions.DependencyInjection
{ 
    public static class ViceVersaExtensions
    {
        // 为每一个你写的Service写一个这样的函数，把 UserService 替换为你实现的 Service
        public static IServiceCollection AddViceVersaUserService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IUserService, UserService>();
        }
    }
}
