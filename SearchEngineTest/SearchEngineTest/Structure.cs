using System;
using System.ComponentModel;
using System.Configuration;

namespace SearchEngineTest
{
    public enum SearchSite
    {
        [Description("Microsoft Graph")]
        MSGraph,
        [Description("Office Dev Center")]
        OfficeDevCenter
    }

    /// <summary>
    /// This struct contains the searched result's information
    /// </summary>
    public struct SearchedResult
    {
        /// <summary>
        /// The result name
        /// </summary>
        public string Name;

        /// <summary>
        /// The detail link
        /// </summary>
        public string DetailLink;
    }
}