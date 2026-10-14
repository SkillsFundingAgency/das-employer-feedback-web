using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.EmployerFeedback.Infrastructure.Configuration;
using SFA.DAS.Encoding;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.EmployerFeedback.Web.StartupExtensions
{
    [ExcludeFromCodeCoverage]
    public static class AddConfigurationOptionsExtension
    {
        public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<EmployerFeedbackWebConfiguration>(configuration.GetSection(nameof(EmployerFeedbackWebConfiguration)));
            services.AddSingleton(cfg => cfg.GetService<IOptions<EmployerFeedbackWebConfiguration>>().Value);
            services.Configure<EmployerFeedbackOuterApiConfiguration>(configuration.GetSection(nameof(EmployerFeedbackOuterApiConfiguration)));
            services.AddSingleton(cfg => cfg.GetService<IOptions<EmployerFeedbackOuterApiConfiguration>>().Value);
            var dasEncodingConfig = new EncodingConfig { Encodings = [] };
            configuration.GetSection(nameof(dasEncodingConfig.Encodings)).Bind(dasEncodingConfig.Encodings);
            services.AddSingleton(dasEncodingConfig);
        }
    }
}