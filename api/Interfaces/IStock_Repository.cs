using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Helpers;
using api.Models;


namespace api.Interfaces
{
    public interface IStock_Repository
    {
        Task<List<Stock>> GetAllStocks(QueryObject query);
        Task<Stock?> GetStockById(int id);

        Task<Stock> CreateNewStock( Stock stock);

        Task<Stock?> UpdateStock(int id, UpdateStockRequestDTO stock);

        Task <Stock?> DeleteStockById(int id);

        Task<bool> DoesStockExists(int stockid);

        Task<Stock?> GetBySymbol(string symbol);
        

    }
}