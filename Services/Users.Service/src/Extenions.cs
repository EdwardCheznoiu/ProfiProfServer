using Users.Catalog.Service.Entities;
using Users.Contracts;

namespace Users.Catalog.Service
{
    public static class Extensions
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto(user.Id, user.Fname, user.Lname, user.Email, user.Password, user.PhoneNumber, user.Role);
        }

        public static UserPublishDto UserPublishAsDto(this User user)
        {
            return new UserPublishDto(user.Id, user.Fname, user.Lname, user.Email, user.Password, user.PhoneNumber, user.Function, user.Cabinet, user.ProfileImage, user.Role, user.Detalis);
        }
    }
}