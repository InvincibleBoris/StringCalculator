using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace StringCalculator.LoggingInterface
{

    /// <summary>
    /// Factory class to get the appropriate ILogger based on what is specified in 
    /// the App.Config file
    /// </summary>
    public class LoggerFactory
    {

        #region Static Variables

        // reference to the ILogger object.  We'll get a reference the first time then keep it
        private static ILogger logger;

        // This variable is used as a lock for thread safety
        private static object lockObject = new object();

        #endregion


        public static ILogger GetLogger()
        {
            lock (lockObject)
            {
                if (logger == null)
                {
                    // This must be our first time, so look at the app.config to figure out what 
                    // ILogger class we need to spin up
                    string asm_name = ConfigurationManager.AppSettings["Logger.AssemblyName"];
                    string class_name = ConfigurationManager.AppSettings["Logger.ClassName"];

                    if (String.IsNullOrEmpty(asm_name) || String.IsNullOrEmpty(class_name))
                        throw new ApplicationException("Missing config data for Logger");

                    Assembly assembly = Assembly.LoadFrom(asm_name);
                    logger = assembly.CreateInstance(class_name) as ILogger;

                    if (logger == null)
                        throw new ApplicationException(
                            string.Format("Unable to instantiate ILogger class {0}/{1}",
                            asm_name, class_name));
                }
                return logger;
            }
        }
    }
}
