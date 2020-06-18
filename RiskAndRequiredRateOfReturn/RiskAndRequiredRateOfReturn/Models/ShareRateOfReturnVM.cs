using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RiskAndRequiredRateOfReturn.Models
{
    public class ShareRateOfReturnVM
    {
        [Required]
        public float CashFlowReceved { get; set; }
        [Required]
        public float ValueAtBeginning { get; set; }
        [Required]
        public float ValueAtEnding { get; set; }


    }
}
