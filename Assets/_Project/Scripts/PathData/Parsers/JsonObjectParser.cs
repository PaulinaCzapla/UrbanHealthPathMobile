using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Base class of parsers of JObjects.
    /// </summary>
    /// <typeparam name="T">Output type.</typeparam>
    public abstract class JsonObjectParser<T> : IParser<JObject, T>
    {
        public virtual T Parse(JObject parsedValue)
        {
            return ParseJsonObject(parsedValue);
        }
        
        protected abstract T ParseJsonObject(JObject json);
    }
}