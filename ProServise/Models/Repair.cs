using System;
using System.Collections.Generic;

namespace ProServise.Models;

public partial class Repair
{
    public int IdRepair { get; set; }

    public DateTime DateReception { get; set; }
    public string Status { get; set; }
    
    public string DescriptionBreaking { get; set; }

    public int IdClient { get; set; }

    public int IdDevice { get; set; }

    public int IdUser { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Device IdDeviceNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
    
    public virtual ICollection<RepairHistory> RepairHistories { get; set; } = new List<RepairHistory>();

    public virtual ICollection<RepairsSpare> RepairsSpares { get; set; } = new List<RepairsSpare>();

    public virtual ICollection<RepairsWork> RepairsWorks { get; set; } = new List<RepairsWork>();
}
