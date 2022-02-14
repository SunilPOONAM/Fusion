using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Server.Helper
{
    public enum LogTypeEnum : int
    {
        /// <summary>
        /// Unknown log item type
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Customer error log item type
        /// </summary>
        CustomerError = 1,
        /// <summary>
        /// Mail error log item type
        /// </summary>
        MailError = 2,
        /// <summary>
        /// Order error log item type
        /// </summary>
        OrderError = 3,
        /// <summary>
        /// Administration area log item type
        /// </summary>
        AdministrationArea = 4,
        /// <summary>
        /// Common error log item type
        /// </summary>
        CommonError = 5,
        /// <summary>
        /// Shipping error log item type
        /// </summary>
        ShippingErrror = 6,
        /// <summary>
        /// Tax error log item type
        /// </summary>
        TaxError = 7,

        /// <summary>
        /// Database error log item type
        /// </summary>
        DatabaseOprationError = 8,

        /// <summary>
        /// Identifies general errors, like IO/missing files, wrong format, timeouts. 
        ///This is default value.
        /// </summary>
        Error = 9,
        /// <summary>
        /// Identifies fatal errors, like null referece, etc.
        /// </summary>
        FatalError = 10,
        /// <summary>
        /// Identifies security errors, like insufficient permisson.
        /// </summary>
        SecurityError = 11,
        /// <summary>
        /// Identifies unhandled exception.
        /// </summary>
        UnhandledError = 12

    }
}
