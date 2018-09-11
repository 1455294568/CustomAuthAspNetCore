using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(16)]
        public string username { get; set; }

        [MaxLength(16)]
        public string passwd { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
