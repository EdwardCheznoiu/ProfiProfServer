using Users.Catalog.Service.Entities;

namespace Users.Catalog.Service
{
    public static class Extensions
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto(user.Id, user.Fname, user.Lname, user.Email, user.PhoneNumber, user.Function, user.Role);
        }
    }
}