﻿namespace UniversityApi.Models
{
    public class StudentCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int age { get; set; }

        public int GroupId { get; set; }
    }
}
