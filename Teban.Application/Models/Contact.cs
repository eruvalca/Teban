﻿using Teban.Application.Common;

namespace Teban.Application.Models;
public class Contact : BaseAuditableEntity
{
    public int ContactId { get; set; }
    public required string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public required string LastName { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }

    public required string TebanUserId { get; set; }
    public TebanUser TebanUser { get; set; }

    public int CommunicationScheduleId { get; set; }
    public CommunicationSchedule? CommunicationSchedule { get; set; }
}
