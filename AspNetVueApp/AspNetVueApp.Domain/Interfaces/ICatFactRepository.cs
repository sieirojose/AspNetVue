using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetVueApp.Domain.Entities;

namespace AspNetVueApp.Domain.Interfaces
{
    public interface ICatFactRepository
    {
        Task AddCatFactAsync(CatFact catFact);
    }
}
