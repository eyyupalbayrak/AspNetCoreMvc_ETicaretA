﻿using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Services
{
    public interface IAccountService
    {
        Task<string> CreateUserAsync(RegisterViewModel model);
        Task<string> FinByNameAsync(LoginViewModel model, List<CartLineViewModel> cartline);
        Task<UserViewModel> Find(string username);
        Task LogoutAsync();
        Task <UserViewModel> FindByIdAsync(int id);
        public void UpdateCartPrice(int cartId);
    }
}
