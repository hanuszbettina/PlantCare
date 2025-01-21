using Microsoft.AspNetCore.Authorization;//.NetCore kell nekünk nem sima AspNet 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlantCare.Data;
using PlantCare.Entities.Dtos.User;
using PlantCare.Logic.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlantCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<AppUser> userManager; 
        RoleManager<IdentityRole> roleManager;
        DtoProvider dtoProvider;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, DtoProvider dtoProvider)
        {
            this.userManager = userManager; //A felhasználók kezelésére szolgál.
            this.roleManager = roleManager; //A felhasználói szerepek kezelésére szolgál.
            this.dtoProvider = dtoProvider; //A DTO-k (Data Transfer Objects) kezelésére szolgáló segédosztály.
        }

        [HttpGet("grantadmin/{userid}")]
        [Authorize(Roles = "Admin")]
        public async Task GrantAdmin(string userid) // hozzáadja az "Admin" szerepkört a felhasználóhoz.
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
                throw new ArgumentException("User not found");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        [HttpGet("revokeadmin/{userid}")]
        [Authorize(Roles = "Admin")]
        public async Task RevokeAdmin(string userid) //elveszi
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
                throw new ArgumentException("User not found");
            await userManager.RemoveFromRoleAsync(user, "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<UserViewDto> GetUsers()
        {
            return userManager.Users.Select(t =>
                dtoProvider.Mapper.Map<UserViewDto>(t)
            );
        }

        [HttpPost("register")]
        public async Task Register(UserInputDto dto)
        {
            var user = new AppUser(dto.UserName);
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            await userManager.CreateAsync(user, dto.Password);
            if (userManager.Users.Count() == 1) //Ha ez az első regisztrált felhasználó, akkor automatikusan adminisztrátorrá lépteti elő.
            {
                await roleManager.CreateAsync(new IdentityRole("Admin")); //létrehozza az "Admin" szerepkört, ha az még nem létezik.
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto) //Ha a bejelentkezés sikeres, egy JWT (JSON Web Token) token-t generál, amelyet a válaszban küld vissza.
        {
            var user = await userManager.FindByNameAsync(dto.UserName); //van-e ilyen felhasználó
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            else
            {
                var result = await userManager.CheckPasswordAsync(user, dto.Password); //jó e a jelszó
                if (!result)
                {
                    throw new ArgumentException("Incorrect password");
                }
                else
                {
                    //generate token
                    var claim = new List<Claim>();
                    claim.Add(new Claim(ClaimTypes.Name, user.UserName!));
                    claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                    foreach (var role in await userManager.GetRolesAsync(user)) //lekérdezi a felhasználó összes szerepét, és hozzáadja őket a claims listához.
                    {
                        claim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    int expiryInMinutes = 24 * 60; //érvényességiidő
                    var token = GenerateAccessToken(claim, expiryInMinutes);  

                    return Ok(new LoginResultDto() //bejelentkezés sikeres és a token generálása megtörtént, akkor a válaszban visszaadjuk a generált token-t és annak lejárati idejét:
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token), //stringgé
                        Expiration = DateTime.Now.AddMinutes(expiryInMinutes)
                    });
                }
            }
        }

        private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims, int expiryInMinutes) //létrehoz egy JWT token-t, amely tartalmazza a felhasználói adatokat és szerepeket.
        {
            var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes("NagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcs"));

            return new JwtSecurityToken(
                  issuer: "plantcare.com",
                  audience: "plantcare.com",
                  claims: claims?.ToArray(),
                  expires: DateTime.Now.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
        }
    }
}
