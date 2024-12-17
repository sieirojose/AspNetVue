using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetVueApp.Domain.Entities;
using AspNetVueApp.Domain.Interfaces;
using AspNetVueApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AspNetVueApp.Infrastructure.Repositories
{
    public class CatFactRepository : ICatFactRepository
    {
        private readonly ApplicationDbContext _context;

        public CatFactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CatFact>> GetAllAsync()
        {
            return await _context.CatFacts.ToListAsync();
        }

        public async Task AddCatFactAsync(CatFact catFact)
        {
            _context.CatFacts.Add(catFact);
            await _context.SaveChangesAsync();
        }
    }
}
