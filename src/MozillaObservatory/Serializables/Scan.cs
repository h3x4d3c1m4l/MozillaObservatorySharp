using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MozillaObservatory.Serializables
{
    public class Scan
    {
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("grade")]
        public Grades Grade { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("response_headers")]
        public Dictionary<string, string> ResponseHeaders { get; set; }

        [JsonProperty("scan_id")]
        public long ScanId { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("likelihood_indicator")]
        public RiskLevels LikelihoodIndicator { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("state")]
        public ScanStates State { get; set; }

        [JsonProperty("tests_failed")]
        public long TestsFailed { get; set; }

        [JsonProperty("tests_passed")]
        public long TestsPassed { get; set; }

        [JsonProperty("tests_quantity")]
        public long TestsQuantity { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ScanStates
    {
        [EnumMember(Value = "ABORTED")]
        Aborted,
        [EnumMember(Value = "FAILED")]
        Failed,
        [EnumMember(Value = "FINISHED")]
        Finished,
        [EnumMember(Value = "PENDING")]
        Pending,
        [EnumMember(Value = "STARTING")]
        Starting,
        [EnumMember(Value = "RUNNING")]
        Running
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RiskLevels
    {
        [EnumMember(Value = "LOW")]
        Low,
        [EnumMember(Value = "MEDIUM")]
        Medium,
        [EnumMember(Value = "HIGH")]
        High,
        [EnumMember(Value = "MAXIMUM")]
        Maximum
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Grades
    {
        [EnumMember(Value = "F")]
        F,
        [EnumMember(Value = "E-")]
        EMinus,
        [EnumMember(Value = "E")]
        E,
        [EnumMember(Value = "E+")]
        EPlus,
        [EnumMember(Value = "D-")]
        DMinus,
        [EnumMember(Value = "D")]
        D,
        [EnumMember(Value = "D+")]
        DPlus,
        [EnumMember(Value = "C-")]
        CMinus,
        [EnumMember(Value = "C")]
        C,
        [EnumMember(Value = "C+")]
        CPlus,
        [EnumMember(Value = "B-")]
        BMinus,
        [EnumMember(Value = "B")]
        B,
        [EnumMember(Value = "B+")]
        BPlus,
        [EnumMember(Value = "A-")]
        AMinus,
        [EnumMember(Value = "A")]
        A,
        [EnumMember(Value = "A+")]
        APlus
    }
}
