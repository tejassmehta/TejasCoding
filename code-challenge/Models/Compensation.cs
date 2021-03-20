using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    public class Compensation
    {
        public String CompensationId { get; set; }
        public String EmployeeId { get; set; }
        public Decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}