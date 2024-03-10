using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Services;
public sealed class AuthService(
    UserManager<AppUser> userManager,
    IJwtProvider jwtProvider)
{
    public async Task<string> LoginAsync(LoginDto request)
    {
        AppUser? user =
            await userManager.Users
            .FirstOrDefaultAsync(p=>
            p.Email == request.UserNameOrEmail || 
            p.UserName == request.UserNameOrEmail);

        if (user == null)
        {
            throw new ArgumentException("Kullanıcı bulunamadı");
        }

        bool result = await userManager.CheckPasswordAsync(user, request.Password); 
        if(!result)
        {
            throw new ArgumentException("Şifre yanlış");
        }

        return jwtProvider.CreateToken();
    }
}
