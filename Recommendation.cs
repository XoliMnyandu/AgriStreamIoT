using System;
using System.Collections.Generic;

namespace AgriStreamIoT.web.Models;

public partial class Recommendation
{
    public int RecommendationId { get; set; }

    public int FarmId { get; set; }

    public int? CropId { get; set; }

    public string RecommendationType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Crop? Crop { get; set; }

    public virtual Farm Farm { get; set; } = null!;
}
