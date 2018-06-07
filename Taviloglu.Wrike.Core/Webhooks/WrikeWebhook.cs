﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Taviloglu.Wrike.Core
{
    /// <summary>
    /// Webhooks allow you to subscribe to notifications about changes in Wrike instead of having to rely on periodic polling. 
    /// </summary>
    public sealed class WrikeWebHook : WrikeObjectWithId
    {
        public WrikeWebHook() { }

        public WrikeWebHook(string accountId, string hookUrl)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentException("accountId can not be null or empty");
            }
            if (string.IsNullOrWhiteSpace(hookUrl))
            {
                throw new ArgumentException("hookUrl can not be null or empty");                
            }

            AccountId = accountId;
            HookUrl = hookUrl;
        }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        [JsonProperty("accountId")]
        public string FolderId { get; set; }
        [JsonProperty("folderId")]
        public string HookUrl { get; set; }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WrikeWebHookStatus Status { get; set; }
    }
    

}
