using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.EntityMappers;
using api.Extensions;
using api.Interfaces;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IStock_Repository _stock_Repository;
        private readonly IPortfolio_Repository _portfolio_Repository;

        private readonly IFMP_Service _fMP_Service;

        public PortfolioController(UserManager<AppUser> userManager,  IStock_Repository stock_Repository, 
        IPortfolio_Repository portfolio_Repository, IFMP_Service fMP_Service)
        {
            _userManager = userManager;
            _stock_Repository =   stock_Repository;
            _portfolio_Repository = portfolio_Repository;
            _fMP_Service = fMP_Service;
            
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetUserPortfolio(){

            var username = User.GetUsername();
            var appuser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolio_Repository.GetUserPortfolio(appuser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreatePortfolio(string symbol){

            var stock = await _stock_Repository.GetBySymbol(symbol);

            if(stock == null){

                stock = await _fMP_Service.FindStockBySymbolAsync(symbol);

                if(stock == null){
                    return BadRequest("stock is not found");
                }

                else{

                    await _stock_Repository.CreateNewStock(stock);
                }
            }

            var username = User.GetUsername();
            var appuser = await _userManager.FindByNameAsync(username);
            
            var userPortfolio = await _portfolio_Repository.GetUserPortfolio(appuser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            var portfolio = new Portfolio{

                StockId = stock.Id,
                AppUserId = appuser.Id
            };

            await _portfolio_Repository.createAsync(portfolio);
            return portfolio == null ?   StatusCode(500, "Could not create") : Created();

        }

        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeletePortfolio(string symbol){

             var username = User.GetUsername();
            var appuser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolio_Repository.GetUserPortfolio(appuser);
            var filteredStock = userPortfolio.Where(stock => stock.Symbol == symbol);

           

            if(filteredStock.Count() == 1){

                 await _portfolio_Repository.DeleteAsync(appuser,symbol);
            }

            else{
                return BadRequest("Stock is not found");
            }

            return Ok();

            

        }
        
        

    }
}