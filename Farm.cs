using System;
using System.Collections.Generic;

namespace AgriStreamIoT.web.Models;

public partial class Farm
{
    public int FarmId { get; set; }

    public int UserId { get; set; }

    public string FarmName { get; set; } = null!;

    public string? Location { get; set; }

    public decimal? SizeHectares { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Crop> Crops { get; set; } = new List<Crop>();

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    public virtual ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();

    public virtual User User { get; set; }
}
