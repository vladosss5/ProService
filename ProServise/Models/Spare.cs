using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProServise.Models;

public partial class Spare
{
    public int IdSpare { get; set; }

    public string Name { get; set; } = null!;

    public float PurchasePrice { get; set; }

    public float RetailPrice { get; set; }

    public int CountSpare { get; set; }
    [NotMapped] public bool SelectSpare { get; set; } = false;

    public virtual ICollection<RepairsSpare> RepairsSpares { get; set; } = new List<RepairsSpare>();
}
