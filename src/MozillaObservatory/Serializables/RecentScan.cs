using MozillaObservatory.Serializables.Tests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozillaObservatory.Serializables
{
    public class GradeValue
    {
        public Grades Grade { get; set; }

        public long Value { get; set; }
    }
}
