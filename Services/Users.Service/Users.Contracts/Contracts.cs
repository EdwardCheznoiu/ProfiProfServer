
using System;

namespace Users.Contracts
{
    public record UserPublishDto(Guid Id, string Fname, string Lname, string Email, string Password, string PhoneNumber, string ProfileImage, string Function, string Cabinet, string Details, string Role);
    public record RegisterRecordDto();
}
