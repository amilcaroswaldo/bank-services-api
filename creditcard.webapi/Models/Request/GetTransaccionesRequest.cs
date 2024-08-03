namespace creditcard.webapi.Models.Request
{
    public class GetTransaccionesRequest
    {
        public string NumeroTarjeta { get; set; }
        public string FchInicio { get; set; }
        public string FchFin { get; set; }
    }
}
