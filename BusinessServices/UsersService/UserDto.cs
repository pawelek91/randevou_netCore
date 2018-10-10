using System;

namespace BusinessServices.MessageService
{
    public class UserDto
    {
        public int? Id{get;set;}
        public DateTime? BirthDate { get; set; }
        public string Name{get;set;}
        public string DisplayName{get;set;}
        public char? Gender{get;set;}
    }
}