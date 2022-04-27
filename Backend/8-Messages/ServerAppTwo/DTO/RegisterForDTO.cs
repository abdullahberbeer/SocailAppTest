using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAppTwo.DTO
{
    public class RegisterForDTO
    {
         public string Name { get; set; }
         public string UserName { get; set; }
         public string Email { get; set; }
         public string Gender { get; set; }
         public string Password { get; set; }
         public DateTime DateOfBirth { get; set; }
         public string Country { get; set; }
         public string City { get; set; }
    }
}