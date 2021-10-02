using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.DTO
{
    public class AlunoResponsavelDTO
    {
        public AlunoDTO Aluno { get; set; }
        public ResponsavelDTO Responsavel { get; set; }
        public String Parentesco { get; set; }
    }
}
