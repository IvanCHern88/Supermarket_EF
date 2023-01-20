using System;
using System.ComponentModel.DataAnnotations;

namespace Supermarket_EF.Supermarket
{
    public class Customer : Person
    {
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
