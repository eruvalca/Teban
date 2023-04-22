using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teban.Contracts.Requests.V1.Contacts;
public class ImportContactsRequest
{
    public IEnumerable<CreateContactRequest> Contacts { get; set; } = Enumerable.Empty<CreateContactRequest>();
}
