using AutoMapper;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.Data;
using LabWebAPI.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabWebAPI.Contracts.DTO.Comment;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Product;
using LabWebAPI.Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LabWebAPI.Services.Services
{
    internal class CommentService:ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly UserManager<User> _userManager;

        public CommentService(IMapper mapper,
        IRepository<Product> productRepository,
        IRepository<Comment> commentRepository,
        UserManager<User> userManager)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _commentRepository = commentRepository;
            _userManager = userManager;
        }
        public async Task<CommentDTO> AddComment(SimpleCommentWithUserIdDTO model)
        {


                // Перевірте, чи користувач існує
                var user = await _userManager.FindByIdAsync(model.UserWhoCreatedId) ?? throw new UserNotFoundException("User not found");
               
                var product = await _productRepository.GetByKeyAsync(model.ProductId) ?? throw new ProductNotFoundException("Product not found!");

                // Створіть новий продукт
                var coment = new Comment
                {
                    Text = model.Text,
                    PublicationDate = model.PublicationDate,
                    UserWhoCreated = user, // Пов'язання з поточним користувачем
                    Product = product // Пов'язання з поточним продуктом
                };

            try { 
                // Додайте продукт до бази даних
                var result = await _commentRepository.AddAsync(coment);
                await _commentRepository.SaveChangesAsync();
                var mappedProduct = new CommentDTO()
                {
                    Id = result.Id,
                    Text = model.Text,
                    PublicationDate = product.PublicationDate,
                    ProductId = result.ProductId,
                    UserWhoCreatedId  = result.UserWhoCreatedId,
                    UserWhoCreated = new SimpleUserInfoWithAvatarDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Surname = user.Surname,
                        ImageAvatarUrl = user.ImageAvatarUrl
                    },
                    Product = new SimpleProductWithIdDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        PublicationDate= product.PublicationDate,
                    }
                };

              
                    return mappedProduct;
        }
                 catch (Exception ex)
            {
                throw new BadRequestException("Failed to create product");
            }


        }

        public async Task<IEnumerable<CommentDTO>> GetProductCommentsAsync(string productId)
        {

         
                var comments = await _commentRepository.Query()
                .Where(p=>p.ProductId== productId)
                .Include(p => p.UserWhoCreated)
                .Include(p => p.Product)
                .ToListAsync();

                var commentsInfo = comments.Select(comment =>
                {

                    return new CommentDTO()
                    {
                        Id = comment.Id,
                        Text = comment.Text,
                        PublicationDate = comment.PublicationDate,

                        ProductId = comment.ProductId,
                        UserWhoCreatedId = comment.UserWhoCreatedId,
                        UserWhoCreated = new SimpleUserInfoWithAvatarDTO
                        {
                            Id = comment.UserWhoCreated.Id,
                            Name = comment.UserWhoCreated.Name,
                            Email = comment.UserWhoCreated.Email,
                            Surname = comment.UserWhoCreated.Surname,
                            ImageAvatarUrl = comment.UserWhoCreated.ImageAvatarUrl
                        },
                        Product = new SimpleProductWithIdDTO
                        {
                            Id = comment.Product.Id,
                            Name = comment.Product.Name,
                            Description = comment.Product.Description,
                            Price = comment.Product.Price,
                            PublicationDate = comment.Product.PublicationDate,
                        }
                    };

                   
                })
                .ToList();
                return commentsInfo;
            
        }
    }
}
