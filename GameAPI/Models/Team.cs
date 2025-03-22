using System;
using System.Collections.Generic;

namespace GameAPI.Models;

public partial class Team
{
    public int Id { get; set; }

    public string TeamName { get; set; } = null!;

    public virtual ICollection<Game> GameAwayTeams { get; set; } = new List<Game>();

    public virtual ICollection<Game> GameHomeTeams { get; set; } = new List<Game>();

    public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new List<PlayerTeam>();
}
