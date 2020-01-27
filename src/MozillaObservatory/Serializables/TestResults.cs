using MozillaObservatory.Serializables.Tests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozillaObservatory.Serializables
{
    public class TestResults
    {
        [JsonProperty("content-security-policy")]
        public ContentSecurityPolicy ContentSecurityPolicy { get; set; }

        [JsonProperty("cross-origin-resource-sharing")]
        public CrossOriginResourceSharing CrossOriginResourceSharing { get; set; }

        [JsonProperty("public-key-pinning")]
        public PublicKeyPinning PublicKeyPinning { get; set; }

        [JsonProperty("redirection")]
        public Redirection Redirection { get; set; }

        [JsonProperty("strict-transport-security")]
        public StrictTransportSecurity StrictTransportSecurity { get; set; }

        [JsonProperty("subresource-integrity")]
        public SubresourceIntegrity SubresourceIntegrity { get; set; }

        [JsonProperty("x-content-type-options")]
        public XContentTypeOptions XContentTypeOptions { get; set; }

        [JsonProperty("x-frame-options")]
        public XFrameOptions XFrameOptions { get; set; }

        [JsonProperty("x-xss-protection")]
        public XXssProtection XXssProtection { get; set; }
    }
}
