using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stocks;
using api.EntityMappers;
using api.Models;
using api.Service;
using Newtonsoft.Json;

namespace api.Repository
{
    public class FMP_ServiceImpl : IFMP_Service
    {
        
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        
        public FMP_ServiceImpl(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            
        }


        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try{

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}");

                if(result.IsSuccessStatusCode){
                    var contnet = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<List<FMPStock>>(contnet);
                    var stock = tasks.First();

                    if(stock != null){

                        return stock.ToStockFromFMPStock();
                    }
                    else{
                        return null;
                    }
                }
                return null;

            }catch(Exception e){
                System.Console.WriteLine(e);
                return null;

            }
        }
    }
}