﻿using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Howpay
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool Deleted { get; set; }

    public virtual ICollection<Plan> Plans { get; } = new List<Plan>();
}
