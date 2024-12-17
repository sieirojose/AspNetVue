using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetVueApp.Domain.Interfaces;

namespace AspNetVueApp.Infrastructure.Jobs
{
    public class CatFactJob
    {
        private readonly ICatFactRepository _catFactRepository;

        public CatFactJob(ICatFactRepository catFactRepository)
        {
            _catFactRepository = catFactRepository;
        }

        public async Task ProcessCatFactsAsync()
        {
            try
            {
                var catFacts = await _catFactRepository.GetAllAsync();

                if (!catFacts.Any())
                {
                    Console.WriteLine("No cat facts to process.");
                    return;
                }
                else 
                { 
                    foreach (var fact in catFacts)
                    {
                        Console.WriteLine($"Fact: {fact.Fact}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}