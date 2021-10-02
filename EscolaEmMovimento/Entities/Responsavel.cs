using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Entities
{
    public partial class Responsavel
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Telefone { get; set; }
        public String Email { get; set; }
        public String Parentesco { get; set; }
    }
}
