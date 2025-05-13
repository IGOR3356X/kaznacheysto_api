using System;
using System.Collections.Generic;

namespace KaznacheystvoCalendar.Models;

public partial class EventVisible
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public int EventId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
