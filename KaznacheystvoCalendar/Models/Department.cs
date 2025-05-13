using System;
using System.Collections.Generic;

namespace KaznacheystvoCalendar.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventVisible> EventVisibles { get; set; } = new List<EventVisible>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
