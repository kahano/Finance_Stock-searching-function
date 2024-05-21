using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository_Impl : IPortfolio_Repository

    {
        // private readonly UserManager<AppUser> _userManager;
        // private readonly IStock_Repository _stock_Repository;
        private readonly ApplicationDBcontext _context;

        public PortfolioRepository_Impl(ApplicationDBcontext context){
           
           _context = context;
           
        }

        public async Task<Portfolio> createAsync(Portfolio portfolio)
        {
             await _context.AddAsync(portfolio);
             await _context.SaveChangesAsync();
             return portfolio;
        }

        public async Task<Portfolio?> DeleteAsync(AppUser appUser , string symbol)
        {
             var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Stock.Symbol == symbol);
             if (portfolioModel == null){
                return null;
             }

             _context.Remove(portfolioModel);
             await _context.SaveChangesAsync();
             return portfolioModel;

            
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await  _context.Portfolios.Where(s => s.AppUserId == user.Id)
                    .Select(stock => new Stock{

                        Id = stock.Stock.Id,
                        Symbol = stock.Stock.Symbol,
                        CompanyName = stock.Stock.CompanyName,
                        Purchase = stock.Stock.Purchase,
                        LastDiv =  stock.Stock.LastDiv,
                        Industry = stock.Stock.Industry, 
                        Marketkap = stock.Stock.Marketkap



                    }).ToListAsync();
        }
    }
}