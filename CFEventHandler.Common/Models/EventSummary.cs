using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Models
{
    public class EventSummary
    {
        public DateTimeOffset Date { get; set; }

        public string EventClientId { get; set; }

        public string EventTypeId { get; set; }

        public int Count { get; set; }
    }
}
