using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DTO;
using WebService.Entities;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsavelController : ControllerBase
    {
        private readonly DBContext DBContext;
        public ResponsavelController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetResponsaveis")]
        public async Task<ActionResult<List<ResponsavelDTO>>> Get()
        {
            var List = await DBContext.Responsavel.Select(
                s => new ResponsavelDTO
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    DataNascimento = s.DataNascimento,
                    Telefone = s.Telefone,
                    Email = s.Email
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
    }
}
