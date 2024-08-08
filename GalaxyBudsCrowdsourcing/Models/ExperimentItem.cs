using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace GalaxyBudsCrowdsourcing.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Devices
    {
        Buds,
        BudsPlus,
        BudsLive,
        BudsPro,
        Buds2,
        Buds2Pro,
        BudsFe,
        Buds3,
        Buds3Pro
    }
    
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Environment
    {
        ProductionOnly,
        Production,
        Internal,
    }
    
    public class ExperimentItem
    {
        public long Id { set; get; }
        public string? Name { set; get; }
        public string? Reason { set; get; }
        
        
        [JsonIgnore]
        public bool Enabled { set; get; }
        public bool IsLegacyVariant { set; get; }
        
        public Environment? Environment { set; get; }

        public Devices[] TargetDevices { set; get; } = [];
        public int? TimeConstraint { set; get; }
        public int? MinimumRevision { set; get; }
        public int? MaximumRevision { set; get; }
        public string? MinimumAppVersion { set; get; }
        public string? MaximumAppVersion { set; get; }
        
        [NotMapped]
        public string? Signature { set; get; }
        public string? Script { set; get; }
    }
}