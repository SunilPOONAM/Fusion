using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fusion.Server.Helper
{
    public class LogManager
    {

        static DataSet ds;
        #region Properties
        private int _LogID;
        private int _LogTypeID;
        private int _Severity;
        private string _Message;
        private string _Exception;
        private string _IPAddress;
        private int _CustomerID;
        private string _PageURL;
        private DateTime _CreatedOn;

        /// <summary>
        /// Gets or sets the log identifier
        /// </summary>
        public int LogID
        {
            get
            {
                return _LogID;
            }
            set
            {
                _LogID = value;
            }
        }

        /// <summary>
        /// Gets or sets the log type identifier
        /// </summary>
        public int LogTypeID
        {
            get
            {
                return _LogTypeID;
            }
            set
            {
                _LogTypeID = value;
            }
        }

        /// <summary>
        /// Gets or sets the severity
        /// </summary>
        public int Severity
        {
            get
            {
                return _Severity;
            }
            set
            {
                _Severity = value;
            }
        }

        /// <summary>
        /// Gets or sets the short message
        /// </summary>
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }
        }

        /// <summary>
        /// Gets or sets the full exception
        /// </summary>
        public string Exception
        {
            get
            {
                return _Exception;
            }
            set
            {
                _Exception = value;
            }
        }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IPAddress
        {
            get
            {
                return _IPAddress;
            }
            set
            {
                _IPAddress = value;
            }
        }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                _CustomerID = value;
            }
        }

        /// <summary>
        /// Gets or sets the page URL
        /// </summary>
        public string PageURL
        {
            get
            {
                return _PageURL;
            }
            set
            {
                _PageURL = value;
            }
        }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn
        {
            get
            {
                return _CreatedOn;
            }
            set
            {
                _CreatedOn = value;
            }
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="LogType">Log item type</param>
        /// <param name="Message">The short message</param>
        /// <param name="Exception">The exception</param>
        /// <returns>A log item</returns>
        public static void InsertLog(LogTypeEnum LogType, string Message, Exception Exception)
        {

            int CustomerID = 0;
            //if (HttpContext.Current != null && HttpContext.Current.User != null)
            //    CustomerID = NopContext.Current.User.CustomerID;
            string IPAddress = string.Empty;
            //if (HttpContext.Current != null && HttpContext.Current.Request != null)
            IPAddress = "";//HttpContext.Current.Request.UserHostAddress;
            string PageURL = "";// GetThisPageURL(true);
            InsertLog(LogType, 11, Message, Exception, IPAddress, CustomerID, PageURL);
        }

        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="LogType">Log item type</param>
        /// <param name="Severity">The severity</param>
        /// <param name="Message">The short message</param>
        /// <param name="exception">The full exception</param>
        /// <param name="IPAddress">The IP address</param>
        /// <param name="CustomerID">The customer identifier</param>
        /// <param name="PageURL">The page URL</param>
        /// <returns>Log item</returns>
        public static void InsertLog(LogTypeEnum LogType, int Severity, string Message,
            Exception exception, string IPAddress, int CustomerID, string PageURL)
        {
            //don't log thread abort exception
            if ((exception != null) && (exception is System.Threading.ThreadAbortException))
                return;

            if (IPAddress == null)
                IPAddress = string.Empty;

            DateTime CreatedOn = DateTime.Now;

            InsertLog((int)LogType, Severity, Message,
             exception == null ? string.Empty : exception.ToString(), IPAddress, CustomerID, PageURL, CreatedOn);

        }
        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="LogTypeID">Log item type identifier</param>
        /// <param name="Severity">The severity</param>
        /// <param name="Message">The short message</param>
        /// <param name="Exception">The full exception</param>
        /// <param name="IPAddress">The IP address</param>
        /// <param name="CustomerID">The customer identifier</param>
        /// <param name="PageURL">The page URL</param>
        /// <param name="CreatedOn">The date and time of instance creationL</param>
        /// <returns>Log item</returns>
        public static void InsertLog(int LogTypeID, int Severity, string Message,
            string Exception, string IPAddress, int CustomerID, string PageURL, DateTime CreatedOn)
        {
            string fileName = "ErrorLog_" + System.DateTime.Now.Date.ToShortDateString();
            fileName = fileName.Replace("/", "-");
            string fileExtension = "txt";
            string contents = "\r\n" + System.DateTime.Now + "\r\n" + LogTypeID.ToString() + " " + Severity.ToString() + " " + Message + " " + Exception + " " + IPAddress + " " + CustomerID.ToString() + " " + PageURL + " " + CreatedOn.ToString();
            string filePath = "";
            string mainDirectoryPath = "~/SiteFiles/ErrorLog";

            filePath = (string)AppDomain.CurrentDomain.GetData(mainDirectoryPath + "\\" + fileName + "." + fileExtension);

            //FileStream fs;
            //if (!File.Exists(filePath))
            //{
            //    fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
            //}
            //else
            //{
            //    fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);

            //}
            //StreamWriter sw = new StreamWriter(fs);

            //sw.Write(contents);
            //sw.Flush();
            //sw.Close();
            //fs.Close();


        }

        /// <summary>
        /// Gets this page name
        /// </summary>
        /// <returns></returns>
        //public static string GetThisPageURL(bool includeQueryString)
        //{
        //    string URL = string.Empty;
        //    if (HttpContext.Current == null)
        //        return URL;

        //    //if (includeQueryString)
        //    //{
        //    //    string storeHost = GetStoreHost(false);
        //    //    if (storeHost.EndsWith("/"))
        //    //        storeHost = storeHost.Substring(0, storeHost.Length - 1);
        //    //    URL = storeHost + HttpContext.Current.Request.RawUrl;
        //    //}
        //    //else
        //    //{
        //    URL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
        //    //}
        //    return URL;
        //}

        #endregion

        #region Custom Properties

        ///// <summary>
        ///// Gets the customer
        ///// </summary>
        //public Customer Customer
        //{
        //    get
        //    {
        //        return CustomerManager.GetCustomerByID(CustomerID);
        //    }
        //}

        /// <summary>
        /// Gets the log type
        /// </summary>
        public LogTypeEnum LogType
        {
            get
            {
                return (LogTypeEnum)LogTypeID;
            }
        }
        #endregion

        public void ProcessException(Exception exc, LogTypeEnum LogType)
        {
            LogManager.InsertLog(LogType, exc.Message, exc);
        }
    }

}
