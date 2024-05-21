using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPortfolio_Repository
    {
        public Task<List<Stock>> GetUserPortfolio(AppUser user);

        public Task<Portfolio> createAsync(Portfolio portfolio);

        public Task<Portfolio?> DeleteAsync(AppUser appUser, string symbol);

        
    }
}