using System;
using System.Collections.Generic;

namespace KaznacheystvoCalendar.Models;

public partial class Event
{
    public int Id { get; set; }

    public int ManagerId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDateTime { get; set; }

    public int LocationId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime EndDateTime { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<EventMember> EventMembers { get; set; } = new List<EventMember>();

    public virtual ICollection<EventVisible> EventVisibles { get; set; } = new List<EventVisible>();

    public virtual Location Location { get; set; } = null!;

    public virtual User Manager { get; set; } = null!;
}
