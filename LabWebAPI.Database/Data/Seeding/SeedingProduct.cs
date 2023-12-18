﻿using LabWebAPI.Contracts.Data;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.Exceptions;
using LabWebAPI.Contracts.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Database.Data.Seeding
{
    public static class SeedingProduct
    {
        public static async Task BasicProduct(UserManager<User> userManager,
             IRepository<Product> productRepository)
        {


            var admins = await userManager.GetUsersInRoleAsync(AuthorizationRoles.Admin.ToString());
            if (admins != null && admins.Count > 0) {
                var admin = admins.First(); // Вибрати першого адміністратора
                var testProduct = new Product
                {
                    Name = "test",
                    Description = "test product",
                    Price = 500,
                    PublicationDate = DateTime.Now,
                    UserWhoCreatedId = admin.Id
                };
                await productRepository.AddAsync(testProduct);
                await productRepository.SaveChangesAsync();
            }
            else
            {
                throw new UserNotFoundException("No admin in system");
            }
        }
    }
}