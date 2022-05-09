using Microsoft.Extensions.Options;
using Seeker.Services.Proxies.Generator.Commands;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Seeker.Services.Proxies.Generator
{
    public interface ISeekerProxy
    {
        Task SendCasToOptAsync(CasSeekerToOptCommandProxies command);
        Task SendCasToGeneratorAsync(CasSeekerCommandProxies command);
        Task SendCasToGeneratorSwitch(CasSeekerSwithCommandProxies command);
    }

    public class SeekerProxy : ISeekerProxy
    {
        // inyeccicon dependencias para los objetos URL y http
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;
        public SeekerProxy(
            HttpClient httpClient,
            IOptions<ApiUrls> apiUrls
            )
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        // para enviar a generador
        public async Task SendCasToGeneratorAsync(CasSeekerCommandProxies command2)
        {
            // generar el Json
            var content = new StringContent(
                           JsonSerializer.Serialize(command2),
                           Encoding.UTF8,
                           "application/json"
                       );

            //enviamos json por protocolo http 
            var request = await _httpClient.PostAsync(_apiUrls.GeneratorUrl, content);
            request.EnsureSuccessStatusCode();
        }

        public async Task SendCasToGeneratorSwitch(CasSeekerSwithCommandProxies command3)
        {
            // generar el Json
            var content = new StringContent(
                           JsonSerializer.Serialize(command3),
                           Encoding.UTF8,
                           "application/json"
                       );

            //enviamos json por protocolo http 
            var request = await _httpClient.PostAsync(_apiUrls.GeneratorUrl, content);
            request.EnsureSuccessStatusCode();
        }

        // para enviar a optimizador
        public async Task SendCasToOptAsync(CasSeekerToOptCommandProxies command)
        {
            // generar el Json
            var content = new StringContent(
                           JsonSerializer.Serialize(command),
                           Encoding.UTF8,
                           "application/json"
                       );

            //enviamos json por protocolo http 
            var request = await _httpClient.PostAsync(_apiUrls.OptimizerUrl, content);
            request.EnsureSuccessStatusCode();
        }
    }
}

