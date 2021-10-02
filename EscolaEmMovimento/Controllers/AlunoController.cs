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
    public class AlunoController : ControllerBase
    {
        private readonly DBContext DBContext;
        public AlunoController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetAlunos")]
        public async Task<ActionResult<List<AlunoDTO>>> Get()
        {
            var List = await DBContext.Aluno.Select(
                s => new AlunoDTO
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    DataNascimento = s.DataNascimento,
                    Segmento = s.Segmento,
                    Foto = s.Foto,
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
