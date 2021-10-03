using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        private static readonly Expression<Func<AlunoResponsavel, AlunoResponsavelDTO>> AsAlunoRespDTO =
            x => new AlunoResponsavelDTO
            {
                IdAluno = x.IdAluno,
                IdResponsavel = x.IdResponsavel
            };

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
                    Email = s.Email,
                    Responsaveis = DBContext.AlunoResponsavel.Where(a => a.IdAluno.Equals(s.Id)).Select(AsAlunoRespDTO).ToList()
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
                    Email = s.Email,
                    Responsaveis = DBContext.AlunoResponsavel.Where(a => a.IdAluno.Equals(s.Id)).Select(AsAlunoRespDTO).ToList()
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
                    Email = s.Email,
                    Responsaveis = DBContext.AlunoResponsavel.Where(a => a.IdAluno.Equals(s.Id)).Select(AsAlunoRespDTO).ToList()
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

            List<AlunoDTO> listaAlunos = new List<AlunoDTO>();

            foreach(var idAlunos in idsRelacao)
            {
                int idAluno = Convert.ToInt32(idAlunos.ToString());
                // Busca os alunos por Id
                AlunoDTO alunoId = await DBContext.Aluno.Where(s => s.Id == idAluno)
                .Select(s => new AlunoDTO
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    DataNascimento = s.DataNascimento,
                    Segmento = s.Segmento,
                    Foto = s.Foto,
                    Email = s.Email,
                    Responsaveis = DBContext.AlunoResponsavel.Where(a => a.IdAluno.Equals(s.Id)).Select(AsAlunoRespDTO).ToList()
                }).FirstOrDefaultAsync();

                listaAlunos.Add(alunoId);
            }


            if (Responsaveis == null)
            {
                return NotFound();
            }
            else
            {
                return listaAlunos;
            }
        }
        #endregion

        #region Sets
        [HttpPost("InsereAluno")]
        public async Task<HttpStatusCode> InsereAluno(AlunoDTO Aluno)
        {
            Aluno alunoDB = new Aluno()
            {
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
                int idInserido = alunoDB.Id;
                    
                // Insere os responsáveis pelo aluno
                foreach (var responsavel in Aluno.Responsaveis)
                {
                    int idResponsavel = Convert.ToInt32(responsavel.ToString());

                    // Insere os responsáveis deste aluno
                    AlunoResponsavel AlunoResponsavelDB = new AlunoResponsavel()
                    {
                        IdAluno = idInserido,
                        IdResponsavel = idResponsavel
                    };

                    DBContext.AlunoResponsavel.Add(AlunoResponsavelDB);
                }
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
