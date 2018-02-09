﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Taviloglu.Wrike.Core;

namespace Taviloglu.Wrike.ApiClient
{
    public class WrikePostDataBuilder
    {
        List<KeyValuePair<string, string>> _parameters;

        public WrikePostDataBuilder()
        {
            _parameters = new List<KeyValuePair<string, string>>();
        }

        public WrikePostDataBuilder AddParameter(string name, object value, JsonConverter jsonConverter = null)
        {
            if (value == null)
            {
                return this;
            }

            if (value is bool)
            {
                AddBool(name, (bool)value);
                return this;
            }

            if (value is string)
            {
                AddString(name, (string)value);
                return this;
            }

            if (value is int)
            {
                AddInt(name, (int)value);
                return this;
            }

            if (value is IList)
            {
                AddList(name, (IList)value);
                return this;
            }

            if (value is Enum)
            {
                AddEnum(name, (Enum)value);
                    return this;
            }

            if (value is IWrikeObject)
            {
                AddWrikeObject(name, (IWrikeObject)value, jsonConverter);
                return this;
            }

            throw new ArgumentException($"{value.GetType()} is not implemented");
        }

        private void AddEnum(string parameterName, Enum parameterValue)
        {
            _parameters.Add(new KeyValuePair<string, string>(parameterName, parameterValue.ToString()));
        }

        private void AddBool(string parameterName, bool parameterValue)
        {
            _parameters.Add(new KeyValuePair<string, string>(parameterName, parameterValue.ToString().ToLower()));
        }
        private void AddString(string parameterName, string parameterValue)
        {
            _parameters.Add(new KeyValuePair<string, string>(parameterName, parameterValue));
        }

        private void AddInt(string parameterName, int parameterValue)
        {
            _parameters.Add(new KeyValuePair<string, string>(parameterName, parameterValue.ToString()));
        }

        private void AddList(string parameterName, IList parameterValue)
        {
            _parameters.Add(new KeyValuePair<string, string>(parameterName, JsonConvert.SerializeObject(parameterValue)));
        }

        private void AddWrikeObject(string parameterName, IWrikeObject parameterValue, JsonConverter jsonConverter = null)
        {
            if (jsonConverter != null)
            {
                _parameters.Add(new KeyValuePair<string, string>(parameterName, JsonConvert.SerializeObject(parameterValue, jsonConverter)));
            }
            else
            {
                _parameters.Add(new KeyValuePair<string, string>(parameterName, JsonConvert.SerializeObject(parameterValue)));
            }
        }

        public FormUrlEncodedContent GetPostData()
        {
            return new FormUrlEncodedContent(_parameters);
        }
    }
}
