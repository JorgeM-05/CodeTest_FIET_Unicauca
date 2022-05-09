using AppConsole.RabbitMQ.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppConsole.RabbitMQ.Controllers
{
    public interface ISeekerProxy
    {
        Task Send(CasCreate command);
    }
    public class ControllerRabbitToGenerator: ISeekerProxy
    {
        private readonly HttpClient _httpClient;


        public ControllerRabbitToGenerator(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpClient = httpClient;
        }

        public async Task Send(CasCreate command)
        { 
            try
            {
                //var command = 2 + 2;
                var content = new StringContent(
                  JsonSerializer.Serialize(command),
                  Encoding.UTF8,
                  "application/json"
              );

                var request2 = await _httpClient.PostAsync($"http://localhost:50810/", content);
                var request = await _httpClient.GetAsync($"http://localhost:50810/");
                Console.WriteLine(request2);
                request.EnsureSuccessStatusCode();
            }
            catch(Exception e)
            {
                throw new NotImplementedException();
            }
           
        }

    }

   
}
