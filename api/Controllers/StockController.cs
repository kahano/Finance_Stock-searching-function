using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.EntityMappers;
using api.Helpers;
using api.Interfaces;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
       

        private readonly IStock_Repository _stockRepository_Impl;

        private readonly ApplicationDBcontext _context;

        public StockController(ApplicationDBcontext context, IStock_Repository stockRepository_Impl)
        {
      
            _stockRepository_Impl = stockRepository_Impl;
             _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query){

              if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
            var stocks =  await _stockRepository_Impl.GetAllStocks(query);
            var stockdto = stocks.Select(x => x.ToStockDTO()).ToList();
            return Ok(stockdto);
        }

        [HttpGet("{id:int}")]
   
        public async Task<IActionResult> GetById([FromRoute]int id){

              if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
            var stock  = await _stockRepository_Impl.GetStockById(id);
            if(stock == null){
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stock){

              if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
            var stockModel = stock.ToStock();
            await _stockRepository_Impl.CreateNewStock(stockModel);
             return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());
        }

        [HttpPut("{id:int}")]

        public async Task<IActionResult> Update ([FromRoute] int id, [FromBody] UpdateStockRequestDTO stock){

              if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepository_Impl.UpdateStock(id,stock);
          
            return Ok(stockModel);



        }

        [HttpDelete("{id:int}")]
      

        public async Task<IActionResult> Delete([FromRoute]int id){

              if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
             var stockModel = await _stockRepository_Impl.DeleteStockById(id);  
             if(stockModel == null){
                return NotFound();
             }   

            return NoContent();
        }






       
    }
}