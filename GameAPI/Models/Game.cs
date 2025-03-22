using System;
using System.Collections.Generic;

namespace GameAPI.Models;

public partial class Game
{
    public int Id { get; set; }

    public int HomeTeamId { get; set; }

    public int AwayTeamId { get; set; }

    public string? Location { get; set; }

    public DateTimeOffset GameDateTime { get; set; }

    public virtual Team AwayTeam { get; set; } = null!;

    public virtual Team HomeTeam { get; set; } = null!;

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();
}
