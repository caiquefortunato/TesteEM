using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        #region Metodos GET
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

        [HttpGet("GetAlunoByNome")]
        public async Task<ActionResult<List<AlunoDTO>>> GetAlunoByNome(String nome)
        {
            var List = await DBContext.Aluno.Where(s => s.Nome.Contains(nome))
                .Select(s => new AlunoDTO
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    DataNascimento = s.DataNascimento,
                    Segmento = s.Segmento,
                    Foto = s.Foto,
                    Email = s.Email
                }).ToListAsync();

            if (List == null)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetAlunoBySegmento")]
        public async Task<ActionResult<List<AlunoDTO>>> GetAlunoBySegmento(String segmento)
        {
            var List = await DBContext.Aluno.Where(s => s.Segmento == segmento)
                .Select(s => new AlunoDTO
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    DataNascimento = s.DataNascimento,
                    Segmento = s.Segmento,
                    Foto = s.Foto,
                    Email = s.Email
                }).ToListAsync();

            if (List == null)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetAlunoByResponsavel")]
        public async Task<ActionResult<List<AlunoDTO>>> GetAlunoByResponsavel(String responsavel)
        {
            var Responsaveis = await DBContext.Responsavel.Where(s => s.Nome.Contains(responsavel)).ToListAsync();
            ArrayList idsRelacao = new ArrayList();

            foreach(var item in Responsaveis)
            {
                var relacao = await DBContext.AlunoResponsavel.Where(s => s.IdResponsavel.Equals(item.Id)).ToListAsync();
                foreach(var r in relacao) idsRelacao.Add(r.IdAluno);
            }


            List<AlunoDTO> resultado = new List<AlunoDTO>();


            if (Responsaveis == null)
            {
                return NotFound();
            }
            else
            {
                return resultado;
            }
        }
        #endregion

        #region Sets
        [HttpPost("InsereAluno")]
        public async Task<HttpStatusCode> InsereAluno(AlunoDTO Aluno)
        {
            Aluno alunoDB = new Aluno()
            {
                Id = Aluno.Id,
                Nome = Aluno.Nome,
                DataNascimento = Aluno.DataNascimento,
                Segmento = Aluno.Segmento,
                Foto = Aluno.Foto,
                Email = Aluno.Email
            };

            if(Aluno.VerificaObrigatoriedadeEmail() && Aluno.VerificaSegmento())
            {
                DBContext.Aluno.Add(alunoDB);
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
