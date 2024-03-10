﻿using Microsoft.IdentityModel.Tokens;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Services;
public sealed class JwtProvider :IJwtProvider
{
    public string CreateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("benim şifre anahtarım benim şifre anahtarım benim şifre anahtarım benim şifre anahtarım benim şifre anahtarım"));

        JwtSecurityToken jwtSecurityToken = new(
            issuer:"Hüseyin Serhan Kunt",
            audience:"Hüseyin Serhan Kunt",
            claims:null,
            notBefore:DateTime.Now,
            expires:DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler handler = new();
        string token = handler.WriteToken(jwtSecurityToken);

        return token;
    }
}
