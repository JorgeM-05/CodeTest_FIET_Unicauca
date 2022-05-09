using Generator.Services.Proxies.Seeker.Commands;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Generator.Services.Proxies.Seeker
{
    // Interfaz inyeccion dependencias 
    public interface ISeekerProxy
    {
        Task SendCasAsync(CasCreateCommandSeeker command);
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

        //
        public async Task SendCasAsync(CasCreateCommandSeeker command)
        {
            // generar el Json
            var content = new StringContent(
                           JsonSerializer.Serialize(command),
                           Encoding.UTF8,
                           "application/json"
                       );

            //enviamos json por protocolo http
            var request = await _httpClient.PostAsync(_apiUrls.SeekerUrl + "Cas", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
