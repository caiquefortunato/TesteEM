﻿using System;
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

        public bool VerificaObrigatoriedadeEmail()
        {
            if (Segmento.Equals("Fundamental") && Email == null) return false;
            else return true;
        }

        public bool VerificaSegmento()
        {
            if (Segmento.Equals("Fundamental") || Segmento.Equals("Infantil")) return true;
            else return false;
        }
    }
}
