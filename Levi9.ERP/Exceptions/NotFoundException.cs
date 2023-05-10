namespace Levi9.ERP.Exceptions
{
    public class NotFoundException : CustomExceptionBase
    {
        public NotFoundException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status404NotFound;
        }
    }
}
