using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teban.UI.ViewModels;

public class ContactCardViewModel
{
    public int ContactId { get; set; }
    public required string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public required string LastName { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }

    public string TebanUserId { get; set; } = string.Empty;
    public string? Frequency { get; set; }
    public DateTime? StartDate { get; set; }

    public string FullName => $"{FirstName} {MiddleName} {LastName}";
    public string FirstLastName => $"{FirstName} {LastName}";

    public bool IsSelected { get; set; }
}