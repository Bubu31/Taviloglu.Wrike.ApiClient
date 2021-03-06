﻿using System.Threading.Tasks;
using Taviloglu.Wrike.Core;

namespace Taviloglu.Wrike.ApiClient
{
    public interface IWrikeVersionClient
    {
        /// <summary>
        /// Returns current API version info
        /// </summary>
        Task<WrikeVersion> GetAsync();
    }

}
