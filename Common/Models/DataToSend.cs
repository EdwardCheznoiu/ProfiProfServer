using System;

namespace Common.Models
{
    public class DataToSend
    {
        public Guid UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Function { get; set; }
        public string Cabinet { get; set; }
        public string ProfileImage { get; set; }
        public string Role { get; set; }
        public string Details { get; set; }
        public DateTimeOffset LastLogin { get; set; }
        public int LoginTimes { get; set; }
    }

}