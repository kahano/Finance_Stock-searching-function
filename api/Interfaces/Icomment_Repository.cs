using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
     public interface IComment_Repository
    {
        Task<List<Comment>> GetAllComments(CommentQueryObject query);
        Task<Comment?> GetCommentById(int id);

        Task<Comment> CreateNewComment( Comment comment);

        Task<Comment?> UpdateComment(int id, Comment Comment);

        Task <Comment?> DeleteCommentById(int id);

        // Task<Comment?> GetCommentByStockId(int id);
    }
}