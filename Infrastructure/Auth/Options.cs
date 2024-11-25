using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Infrastructure.Auth
{
    public class Options : IConfigureOptions<Options>
    {
        private readonly IConfiguration _configuration;
        public Options(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(Options options)
        {
            _configuration.GetSection("JWT").Get<Options>();
        }
    }
}