﻿using Newtonsoft.Json;

namespace Taviloglu.Wrike.Core
{
    public class WrikeCustomFieldData : WrikeObjectWithId
    {
        /// <summary>
        /// Custom field value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

    }
}
