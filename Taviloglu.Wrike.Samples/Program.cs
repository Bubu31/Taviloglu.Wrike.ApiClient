﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Taviloglu.Wrike.ApiClient;
using Taviloglu.Wrike.Core;

namespace Taviloglu.Wrike.Samples
{
    class Program
    {
        static void Main(string[] args)
        {

            MainAsync(args).Wait();
            
        }

        static async Task MainAsync(string[] args)
        {
            var bearerToken = "7VIw0efGxvG3IrTYUrgCiSvZhrMsFUkh2VEcp0Noznc3QiYj6jrm0lbcd6nobFW5-N-WFIUKC";
            var wrikeClient = new WrikeClient(bearerToken);

            #region Colors
            //var colors = await wrikeClient.GetColorsAsync();
            #endregion

            #region CustomFields
            //var customFields = await wrikeClient.GetCustomFieldsAsync();
            //var customFields = await wrikeClient.GetCustomFiledInfoAsync(new List<string> { "IEABX2HEJUAAREOB", "IEABX2HEJUAAREOD" });

            //TODO: returns invalid-parameter error if type is not Text
            //var updatedCustomField = await wrikeClient.UpdateCustomFieldAsync(
            //    "IEABX2HEJUAATMXD", 
            //    title: "sinanTestField3",
            //    type: WrikeCustomFieldType.Money);


            //var newCustomField = new WrikeCustomField
            //{
            //    AccountId = "IEABX2HE",
            //    Title = "Sinan Test Custom Duration",
            //    Type = WrikeCustomFieldType.Duration
            //};
            //var field = await wrikeClient.CreateCustomFieldAsync(newCustomField);
            #endregion

            #region Tasks
            //var tasks = await wrikeClient.GetTasksAsync(new List<string> { "IEABX2HEKQGIKBTE", "IEABX2HEKQGIKBYK" });
            #endregion

            #region Users
            var user = await wrikeClient.GetUserAsync("");
            #endregion
        }
    }
}
