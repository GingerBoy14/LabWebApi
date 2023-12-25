using LabWebAPI.Contracts.ApiModels;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
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
    public interface IUserService
    {
        Task UploadAvatar(IFormFile avatar, string userId);
        Task<DownloadFile> GetUserImageAsync(string userId);
        Task<UserInfoDTO> GetUserProfileAsync(string id);
        Task<UserProfileDTO> EditUserProfileDataAsync(UserProfileDTO userDTO);
        
        Task DeleteUserProfileAsync(string userId);
        Task ChangeUserPassword(string userId, string currentPassword, string newPassword);
        Task<bool> ValidatePassword(string userId, string password);
    }
}
