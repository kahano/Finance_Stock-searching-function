using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Service
{
    public interface IFMP_Service
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
        
    }
}