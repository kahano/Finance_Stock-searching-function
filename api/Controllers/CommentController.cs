using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using api.EntityMappers;
using api.Models;
using api.DTOs;
using api.Helpers;
using Microsoft.AspNetCore.Identity;
using api.Extensions;
using api.Service;
using Microsoft.AspNetCore.Authorization;


namespace api.Controllers
{
    
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase

    {
        private readonly IComment_Repository _comment_Repository;
        private readonly IStock_Repository _stock_Repository;

        private readonly UserManager<AppUser> _userManager;

        private readonly ApplicationDBcontext _context;

        private readonly IFMP_Service _fMP_Service;

        public CommentController(ApplicationDBcontext context, IComment_Repository comment_Repository 
        , IStock_Repository stock_Repository,UserManager<AppUser> userManager, IFMP_Service fMP_Service){

            _context = context;
            _comment_Repository = comment_Repository;
            _stock_Repository = stock_Repository;
            _userManager = userManager;
            _fMP_Service = fMP_Service;
        }

        [HttpGet]
        [Authorize]
    
        public async Task<IActionResult> GetAllCommentsAsync([FromQuery] CommentQueryObject query){

            if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
            var comments = await _comment_Repository.GetAllComments(query);
            var commentlist = comments.Select(comment => comment.ToCommentDTO());
            return Ok(commentlist);
        }

       

        [HttpPost]
         [Route("{symbol:alpha}")]
        
        public async Task<IActionResult> CreateComment(CommentRequestDTO comment, [FromRoute] string symbol){
            
            if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
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
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = comment.FromCommentRequestDTO(stock.Id);
            commentModel.AppUserId = appUser.Id;
            await _comment_Repository.CreateNewComment(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDTO());

        }

       
        [HttpGet("{id:int}")]
        
        public async Task<IActionResult> GetById([FromRoute]int id){


              if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
            var comment =  await _comment_Repository.GetCommentById(id);
            if(comment == null){
                return NotFound();
            }
            return Ok(comment.ToCommentDTO());
        }

        [HttpDelete("{id:int}")]

        public async Task<IActionResult> DeleteById([FromRoute] int id){

              if(!ModelState.IsValid){
                
                return BadRequest(ModelState);
            }
            var comment = await _comment_Repository.DeleteCommentById(id);
            if(comment == null){
                return NotFound();
            }
            return NoContent();

        }


         [HttpPut]
         [Route("{id:int}")]

        public async Task<IActionResult> Update([FromBody] CommentRequestUpdateDTO commentdto , [FromRoute]int id){


                if(!ModelState.IsValid){
                
                    return BadRequest(ModelState);
                }
                var comment =  await _comment_Repository.UpdateComment(id,commentdto.FromCommentRequesUpdatetDTO());
               
                if(comment is null){
                    return NotFound("comment doesn't exist");
                }
                return Ok(comment.ToCommentDTO());           

        }

       


        


    }
}