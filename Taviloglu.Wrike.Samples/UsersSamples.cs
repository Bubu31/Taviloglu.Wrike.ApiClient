﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taviloglu.Wrike.ApiClient;

namespace Taviloglu.Wrike.Samples
{
    public static class UsersSamples
    {
        public static async Task Run(WrikeClient client)
        {
            var user = await client.Users.GetAsync("userId");
        }
    }
}
