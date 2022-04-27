using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAppTwo.DTO
{
    public class MessageForCreateDTO
    {
          public int SenderId { get; set; }
     
        public int RecipientId { get; set; }
       
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }=DateTime.Now;
    }
}