using creditcard.application.Features.Logs.Commands;
using creditcard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Interfaces
{
    public interface IEstadoCuentaCommand
    {
        Task<GenericResponse> AddTransaccion(string Numero_Tarjeta, string Descripcion, double Monto, string Tipo_Transaccion, string categoria);
        Task<GenericResponse> Addpago(string Numero_Tarjeta,double Monto);
    }
}
