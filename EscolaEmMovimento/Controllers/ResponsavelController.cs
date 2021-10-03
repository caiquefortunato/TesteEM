using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebService.DTO;
using WebService.Entities;
using WebService.Services;

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

        #region Get
        [HttpGet("GetResponsaveis")]
        [Authorize]
        public async Task<ActionResult<List<ResponsavelDTO>>> Get()
        {
            var List = await DBContext.Responsavel.Select(
                s => new ResponsavelDTO
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    DataNascimento = s.DataNascimento,
                    Telefone = s.Telefone,
                    Email = s.Email,
                    Parentesco = s.Parentesco
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
        #endregion

        #region Set
        [HttpPost("InsereResponsavel")]
        [Authorize]
        public async Task<HttpStatusCode> InsereResponsavel(ResponsavelDTO Responsavel)
        {
            Responsavel responsavelDB = new Responsavel()
            {
                Id = Responsavel.Id,
                Nome = Responsavel.Nome,
                DataNascimento = Responsavel.DataNascimento,
                Telefone = Responsavel.Telefone,
                Parentesco = Responsavel.Parentesco,
                Email = Responsavel.Email
            };

            if (Responsavel.VerificaObrigatoriedadeEmail())
            {
                DBContext.Responsavel.Add(responsavelDB);
                await DBContext.SaveChangesAsync();

                return HttpStatusCode.Created;
            }
            else
            {
                return HttpStatusCode.NoContent;
            }
        }
        #endregion
    }
}
