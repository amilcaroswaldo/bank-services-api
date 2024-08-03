using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Domain.ClientesResponse
{
    public class ClienteResponse
    {
        public string ClienteId { get; set; }
        public string Nombre { get; set; }
        public string NumeroIdentidad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
