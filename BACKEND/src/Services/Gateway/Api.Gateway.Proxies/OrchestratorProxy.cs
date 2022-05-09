using Api.Gateway.Models.seeker.Commands;
using Api.Gateway.Models.seeker.DTOs;
using Api.Gateway.Services.BusinessLogic.Commands;
using Api.Gateway.Services.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies
{
    public interface IOrchestratorProxy
    {
        Task<List<CasCreateCommand>> GetOnlySeeker(CasSeekerCommandProxies command);
        Task<CaRepositoryDto> GetAsync(int id);
        Task CreateAsync(CasCreateCommand command);
        Task<CasCreateCommand> CAsLogic(CasSeekerCommandProxies command);
        Task<CasOptLogic> Dictionary(DictionaryModel command);
    }

    public class OrchestratorProxy : IOrchestratorProxy
    {
        // inyeccicon dependencias para los objetos URL y http
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;
        public OrchestratorProxy(
            HttpClient httpClient,
            IOptions<ApiUrls> apiUrls,
            IHttpContextAccessor httpContextAccessor
            )
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CasCreateCommand>> GetOnlySeeker(CasSeekerCommandProxies command)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );

            var request1 = await _httpClient.PostAsync($"{_apiUrls.SeekerUrl}", content);
            string auxJson = await request1.Content.ReadAsStringAsync();
            var jsonSeeker = JsonConvert.DeserializeObject<List<CasCreateCommand>>(auxJson);
            return await Task.FromResult(jsonSeeker);
        }

        public async Task<CaRepositoryDto> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SeekerUrl}Cas/{id}");
            request.EnsureSuccessStatusCode();

            return System.Text.Json.JsonSerializer.Deserialize<CaRepositoryDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task CreateAsync(CasCreateCommand command)
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );

            var request = await _httpClient.PostAsync($"{_apiUrls.SeekerUrl}Cas", content);
            request.EnsureSuccessStatusCode();
        }
        public async Task<CasCreateCommand> CAsLogic(CasSeekerCommandProxies command)
        {

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );
            var request1 = await _httpClient.PostAsync($"{_apiUrls.SeekerUrl}", content);
            string auxJson = await request1.Content.ReadAsStringAsync();
            var jsonSeeker = JsonConvert.DeserializeObject<List<CasCreateCommand>>(auxJson);

            if (jsonSeeker.Count < 1)
            {
                return null;
            }
            else
            {
                if (jsonSeeker[0].Alphabet == command.Alphabet && jsonSeeker[0].Columns == command.Columns)
                {
                    var varDictionary = new DictionaryModel()
                    {
                        Columns = Int32.Parse(jsonSeeker[0].Columns.ToString()),
                        Strength = Int32.Parse(jsonSeeker[0].Strength.ToString()),
                        Alphabet = jsonSeeker[0].Alphabet.ToString(),
                        TarjetAlphabet = command.Alphabet_user.ToString(),
                        Rows = Int32.Parse(jsonSeeker[0].Rows.ToString()),
                        CA_notes = jsonSeeker[0].CA_notes.ToString()
                    };

                    var req2 = await Dictionary(varDictionary);

                    jsonSeeker[0].CA_notes = req2.CA_notes;


                    return jsonSeeker[0];
                }
                else
                {
                    dynamic jsonObj = JsonConvert.DeserializeObject(auxJson);
                    var commandOpt = new CasOptCommandSeeker()
                    {
                        CAID = Int32.Parse(jsonObj[0]["caid"].ToString()),
                        Columns = Int32.Parse(jsonObj[0]["columns"].ToString()),
                        Strength = command.Strength,
                        //Strength = Int32.Parse(jsonObj[0]["strength"].ToString()),
                        Alphabet = jsonObj[0]["alphabet"].ToString(),
                        Rows = Int32.Parse(jsonObj[0]["rows"].ToString()),
                        CA_notes = jsonObj[0]["cA_notes"].ToString(),

                        TarjetAlphabet = command.Alphabet.ToString(),
                        TarjetColumns = command.Columns,
                    };

                    //DictionaryModel com = new DictionaryModel();
                    //com.Columns = 


                    var contentOpt = new StringContent(
                     System.Text.Json.JsonSerializer.Serialize(commandOpt),
                    Encoding.UTF8,
                    "application/json"
                    );

                    var request2 = await _httpClient.PostAsync($"{_apiUrls.OptimizerUrl}Optimizer", contentOpt);
                    string auxJson2 = await request2.Content.ReadAsStringAsync();
                    var jsonOpt2 = new CasCreateCommand();
                    var varDictionary = new DictionaryModel();
                    if (auxJson2 == "")
                    {

                        varDictionary.Columns = Int32.Parse(jsonSeeker[0].Columns.ToString());
                        varDictionary.Strength = Int32.Parse(jsonSeeker[0].Strength.ToString());
                        varDictionary.Alphabet = jsonSeeker[0].Alphabet.ToString();
                        varDictionary.TarjetAlphabet = command.Alphabet_user.ToString();
                        varDictionary.Rows = Int32.Parse(jsonSeeker[0].Rows.ToString());
                        varDictionary.CA_notes = jsonSeeker[0].CA_notes.ToString();
                    }
                    else
                    {
                        jsonOpt2 = JsonConvert.DeserializeObject<CasCreateCommand>(auxJson2);
                        varDictionary.Columns = Int32.Parse(jsonOpt2.Columns.ToString());
                        varDictionary.Strength = Int32.Parse(jsonOpt2.Strength.ToString());
                        varDictionary.Alphabet = jsonOpt2.Alphabet.ToString();
                        varDictionary.TarjetAlphabet = command.Alphabet_user.ToString();
                        varDictionary.Rows = Int32.Parse(jsonOpt2.Rows.ToString());
                        varDictionary.CA_notes = jsonOpt2.CA_notes.ToString();
                        
                    }

                    var req2 = await Dictionary(varDictionary);
                    jsonOpt2.Alphabet = req2.TarjetAlphabet;
                    jsonOpt2.CA_notes = req2.CA_notes;

                    //return jsonSeeker[0];

                    //var jsonOpt = JsonConvert.DeserializeObject<List<CasCreateCommand>>(auxJson2);
                    return await Task.FromResult(jsonOpt2);

                }
            }

        }

        
        public async Task<CasOptLogic> Dictionary(DictionaryModel command)
        {


            String[] Alphabet = command.TarjetAlphabet.Split('/');
            String[] Ca_notes = command.CA_notes.Split('\n');
            var aux = 0;

            String concatenado = "";
            Dictionary<int, string> nums = new Dictionary<int, string>();

            for (int i = 0; i < Ca_notes.Length - 1; i++)
            {
                //String[] Vatiable_User = Alphabet[i].Split(',');
                String[] Vatiable = Ca_notes[i].Split(' ');

                for (int K = 0; K < command.Columns; K++)
                {
                    String[] Vatiable_User = Alphabet[K].Split(',');
                    String[] Vatiable2 = Ca_notes[K].Split(' ');

                    for (int j = 0; j < Vatiable_User.Length; j++)
                    {

                        nums.Add(j, Vatiable_User[j]);
                    }
                    if (K < command.Columns - 1)
                    {
                        concatenado = concatenado + nums[Int32.Parse(Vatiable[K])] + ",";
                        nums.Clear();
                    }
                    else
                    {
                        concatenado = concatenado + nums[Int32.Parse(Vatiable[K])];
                        nums.Clear();
                    }
                }
                if (i < Ca_notes.Length - 2)
                {
                    concatenado = concatenado + "/";
                }
            }

            var commandOpt = new CasOptLogic()
            {
                Columns = command.Columns,
                Strength = command.Strength,
                Alphabet = command.TarjetAlphabet,
                TarjetAlphabet = command.TarjetAlphabet,
                Rows = command.Rows,
                CA_notes = concatenado,
            };


            return await Task.FromResult(commandOpt);
        }

  
    }
}