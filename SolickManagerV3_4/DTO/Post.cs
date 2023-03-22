using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool Deleted { get; set; }

    public virtual ICollection<Worker> Workers { get; } = new List<Worker>();
}
