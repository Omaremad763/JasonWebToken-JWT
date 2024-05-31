using jwt__dev_Creed.helper;
using jwt__dev_Creed.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace jwt__dev_Creed.services
{
    public class Auth_service : IAuth_service
    {
        private readonly UserManager<Application_user> _userManager;

        private readonly jwt _jwt;
        public Auth_service(UserManager<Application_user> userManager, IOptions<jwt> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async  Task<Auth_model> Gettoken_async(TokenRequest_model model)
        {
            var auth_model = new Auth_model();
            var user = await _userManager.FindByEmailAsync(model.E_mail);
            if (user == null|| ! await _userManager.CheckPasswordAsync(user, model.password))
            {

                auth_model.message = "Either E_mail or password is uncorrect";

                return auth_model;
            }

            var jwt_security_token = await CreateJwtToken(user);
            var roles_list = await _userManager.GetRolesAsync(user);
            
            auth_model.is_auth = true;
            auth_model.token=new JwtSecurityTokenHandler().WriteToken(jwt_security_token);
            auth_model.E_mail =  user.Email;
            auth_model.user_name = user.UserName;
            auth_model.Expiration_date = jwt_security_token.ValidTo;
            auth_model.roles = roles_list.ToList();
            return auth_model;
        }

        public async Task<Auth_model> register_async(register_model model)
        {
            if (await _userManager.FindByEmailAsync(model.E_mail) is not null)
                return new Auth_model { message = "email is already registered" };
           
            if (await _userManager.FindByNameAsync(model.E_mail) is not null)
                return new Auth_model { message = "Name is already registered" };

            var user = new Application_user
            {
                UserName = model.User_name,
                Email = model.E_mail,
                first_name = model.first_name,
                last_name = model.last_name,

            };
        
            var result= await _userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded) {

                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new Auth_model { message = errors };}

            await _userManager.AddToRoleAsync(user, "user");

            var jwt_security_token = await CreateJwtToken(user);

            return new Auth_model
            {
                E_mail = user.Email,
                Expiration_date = jwt_security_token.ValidTo,
                is_auth = true,
                roles = new List <string> { "User" },
                token=new JwtSecurityTokenHandler().WriteToken(jwt_security_token),
                user_name=user.UserName
            };

        }


        private async Task<JwtSecurityToken> CreateJwtToken(Application_user user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.Duration_days),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
