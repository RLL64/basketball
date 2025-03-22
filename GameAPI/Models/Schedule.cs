using System;
using System.Collections.Generic;

namespace GameAPI.Models;

public partial class Schedule
{
    public string? Location { get; set; }

    public DateTimeOffset GameDateTime { get; set; }

    public string HomeTeam { get; set; } = null!;

    public string AwayTeam { get; set; } = null!;
}
