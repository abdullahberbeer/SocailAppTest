using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAppTwo.DTO
{
    public class UserForUpdateDTO
    {
          public string City { get; set; }
        public string Country { get; set; }
        public string  Introduction{ get; set; }
        public string  Hobbies{ get; set; }
        public string  ProfileImage{ get; set; }
    }
}