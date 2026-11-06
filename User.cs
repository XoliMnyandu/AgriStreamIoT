using System;
using System.Collections.Generic;

namespace AgriStreamIoT.web.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PassWordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Farm> Farms { get; set; } = new List<Farm>();
}
