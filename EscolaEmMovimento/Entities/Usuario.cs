using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DTO;

namespace WebService.Entities
{
    public class Usuario
    {
        public static UsuarioDTO Get (string username, string password)
        {
            var usuarios = new List<UsuarioDTO>();
            usuarios.Add(new UsuarioDTO { Id = 1, Username = "batman", Password = "batman", Role = "manager" });
            usuarios.Add(new UsuarioDTO { Id = 1, Username = "batman", Password = "batman", Role = "employee" });

            return usuarios.Where(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == password).First();
        }
    }
}
