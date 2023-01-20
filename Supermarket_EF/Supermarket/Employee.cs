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
        public virtual Department Department { get; set; }
        public virtual List<Ticket> Tickets { get; set; }= new List<Ticket>();
    }
}
