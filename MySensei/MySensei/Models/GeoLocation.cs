using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class Country
    {
        public int ID { get; set; }
        [StringLength(10)]
        public string CC_FIPS { get; set; }
        [StringLength(10)]
        public string CC_ISO { get; set; }
        [StringLength(10)]
        public string TLD { get; set; }
        [StringLength(100)]
        public string COUNTRY_NAME { get; set; }
        public virtual ICollection<AppUser> Users { get; set; }
        //public virtual ICollection<City> Cities { get; set; }
    }

    public class City
    {
        public int ID { get; set; }
        //    public int CountryID { get; set; }
        [StringLength(10)]
        public string CC_FIPS { get; set; }
        [StringLength(100)]
        public string FULL_NAME_ND { get; set; }
        //    public virtual Country Country { get; set; }
    }


}