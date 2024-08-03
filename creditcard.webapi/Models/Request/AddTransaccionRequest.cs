namespace creditcard.webapi.Models.Request
{
    public class AddTransaccionRequest
    {
        public string NumeroTarjeta { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public string TipoTransaccion { get; set; }
        public string Categoria { get; set; }
    }
}
