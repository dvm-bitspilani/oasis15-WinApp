using System;
using System.Collections.Generic;
using System.Text;

namespace HubApp4
{
    public class parseresult
    {
        public parseresult(String EventName, string Result)
        {
            this.eventName = EventName;
            this.result = Result;

            //  this.Description = description;

        }
        public string eventName { get; private set; }
        public string result { get; private set; }

    }
}
