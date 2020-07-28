using MozillaObservatory.Serializables.Tests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozillaObservatory.Serializables
{
    public class RecentScan
    {
        public string Hostname { get; set; }

        public Grades Grade { get; set; }
    }
}
