namespace Levi9.ERP.Exceptions
{
    public class CustomExceptionBase : Exception
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public CustomExceptionBase(string message)
        {
            ErrorMessage = message;
        }
    }
}
