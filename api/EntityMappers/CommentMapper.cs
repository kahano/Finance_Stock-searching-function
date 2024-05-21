using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;

using api.Models;

namespace api.EntityMappers
{
    public static class CommentMapper
    {

        public static CommentDTO ToCommentDTO(this Comment comment){
            
            return new CommentDTO{

                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedDate = comment.CreatedDate,
                StockId = comment.StockId,
                CreatedBy = comment.appUser.UserName
              
            };
        }

        public static Comment FromCommentRequestDTO(this CommentRequestDTO comment, int stockId){

             return new Comment{

                Title = comment.Title,
                Content = comment.Content,
                StockId = stockId,
              
             
            };
        }

        public static Comment FromCommentRequesUpdatetDTO(this CommentRequestUpdateDTO comment){

             return new Comment{

           
                Title = comment.Title,
                Content = comment.Content,
             
               
            };
        }
        
        


    }
}