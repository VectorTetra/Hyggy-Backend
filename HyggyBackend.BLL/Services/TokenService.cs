﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using HyggyBackend.DAL.Entities.Employes;
using Microsoft.AspNetCore.Identity;

namespace HyggyBackend.BLL.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;
		private readonly UserManager<User> _userManager;
		private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
			_userManager = userManager;
		}
        public async Task<string> CreateToken(User user)
		{
			var userRoles = await _userManager.GetRolesAsync(user);
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.NameId, user.Id),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
			};

			claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds,
				Issuer = _config["JWT:Issuer"],
				Audience = _config["JWT:Audience"]
				
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
