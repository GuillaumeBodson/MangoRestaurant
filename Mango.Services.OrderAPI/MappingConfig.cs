using AutoMapper;

namespace Mango.Services.OrderAPI
{
    public static class MappingConfig
    {
        private static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {

            });
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            IMapper mapper = RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
