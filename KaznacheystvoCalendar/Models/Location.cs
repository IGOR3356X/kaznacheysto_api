using System;
using System.Collections.Generic;

namespace KaznacheystvoCalendar.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
