using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Panappta {
    /// <summary>
    /// Parses the Panappta Feed.
    /// </summary>
    public class XmlParser {
        /// <summary>
        /// Maintains our structure for the data.
        /// </summary>
        public struct XmlItems {
            public string Title;
            public string FullTitle;
            public string URL;
            public DateTime StartDate;
            public DateTime LastUpdate;
            public ServerType Type;
            public string DownTime;
            public string Summary;
        }

        /// <summary>
        /// Holds our ServerType. 
        /// </summary>
        public enum ServerType {
            Web_Server,
            Email_Server,
            Spool,
            Unknown
        }

        /// <summary>
        /// Downloads and parses the XML file. 
        /// </summary>
        public void Read() {
            bool bError = false; // If this is false, we were unable to grab the XML
            OutageItems = new List<XmlItems>(); // Our object
            XmlReader xml = null; // Our XML object to parse data
            XmlItems tmp; // Our temp XML Items object

            try {
                //xml = XmlReader.Create("S:\\test2.xml");
                xml = XmlReader.Create("http://feeds.panopta.com/active_outages/?c=xOmmomFveAQcBRX5v3TAJhKxjn2Sl"); // Attempt to pull the XML
            } catch (Exception) { // If the XML is unable to be retrieved, we set bError to true. 
                bError = true;
            }

            if (xml != null && !bError) // Only process if the XML object is NOT null and bError is fasle
                while (xml.Read()) { // Loop through each item in the xml tree
                    tmp = new XmlItems();
                    try {
                        if (xml.ReadToFollowing("entry")) {
                            if (xml.ReadToFollowing("title")) {
                                tmp.FullTitle = xml.ReadElementContentAsString(); // Grab the title
                                tmp.Title = ParseTitle(tmp.FullTitle, out tmp.Type); // Parse the title, return type as well
                            }

                            if (xml.ReadToFollowing("updated")) // Parse the updated 
                                tmp.LastUpdate = ParseDate(xml.ReadElementContentAsString());

                            if (xml.ReadToFollowing("link")) {
                                xml.MoveToFirstAttribute(); // Move to first item (attr)
                                xml.MoveToNextAttribute(); // Move to second (the url)
                                tmp.URL = xml.Value.Replace("//outage", "/outage"); // Fix for Regan! 
                            }

                            if (xml.ReadToFollowing("published")) // Grab the published date
                                tmp.StartDate = ParseDate(xml.ReadElementContentAsString());

                            if (xml.ReadToFollowing("summary")) { // Grab the summary
                                try { // Dirty, horrible, noobish quick fix. If summary doesn't exist, it crashes. But it's safe to ignore.
                                    tmp.Summary = xml.ReadElementContentAsString();
                                } catch (Exception) {
                                    tmp.Summary = null;
                                }
                            }


                            // Calculate our downtime day/hour/min/sec
                            tmp.DownTime = string.Format("{0} day(s), {1} hour(s), {2} minute(s), and {3} second(s)",
                                DateTime.Now.Subtract(tmp.StartDate).Days,
                                DateTime.Now.Subtract(tmp.StartDate).Hours,
                                DateTime.Now.Subtract(tmp.StartDate).Minutes,
                                DateTime.Now.Subtract(tmp.StartDate).Seconds);
                        }
                    } catch (Exception) { // This should almost never, ever, ever, happen. Just for safe measures because of lazyiness.

                    }
                    OutageItems.Add(tmp); // Add the item to our list. 
                }

            if (xml != null) // Cleanup our XML object if it's not null
                xml.Close(); // Close the XML

            xml = null; // Kill the object for safe measure
        }

        /// <summary>
        /// Parses the title to make it clean.
        /// </summary>
        /// <param name="strValue">The value to parse.</param>
        /// <param name="type">Returns the server/error type (http, email, spool)</param>
        /// <returns></returns>
        string ParseTitle(string strValue, out ServerType type) {
            string result = null;
            type = ServerType.Web_Server;

            if (strValue.Contains(":")) // Remove anything after : 
                result = strValue.Substring(0, strValue.IndexOf(":"));
            else // Fix: This prevents a null reference.
                result = strValue;

            if (strValue.ToLower().Contains("http")) // Check if it's a webserver (http)
                type = ServerType.Web_Server;

            if (strValue.ToLower().Contains("email")) // Check if it's email
                type = ServerType.Email_Server;

            if (strValue.ToLower().Contains("spool")) // Check if it's spool
                type = ServerType.Spool;

            return result;
        }

        /// <summary>
        /// Converts a string to a date/time.
        /// </summary>
        /// <param name="strValue">The value to convert.</param>
        /// <returns></returns>
        DateTime ParseDate(string strValue) {
            DateTime result = DateTime.Now;
            try {
                result = Convert.ToDateTime(strValue);
            } catch (Exception) { // Something REALLY weird happened. This should almost never happen, ever.
                result = DateTime.Now;
            }
            return result;
        }

        /// <summary>
        /// Exposes the OutageItems list.
        /// </summary>
        public List<XmlItems> OutageItems {
            set;
            get;
        }
    }
}
