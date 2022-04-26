using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAppTwo.DTO
{
    public class UserForListDTO
    {
        public int Id { get; set; }
         public string Name { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
      public int Age { get; set; }
      public string Introduction { get; set; }
      
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string  ProfileImage{ get; set; }
        public ImagesForDetailDTO Image { get; set; }
       
    }
}