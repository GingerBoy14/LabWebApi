using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
    public class InvalidTokenException : BadRequestException
    {
        public InvalidTokenException() : base("Invalid Token") { }
    }
    public class InvalidLoginException : UnauthorizedException
    {
        public InvalidLoginException() : base("Incorrect Email or Password!") { }
    }
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException() : base("System don't have this role!") { }
    }
    public class UserAlreadyExistsException : BadRequestException
    {
        public UserAlreadyExistsException(string value) : base($"User with this {value} was already exists!") { }
    }
}
