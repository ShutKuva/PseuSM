using BLL.Entities;
using Core;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using DAL.Abstractions.UnitOfWork;
using DALUser = DAL.Entities.User;
using System.Linq.Expressions;
using DAL.Abstractions.Repository;
using BLL.Services.BaseServices;
using AutoMapper;
using BLL.Abstractions.Services;

namespace BLL.Services
{
    public class JwtUserService : ServiceBase<DALUser>, IJwtService
    {
        private readonly JwtProperties _jwtProperties;
        private readonly IMapper _mapper;

        public JwtUserService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<JwtProperties> jwtPropertiesOptions) : base(unitOfWork)
        {
            _jwtProperties = jwtPropertiesOptions.Value ?? throw new ArgumentNullException(nameof(jwtPropertiesOptions));
            _mapper = mapper;
        }

        public Task<string> GenerateAccessTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(UserClaimsConstants.Id, user.Id.ToString()),
                new Claim(UserClaimsConstants.Name, user.Name!),
            };

            return GetJwtTokenAsync(claims);
        }

        public Task<string> GenerateRefreshTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(UserClaimsConstants.Id, user.Id.ToString()),
                new Claim(UserClaimsConstants.Name, user.Name!),
            };

            return GetJwtTokenAsync(claims);
        }

        private Task<string> GetJwtTokenAsync(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtProperties.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtProperties.Issuer,
                audience: _jwtProperties.Audience,
                claims: claims,
                signingCredentials: credentials
                );

            var handler = new JwtSecurityTokenHandler();

            return Task.FromResult(handler.WriteToken(token));
        }
    }
}
