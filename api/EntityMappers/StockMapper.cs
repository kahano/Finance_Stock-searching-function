using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.DTOs.Stocks;
using api.Models;

namespace api.EntityMappers
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stockModel){
            
            return new StockDTO(

                 stockModel.Id,
                  stockModel.Symbol,
               stockModel.CompanyName,
                stockModel.Purchase,
                stockModel.LastDiv,
               stockModel.Industry,
               stockModel.Marketkap,
                stockModel.Comments.Select(x => x.ToCommentDTO()).ToList()

                
            

            );
        }

        public static Stock ToStock(this CreateStockRequestDTO stockModel){

          
            return new Stock(){
             
               Symbol = stockModel.Symbol ,
               CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                Marketkap = stockModel.Marketkap,
                
            };
        

        }

        public static Stock ToStockFromRequestUpdateDTO(this UpdateStockRequestDTO stockModel){

          
            return new Stock(){
             
               Symbol = stockModel.Symbol ,
               CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                Marketkap = stockModel.Marketkap
            };
        

        }

        public static Stock ToStockFromFMPStock(this FMPStock stockModel){

          
            return new Stock(){
             
               Symbol = stockModel.symbol ,
               CompanyName = stockModel.companyName,
                Purchase =  (decimal)stockModel.price,
                LastDiv = (decimal)stockModel.lastDiv,
                Industry = stockModel.industry,
                Marketkap = stockModel.mktCap
            };
        

        }
    }
}