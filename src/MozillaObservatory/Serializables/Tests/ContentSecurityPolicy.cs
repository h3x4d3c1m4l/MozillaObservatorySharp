using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozillaObservatory.Serializables.Tests
{
    public partial class ContentSecurityPolicy : TestResultBase<ContentSecurityPolicyOutput>
    {
    }

    public partial class ContentSecurityPolicyOutput
    {
        [JsonProperty("data")]
        public Dictionary<string, string[]> Data { get; set; }
    }
}
