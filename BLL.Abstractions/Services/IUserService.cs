﻿using BLL.Entities;
using System.Linq.Expressions;

namespace BLL.Abstractions.Services
{
    public interface IUserService
    {
        Task<User> GetUserByCredentialsAsync(string login, string password);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
