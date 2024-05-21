using api.Data;
using api.DTOs;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository_Impl : IStock_Repository

    {
          private readonly ApplicationDBcontext _context;

        public StockRepository_Impl(ApplicationDBcontext context){
            
             _context = context;
        }
        
        public async Task<Stock> CreateNewStock(Stock stock)
        {
             await _context.AddAsync(stock);
            await  _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStockById(int id)
        {
             Stock? stock = await  _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
              if(stock == null){
                return null;
            }

            _context.Remove(stock);
            await _context.SaveChangesAsync();
            return stock; 
           
        }

        public  Task<bool> DoesStockExists(int stockId)
        {
            return _context.Stocks.AnyAsync(s => s.Id == stockId);
        }

        public  async Task<List<Stock>> GetAllStocks(QueryObject query)
        {
            var stocks =   _context.Stocks.Include(x => x.Comments).ThenInclude(s => s.appUser).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName)){
                stocks = stocks.Where(s => s.CompanyName.Equals(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol)){
                stocks = stocks.Where(s => s.Symbol.Equals(query.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy)){
                
                if(query.SortBy.Equals("symbol",StringComparison.OrdinalIgnoreCase)){

                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy((s => s.Symbol));
                }

                if(query.SortBy.Equals("CompanyName",StringComparison.OrdinalIgnoreCase)){

                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy((s => s.CompanyName));
                }
            }

            var skipNumber = (query.PageNumber-1) * (query.PageSize); 

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            
        }

        public async Task<Stock?> GetBySymbol(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
            
        }

        public async Task<Stock?> GetStockById(int id)
        {
            return await  _context.Stocks.Include(x => x.Comments).ThenInclude(s => s.appUser).FirstOrDefaultAsync(x => x.Id == id);


        }

        public async Task<Stock?> UpdateStock(int id, UpdateStockRequestDTO stock)
        {
             var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null){
                return null;

            }

            stockModel.Symbol = stock.Symbol;
            stockModel.CompanyName = stock.CompanyName;
            stockModel.Purchase = stock.Purchase;
            stockModel.LastDiv = stock.LastDiv;
            stockModel.Industry = stock.Industry;
            stockModel.Marketkap = stock.Marketkap;

            await _context.SaveChangesAsync();
            return stockModel;
        }


      

       
        
    }
}