using System;
using System.Collections.Generic;

namespace AgriStreamIoT.web.Models;

public partial class Crop
{
    public int CropId { get; set; }

    public int FarmId { get; set; }

    public string CropName { get; set; } = null!;

    public DateOnly? PlantintDate { get; set; }

    public DateOnly? ExpectedHaverstDate { get; set; }

    public string CropStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Farm Farm { get; set; } = null!;

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
}
