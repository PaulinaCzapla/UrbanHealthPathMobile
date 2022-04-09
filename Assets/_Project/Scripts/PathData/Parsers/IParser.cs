using System.Collections.Generic;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Interface representing parser - converter from stringifiable type to the desired one.
    /// </summary>
    /// <typeparam name="T">Parsed type.</typeparam>
    /// <typeparam name="Y">Output type.</typeparam>
    public interface IParser<in T, out Y>
    {
        Y Parse(T parsedValue);
    }
}
