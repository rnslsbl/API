using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Others;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utility;

    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        //untuk dapat data dari appsettingjson. builder
        {
            _configuration = configuration;
        }

        public ClaimVM ExtractClaimsFromJwt(string token)
        {
            //extract token payload agar bisa dibaca
            if (token.IsNullOrEmpty()) return new ClaimVM();

            try
            {
                //configure the token validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, //true if we create an issuer
                    ValidateAudience = false, //true if we creatte an audience
                    ValidateLifetime = false, //cek expired token, kalau expired bakal direturn, validasi dari signaure jwt key
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
                };
                //parse and validate the JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                //token yang diinput, ngecek sama atau nggadari tokenValidation, setelah di cek membuat variable baru (outvar)
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                //extract the claims from the JWT Token
                //cek security token null atau nda, sesuai ngga sama claims identity
                if (securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    //mapping , extract value. role ngga karena berbentuk array string
                    //di extract duulu baru di isi role nya(biasa lebih dari 1)
                    var claims = new ClaimVM
                    {
                        NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                        Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                        Email = identity.FindFirst(ClaimTypes.Email)!.Value
                    };
                    var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList();
                    claims.Roles = roles;

                    //return dalam bentuk claims
                    return claims;
                }
            }
            catch
            {
                return new ClaimVM();
            }
            return new ClaimVM();
        }


        public string GenerateRefreshToken()

        //apakah anda masih mau berada di web ini?
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        //pakai List karena mengambil payload lebih dari 1
        {
            //key harus dalam bentuk byte, panjang 256
            var secretKey = new SymmetricSecurityKey(Encoding
                                                       .UTF8
                                                       .GetBytes(_configuration["JWT:Key"]));

            //enkripsi pakai hmcasha256
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            //membentuk pola dari payload, petakan bentuk dari JWT. login ulang setelah 10 menit
            var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWT:Issuer"],
                                                    audience: _configuration["JWT:Audience"],
                                                    claims: claims,
                                                    expires: DateTime.Now.AddMinutes(10),
                                                    signingCredentials: signinCredentials);//dikunci dengan signing credentials

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString; //dalam bentuk string
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            //tokennya di extract
            throw new NotImplementedException();
        }

  
}

