using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace GalaxyBudsCrowdsourcing.Models
{
    public class CoredumpItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required long Id { set; get; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastUpdated { set; get; }
        
        public required string Side { set; get; }
        public required Devices Device { set; get; }
        public required int Revision { set; get; }
        public required string FirmwareVersion { set; get; }
        public required string MacAddress { set; get; }
        public string? AppVersion { set; get; }
        public string? CountryCode { set; get; }
        public Platforms? Platform { set; get; }
        
        public required string Content { set; get; }
    }
}