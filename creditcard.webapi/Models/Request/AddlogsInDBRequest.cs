namespace creditcard.webapi.Models.Request
{
    public class AddlogsInDBRequest
    {
        public string ErrorMessage { get; set; } = null;
        public int ErrorNumber { get; set; }
        public string OriginatingComponent { get; set; } = null;
        public string AdditionalInfo { get; set; } = null;
    }
}
