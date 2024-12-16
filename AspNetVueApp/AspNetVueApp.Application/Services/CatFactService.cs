using AspNetVueApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetVueApp.Application.Services
{
    public class CatFactService
    {
        private readonly HttpClient _httpClient;

        public CatFactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                return result?.Fact ?? "No fact found";
            }

            return "Failed to fetch cat fact.";
        }

        private class CatFactResponse
        {
            public string Fact { get; set; }
        }
    }
}