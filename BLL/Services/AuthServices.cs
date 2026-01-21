using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Jwt;
using DAL.EF.Models;
using DAL.Repos;
using System.Security.Cryptography;
using BCrypt.Net;
using DAL;

namespace BLL.Services
{
    public class AuthService
    {
        DataAccessFactory factory;
        JwtService jwt;

        public AuthService(DataAccessFactory factory, JwtService jwt)
        {
            this.factory = factory;
            this.jwt = jwt;
        }

        public bool Register(RegistrationDTO dto)
        {
            var user = new User
            {
                UserName = dto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
            };
            return factory.G_UserRepository().Add(user);
        }

        public string Login(LoginDTO dto)
        {
            var user = factory.S_UserRepo().GetByUserName(dto.UserName);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return null;

            return jwt.GenerateToken(user.UserName, user.Role);
        }
    }
}

