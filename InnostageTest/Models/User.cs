using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace InnostageTest.Models
{
    public class User : IdentityUser
    {
        public List<Request> Requests { get; set; }
    }
}
