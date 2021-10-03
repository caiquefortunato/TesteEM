using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DTO;
using WebService.Entities;
using WebService.Services;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        private readonly DBContext DBContext;
        public AcessoController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        #region Login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                UsuarioDTO user = await DBContext.Usuario.Select(
                    u => new UsuarioDTO
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Password = u.Password,
                        Role = u.Role
                    })
                .FirstOrDefaultAsync(u => u.Username == usuarioDTO.Username.ToLower() && u.Password == usuarioDTO.Password);

                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(user);
                user.Password = "";
                return new
                {
                    User = user,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
