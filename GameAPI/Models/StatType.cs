﻿using System;
using System.Collections.Generic;

namespace GameAPI.Models;

public partial class StatType
{
    public int Id { get; set; }

    public string StatName { get; set; } = null!;

    public string StatNameAbr { get; set; } = null!;

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();
}
