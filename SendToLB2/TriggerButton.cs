using System;
using System.Collections.Generic;
using System.Text;

namespace SendToLB2
{
    public class TriggerButton
    {
        public string rq { get; set; } = "TriggerButton";
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string ButtonId { get; set; }
    }
}
