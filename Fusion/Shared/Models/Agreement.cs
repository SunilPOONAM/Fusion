using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class Agreement
    {
        public int AgreementID { get; set; }

        public string CustomerID { get; set; }

        public string Nickname { get; set; }

        public string Notes { get; set; }

        public string Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? SalesRep { get; set; }

        public DateTime? AwardDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? PrimaryAgrID { get; set; }

        public int? MPH_Rate { get; set; }

        public short? AgrmtDiscount { get; set; }

        public int? AgreementType { get; set; }

        public decimal? SubContMarkUpParts { get; set; }

        public decimal? SubContMarkUpMaterial { get; set; }

        public decimal? SubContMarkUpContract { get; set; }

        public decimal? SubContMarkUpRental { get; set; }

        public decimal? SubContMarkUpPropane { get; set; }

        public decimal? SubContMarkUpDiesel { get; set; }

        public bool? PrevailingWage { get; set; }

        public bool? TotalIncludesTax { get; set; }

        public int? NoOfYears { get; set; }

        public string AssetType { get; set; }

        public bool? CheckOrACH { get; set; }

        public bool? ENVIRONSURCH { get; set; }

        public bool? MILEAGESURCH { get; set; }
    }
}
