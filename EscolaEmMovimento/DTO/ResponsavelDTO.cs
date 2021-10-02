using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.DTO
{
    public class ResponsavelDTO
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Telefone { get; set; }
        public String Email { get; set; }
        public String Parentesco { get; set; }

        public bool VerificaObrigatoriedadeEmail()
        {
            if (Email == null) return false;
            return true;
        }
    }
}
