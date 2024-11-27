using System;
using System.ComponentModel.DataAnnotations;
namespace Ciurca_Radu_Lab2.Models
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int BookCount { get; set; }

    }
}