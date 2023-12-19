using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.Roles;
using Microsoft.AspNetCore.Identity;
namespace LabWebAPI.Services.Validators
{
    public static class Validator
    {
        public static async Task<bool> IsUserEmailUnique(UserManager<User> manager, string email)
        {
            return await manager.FindByEmailAsync(email) == null;
        }
        public static async Task<bool> IsUniqueUserName(UserManager<User> manager, string username)
        {
            return await manager.FindByNameAsync(username) != null;
        }
        public static async Task<bool> IsAdmin(UserManager<User> manager, string userId)
        {
            var user = await manager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            return await manager.IsInRoleAsync(user, AuthorizationRoles.Admin.ToString());
        }
        public static async Task<bool> IsSystemRoleAndNoAdmin(RoleManager<IdentityRole> manager, AuthorizationRoles role)
        {
            return role == AuthorizationRoles.Admin ||
            await manager.FindByNameAsync(Enum.GetName(role)) == null;
        }
    }
}