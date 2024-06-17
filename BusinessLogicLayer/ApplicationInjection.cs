using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication
            (this IServiceCollection services)
        {
            services
               // .ValidationConfigure()
                .ServicesConfigure();

            return services;
        }

        //private static IServiceCollection ValidationConfigure(this IServiceCollection services)
        //{
        //    services
        //        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
        //        .AddFluentValidationAutoValidation(cfg =>
        //        {
        //            cfg.DisableBuiltInModelValidation = true;
        //            cfg.ValidationStrategy = ValidationStrategy.All;
        //            cfg.EnableCustomBindingSourceAutomaticValidation = true;
        //            cfg.OverrideDefaultResultFactoryWith<CustomValidationResultFactory>();
        //        });

        //    return services;
        //}
        private static IServiceCollection ServicesConfigure(this IServiceCollection services)
        {
            //services
            //    .AddScoped<IServiceService, ServiceService>()

            return services;
        }
    }
}
