namespace Levi9.ERP.Exceptions
{
    public class BadRequestException : CustomExceptionBase
    {
        public BadRequestException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
