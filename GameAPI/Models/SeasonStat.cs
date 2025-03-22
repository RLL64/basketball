using System;
using System.Collections.Generic;

namespace GameAPI.Models;

public partial class SeasonStat
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public decimal? Total2PtsMade { get; set; }

    public decimal? Total2PtsMissed { get; set; }

    public decimal? Total3PtsMade { get; set; }

    public decimal? Total3PtsMissed { get; set; }

    public decimal? TotalFtmade { get; set; }

    public decimal? TotalFtmissed { get; set; }

    public decimal? TotalAssists { get; set; }

    public decimal? TotalBlocks { get; set; }

    public decimal? TotalSteals { get; set; }

    public decimal? TotalFouls { get; set; }

    public decimal? TotalTurnovers { get; set; }

    public decimal? TotalOreb { get; set; }

    public decimal? TotalDreb { get; set; }
}
