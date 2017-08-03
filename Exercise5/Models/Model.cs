using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise5.Models
{
    public class Model
    {

    }
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            :   base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
    public class User
    {
        public int id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public String firstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public String lastName { get; set; }
        public String gender { get; set; }
        public String role { get; set; }
        public String favs { get; set; }
    }
}
