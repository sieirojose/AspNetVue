using AspNetVueApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetVueApp.Domain.Interfaces;


namespace AspNetVueApp.Application.Services
{
    public class CatFactService
    {
        private readonly HttpClient _httpClient;
        private readonly ICatFactRepository _catFactRepository;

        public CatFactService(HttpClient httpClient, ICatFactRepository catFactRepository)
        {
            _httpClient = httpClient;
            _catFactRepository = catFactRepository;
        }

        public async Task<string> GetRandomCatFactAsync()
        {
            var response = await _httpClient.GetAsync("https://catfact.ninja/fact");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CatFactResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (!string.IsNullOrWhiteSpace(result?.Fact))
                {
                    // Guardar el hecho en la base de datos
                    var catFactEntity = new CatFact
                    {
                        Fact = result.Fact,
                        SearchedOn = DateTime.UtcNow
                    };

                    await _catFactRepository.AddCatFactAsync(catFactEntity);

                    return result.Fact; // Retornar el hecho como respuesta
                }
                else
                {
                    throw new InvalidOperationException("Received a null or empty fact from the API.");
                }
            }

            return "Failed to fetch cat fact.";
        }

        // Clase auxiliar para deserializar el JSON
        private class CatFactResponse
        {
            public string Fact { get; set; }
        }
    }
}
