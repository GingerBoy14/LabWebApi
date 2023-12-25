using AutoMapper;
using LabWebAPI.Contracts.ApiModels;
using LabWebAPI.Contracts.Data;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Profile;
using LabWebAPI.Contracts.Exceptions;
using LabWebAPI.Contracts.Helpers;
using LabWebAPI.Contracts.Roles;
using LabWebAPI.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Services.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        IRepository<Product> _productRepository;
        private readonly IOptions<ImageSettings> _imageSettings;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager,
        IMapper mapper,
        IFileService fileService,
        IOptions<ImageSettings> imageSettings,
        SignInManager<User> signInManager,
           IRepository<Product> productRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _fileService = fileService;
            _imageSettings = imageSettings;
            _productRepository = productRepository;
            _signInManager = signInManager;
        }
        public async Task UploadAvatar(IFormFile avatar, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            string newPath = await _fileService.AddFileAsync(avatar.OpenReadStream(), _imageSettings.Value.Path, avatar.FileName);
            if (user.ImageAvatarUrl != null)
            {
                await _fileService.DeleteFileAsync(user.ImageAvatarUrl);
            }
            user.ImageAvatarUrl = newPath;
            await _userManager.UpdateAsync(user);
        }
        public async Task<DownloadFile> GetUserImageAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            _ = user.ImageAvatarUrl ?? throw new NotFoundException("Image not found.");
            var file = await _fileService.GetFileAsync(user.ImageAvatarUrl);
            return file;
        }
        public async Task<UserInfoDTO> GetUserProfileAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException("User not found!");
            var model = new UserInfoDTO();
            _mapper.Map(user, model);
            model.Role = Enum.Parse<AuthorizationRoles>(await GetUserRoleAsync(user));
            return model;
        }
        public async Task<UserProfileDTO> EditUserProfileDataAsync(UserProfileDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.Id) ?? throw new UserNotFoundException("User not found!");
            var userName = await _userManager.FindByNameAsync(model.UserName);
            if (userName != null && userName.Id != model.Id)
            {
                throw new UserAlreadyExistsException("Username");
            }
            var userEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userEmail != null && userEmail.Id != model.Id)
            {
                throw new UserAlreadyExistsException("Email");
            }
        
            _mapper.Map(model, user);
            await _userManager.UpdateAsync(user);
            
            return model;
        }
        public async Task DeleteUserProfileAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException("User not found!");
            await _userManager.RemoveFromRoleAsync(user, await GetUserRoleAsync(user));

            var deleteResult = await _userManager.DeleteAsync(user);
            if (deleteResult.Succeeded)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task ChangeUserPassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException("User not found!");
            var isValidPassword = await _userManager.CheckPasswordAsync(user, currentPassword);

            if (!isValidPassword)
            {
                throw new OldPasswordInvalidException();
            }
            var changePasswordResult =   await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!changePasswordResult.Succeeded)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (var error in changePasswordResult.Errors)
                {
                    errorMessage.Append(error.Description.ToString() + " ");
                }
                throw new BadRequestException(errorMessage.ToString());
            }
            if (changePasswordResult.Succeeded)
            {
                // Optionally, sign the user back in with the new password
                await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }

        public async Task<bool> ValidatePassword(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException("User not found!");

            return await _userManager.CheckPasswordAsync(user, password);

        }

        private async Task<string> GetUserRoleAsync(User user)
        {
            return await Task.FromResult(_userManager.GetRolesAsync(user).Result.FirstOrDefault());
        }
    }
}
