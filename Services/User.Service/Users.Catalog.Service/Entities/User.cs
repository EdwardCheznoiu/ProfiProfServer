using System;
using Common.Entities;

namespace Users.Catalog.Service.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Function { get; set; }
        public string Role { get; set; }
    }
}