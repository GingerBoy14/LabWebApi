using LabWebAPI.Contracts.ApiModels;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Comment;
using LabWebAPI.Contracts.DTO.Product;
using LabWebAPI.Contracts.DTO.Profile;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Services
{
   
    public interface ICommentService
    {
        Task<CommentDTO> AddComment(SimpleCommentWithUserIdDTO model);
        Task<IEnumerable<CommentDTO>> GetProductCommentsAsync(string productId);
    }
}
