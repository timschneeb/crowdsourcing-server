using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using GalaxyBudsCrowdsourcing.Models;

namespace GalaxyBudsCrowdsourcing
{
    public static class Postprocessors
    {
        public static ExperimentItem SignScript(this ExperimentItem item, int apiVersion)
        {
            var script = string.Empty;
            try
            {
                script = File.ReadAllText(item.Script ?? string.Empty);
                item.Script = Convert.ToBase64String(Encoding.UTF8.GetBytes(script));
            }
            catch (Exception ex)
            {
                item.Script = "Currently unavailable";
                Console.Error.WriteLine($"{ex} ({ex.Message}");
            }
            
            var knownSignature = Startup.Configuration["KnownSignature"];
            var rsaSigningKey = Startup.Configuration["RSASigningKey"];
            
            if(string.IsNullOrEmpty(knownSignature) || string.IsNullOrEmpty(rsaSigningKey)) 
                throw new InvalidCredentialException("KnownSignature or RSASigningKey is not set in the configuration file");
            
            switch (apiVersion)
            {
                case 1:
                    item.Signature = Crypto.RsaEncryptWithPrivate(string.Format(knownSignature, item.Id), rsaSigningKey);
                    break;
                case 2:
                    var hash = SHA1.HashData(Encoding.UTF8.GetBytes(script));
                    item.Signature = Crypto.RsaEncryptWithPrivate(Convert.ToBase64String(hash), rsaSigningKey);
                    break;
            }
            return item;
        }
        
        public static IEnumerable<ExperimentItem> SignScripts(this IEnumerable<ExperimentItem> items, int apiVersion)
        {
            return items.Select(x => SignScript(x, apiVersion));
        }
    }
}
