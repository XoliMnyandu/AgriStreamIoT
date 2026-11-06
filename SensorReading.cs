using System;
using System.Collections.Generic;

namespace AgriStreamIoT.web.Models;

public partial class SensorReading
{
    public int Reading { get; set; }

    public int FarmId { get; set; }

    public decimal? Temparature { get; set; }

    public decimal? Humidity { get; set; }

    public decimal? SoilMoisture { get; set; }

    public decimal? SoilPh { get; set; }

    public DateTime RecordedAt { get; set; }

    public virtual Farm Farm { get; set; } = null!;
}
