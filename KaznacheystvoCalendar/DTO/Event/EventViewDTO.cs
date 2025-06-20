﻿namespace KaznacheystvoCalendar.DTO.Event;

public class EventViewDTO
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int[] DepartmentsId {get; set;}
}