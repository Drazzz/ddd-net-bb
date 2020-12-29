using Microsoft.Extensions.Configuration;

namespace DDDNETBB.Persistence.EF.Extensions
{
    internal static class ConfigurationExtensions
    {
        internal static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section)
                .Bind(model);

            return model;
        }
    }    
}