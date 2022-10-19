using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teban.UI.Shared
{
    public partial class MonthYearPicker
    {
        [Parameter]
        public DateTime SelectedMonthYear { get; set; }
        [Parameter]
        public EventCallback<int> MonthYearCallback { get; set; }
    }
}
