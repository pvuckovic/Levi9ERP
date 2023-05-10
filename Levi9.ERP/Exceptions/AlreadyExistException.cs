using Microsoft.AspNetCore.Http;

namespace Levi9.ERP.Exceptions
{
    public class AlreadyExistException : CustomExceptionBase
    {
        public AlreadyExistException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
