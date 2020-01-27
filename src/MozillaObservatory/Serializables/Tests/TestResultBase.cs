using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozillaObservatory.Serializables.Tests
{
    public abstract class TestResultBase<T>
    {
        [JsonProperty("expectation")]
        public string Expectation { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("output")]
        public T Output { get; set; }

        [JsonProperty("pass")]
        public bool Pass { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("score_description")]
        public string ScoreDescription { get; set; }

        [JsonProperty("score_modifier")]
        public long ScoreModifier { get; set; }
    }
}
