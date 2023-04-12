using Microsoft.AspNetCore.Components;

namespace Teban.UI.Shared;
public partial class ErrorList
{
    [Parameter]
    public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
}
