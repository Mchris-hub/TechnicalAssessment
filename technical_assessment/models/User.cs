﻿namespace Technical_assessment.models
{
    public class User
    {
       public Guid UserAuth { get; set; }
       public string UserId { get; set; }
       public string Username { get; set;  }
       public string Email { get; set; }
       public string Password { get; set; }
    }
}