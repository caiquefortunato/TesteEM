using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Entities
{
    public partial class Aluno
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Segmento { get; set; }
        public String Foto { get; set; }
        public String Email { get; set; }
    }
}
