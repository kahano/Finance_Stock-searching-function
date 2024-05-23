using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTOs.Stocks
{
    

    public record StockDTO(
        
           int Id,  
    
          [Required]
          [MaxLength(10,ErrorMessage = "Symbol cannot be over 10 characters")]
          string? Symbol,

          [Required]
         [MaxLength(10,ErrorMessage = "Symbol cannot be over 10 characters")]
          string? CompanyName, 

           [Required]
         [Range(1,1000000000)]
          decimal Purchase, 

          [Required]
         [Range(0.001,100)]
         decimal LastDiv,

         [Required]
         [MaxLength(10,ErrorMessage = "Symbol cannot be over 10 characters")]
            string? Industry ,

        [Range(1,5000000000)]
        long Marketkap,

        List<CommentDTO> Comments
          
        );
   
}