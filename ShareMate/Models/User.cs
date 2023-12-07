using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShareMate.Models
{
    public class User  : IdentityUser
    {

        
       
        public int Level { get; set; }
        public string? Bio { get; set; }
        public int Department { get; set; }

       
    }
}
