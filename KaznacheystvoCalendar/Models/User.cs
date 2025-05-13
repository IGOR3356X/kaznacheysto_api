using System;
using System.Collections.Generic;

namespace KaznacheystvoCalendar.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Photo { get; set; }

    public int? RoleId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<EventMember> EventMembers { get; set; } = new List<EventMember>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Role? Role { get; set; }
}
