using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository_Impl : IComment_Repository
    {
          private readonly ApplicationDBcontext _context;
          public CommentRepository_Impl(ApplicationDBcontext context){
            
                _context = context;
          }

        public async Task<Comment> CreateNewComment(Comment comment)
        {
            await  _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteCommentById(int id)
        {
              var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
              if(comment == null){
                return null;
              }

              _context.Comments.Remove(comment);
              await _context.SaveChangesAsync();
              return comment;
        }

        public async Task<List<Comment>> GetAllComments(CommentQueryObject query) // todo
        {
            var comments =  _context.Comments.Include(s => s.appUser).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.symbol)){
                comments = comments.Where(s => s.Stock.Symbol == query.symbol);
            };

            if(query.IsDescending){
                

                comments = comments.OrderByDescending(s => s.CreatedDate);
            }
            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            var comment = await _context.Comments.Include(s => s.appUser).FirstOrDefaultAsync(x => x.Id == id);
            return comment;
        }

  
        public async Task<Comment?> UpdateComment(int id, Comment comment)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(s => s.Id == id);
            if(commentModel == null){
                return null;
            }

            
         
            commentModel.Title = comment.Title;
            commentModel.Content = comment.Content;

            await _context.SaveChangesAsync();
            return commentModel;

        }
    }
}