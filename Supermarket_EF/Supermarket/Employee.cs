using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket_EF.Supermarket
{
    public class Employee : Person
    {
        public bool IsVaccinated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PaymentPerHour { get; set; }
        public int Department_Id { get; set; }
        public Department Department { get; set; }
        public List<Ticket> Tickets = new List<Ticket>();
    }
}
