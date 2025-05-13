using System;
using System.Collections.Generic;

namespace KaznacheystvoCalendar.Models;

public partial class Comment
{
    public int Id { get; set; }

    public DateTime DateTime { get; set; }

    public string Text { get; set; } = null!;

    public int UserId { get; set; }

    public int? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User User { get; set; } = null!;
}
