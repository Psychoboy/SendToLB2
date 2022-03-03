using System;
using System.Collections.Generic;
using System.Text;

namespace SendToLB2
{
    public class Authentication
    {
        public string rq { get; set; } = "Authentication";
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";
    }
}
