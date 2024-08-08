using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyBudsCrowdsourcing.Models
{
    public enum Platforms
    {
        Windows,
        Linux,
        OSX,
        Other
    }
    
    public class ResultItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required long Id { set; get; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastUpdated { set; get; }

        public required int ResultCode { set; get; }
        public required string ResultCodeString { set; get; }
        public required string Result { set; get; }
        
        public required Devices Device { set; get; }
        public required int Revision { set; get; }
        public required string FirmwareVersion { set; get; }
        public required string MacAddress { set; get; }
        public string? AppVersion { set; get; }
        public string? CountryCode { set; get; }
        public Platforms? Platform { set; get; }
        
        public required long ExperimentId { set; get; }
        public required Environment Environment { set; get; }
    }
}