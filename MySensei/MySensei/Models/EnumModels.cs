using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum Language
    {
        Danish,
        English,
        German
    }

    public enum Cities
    {
        Copenhagen,
        Aarhus,
        Odense,
        Aalborg,
        Frederiksberg,
        Esbjerg,
        Gentofte,
        Gladsaxe,
        Randers,
        Kolding,
        Horsens,
        [Display(Name = "Lyngby-Taarbæk")]
        LyngbyTaarbaek,
        Vejle,
        Hvidovre,
        Roskilde,
        [Display(Name = "Helsingør")]
        Helsingor,
        Herning,
        Silkeborg,
        Næstved,
        [Display(Name = "Greve Strand")]
        GreveStrand,
        [Display(Name = "Tårnby")]
        Tarnby,
        Fredericia,
        Ballerup,
        [Display(Name = "Rødovre")]
        Rodovre,
        Viborg,
        [Display(Name = "Køge")]
        Koge,
        Holstebro,
        [Display(Name = "Brøndby")]
        Brondby,
        Taastrup,
        Slagelse,
        [Display(Name = "Hillerød")]
        Hillerod,
        Albertslund,
        Sønderborg,
        Svendborg,
        Herlev,
        [Display(Name = "Holbæk")]
        Holbaek,
        [Display(Name = "Hjørring")]
        Hjorring,
        [Display(Name = "Hørsholm")]
        Horsholm,
        Frederikshavn,
        Glostrup,
        Haderslev,
        [Display(Name = "Nørresundby")]
        Norresundby,
        Ringsted,
        [Display(Name = "Ølstykke-Stenløse")]
        OlstykkeStenlose,
        Skive
    }
}