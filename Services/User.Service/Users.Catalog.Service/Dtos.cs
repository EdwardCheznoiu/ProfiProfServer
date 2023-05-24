using System;
using System.ComponentModel.DataAnnotations;

namespace Users.Catalog.Service
{
    public record UserDto(Guid Id, string Fname, string Lnmae, string Email, string PhoneNumber, string Function, string Role);
    public record CreateUserDto([Required] string Fname, [Required] string Lname, string Email, string PhoneNumber, string Function, string Role);
    public record UpdateUserDto([Required] string Fname, [Required] string Lname, string Email, string PhoneNumber, string Function, string Role);
}