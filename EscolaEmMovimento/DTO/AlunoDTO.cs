using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.DTO
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Segmento { get; set; }
        public String Foto { get; set; }
        public String Email { get; set; }
    }
}
