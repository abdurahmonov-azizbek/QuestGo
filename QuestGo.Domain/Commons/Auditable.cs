﻿namespace QuestGo.Domain.Commons;

public class Auditable
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);  
    public DateTime? UpdatedAt { get; set; }
}
