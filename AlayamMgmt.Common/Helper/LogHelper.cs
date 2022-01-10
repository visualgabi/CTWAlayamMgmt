using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Helper
{
    public static class LogHelper
    {
         /************  LOG4NET METHODS  ******************/
        #region void WriteToLog(Levels level, string message, Type type)
        public static void WriteToLog(Level level, string message, string className, string MethodName)
        {
            string messageToLog = string.Format(message + " Class name & Method name: {0} {1}",
                                    className, MethodName);

            log(level, messageToLog, null, className);
        }
        public static void WriteToLog(Level level, string message)
        {
            log(level, message, null, null);
        }
        public static void WriteToLog(Level level, string message, Type type, string methodName)
        {
            string messageToLog = string.Format(message + " Class name: {0}, Method name: {1}",
                                    type.Name, methodName);

            log(level, messageToLog, type);
        }

        public static void WriteToLog(Level level, string message, System.Object obj, Type type, string methodName)
        {
            string messageToLog = string.Format(message + " Class name: {0}, Method name: {1} \n {2}",
                                    type.Name, methodName, parseObject(obj));

            log(level, messageToLog, type);
        }

        public static void WriteToLog(Level level, string message, System.Object obj, string className, string methodName)
        {
            string messageToLog = string.Format(message + " Class name: {0}, Method name: {1} \n {2}",
                                    className, methodName, parseObject(obj));

            log(level, messageToLog, null, className);
        }

        public static void WriteToLog(Level level, string message, System.Object obj, string classMethodName)
        {
            string messageToLog = string.Format(message + " Class name: {0}, Method name: {1} \n {2}",
                                    classMethodName, parseObject(obj));

            log(level, messageToLog, null, classMethodName);
        }
        #endregion

        #region void WriteToLog(Levels level, Exception exception, Type type)
        public static void WriteToLog(Level level, Exception exception, Type type, string methodName)
        {
            if (exception.Data["Logged"] == null)
            {
                string messageToLog = string.Format("Class name: {0}, Method name: {1} \n{2}",
                                        type.Name, methodName, parseException(exception));

                log(level, messageToLog, type);
                //to stop duplicate logging.
               // exception.Data["Logged"] = "true";
            }
        }

        public static void WriteToLog(Level level, Exception exception, string className, string methodName)
        {
            if (exception.Data["Logged"] == null)
            {
                string messageToLog = string.Format("Class name: {0}, Method name: {1} \n{2}",
                                        className, methodName, parseException(exception));

                log(level, messageToLog, null, className);
                //to stop duplicate logging.
                // exception.Data["Logged"] = "true";
            }
        }

        public static void WriteToLog(Level level, Exception exception, Type type, string methodName, string data )
        {
            if (exception.Data["Logged"] == null)
            {
                string messageToLog = string.Format("Class name: {0}, Method name: {1} \n{2}",
                                        type.Name, methodName, parseException(exception));

                log4net.GlobalContext.Properties["Data"] = data;
                log(level, messageToLog, type);
                //to stop duplicate logging.
                //exception.Data["Logged"] = "true";
            }
        }

        #endregion

        private static void log(Level level, string messageToLog, Type type, string ClassName = null)
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.ILog log = null;
            if (ClassName != null)
            {
                log = log4net.LogManager.GetLogger(ClassName);
            }
            else
            {
                log = log4net.LogManager.GetLogger(type);
            }

            switch (level)
            {
                case Level.Debug:
                    log.Debug(messageToLog);
                    break;
                case Level.Information:
                    log.Info(messageToLog);
                    break;
                case Level.Warnings:
                    log.Warn(messageToLog);
                    break;
                case Level.Error:
                    log.Error(messageToLog);
                    break;
                case Level.Fatal:
                    log.Fatal(messageToLog);
                    break;
            }
            log = null;
        }

        private static string parseException(Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Environment.NewLine);
            // stringBuilder.Append("============================================================================================================================================================================");
            stringBuilder.Append("Exception Type : " + ex.GetType().ToString());
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("Root Exception Message : " + ex.Message);
            stringBuilder.Append(Environment.NewLine);
            if (ex.InnerException != null)
            {
                if (ex.InnerException.Message != null)
                {   
                    stringBuilder.Append("Inner Exception Details : " + ex.InnerException.Message);
                }
            }

            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("Root Exception Stack Trace : " + ex.StackTrace);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("Root Exception Source : " + ex.Source);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("Root Exception Data Info : ");
            stringBuilder.Append("Logged-In User Window Id : " + System.Environment.UserName);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("Logged-In User DateTime : " + DateTime.Now);
            stringBuilder.Append(Environment.NewLine);
            // stringBuilder.Append("============================================================================================================================================================================");

            return stringBuilder.ToString();
        }

        private static string parseObject(System.Object obj)
        {
            // Display field names of type.        
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Environment.NewLine);
            // stringBuilder.Append("============================================================================================================================================================================");

            FieldInfo[] fi = obj.GetType().GetFields();
            foreach (FieldInfo field in fi)
            {
                stringBuilder.Append(string.Format(" {0} : {1} ", field.Name, field.GetValue(obj).ToString()));
            }

            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append(Environment.NewLine);
            PropertyInfo[] pi = obj.GetType().GetProperties();

            foreach (PropertyInfo prop in pi)
            {
                stringBuilder.Append(string.Format(" {0} : {1} ", prop.Name, prop.GetValue(obj) != null ? prop.GetValue(obj).ToString() : "null"));
            }
            stringBuilder.Append(Environment.NewLine);
            // stringBuilder.Append("============================================================================================================================================================================");
            stringBuilder.Append(Environment.NewLine);
            return stringBuilder.ToString();
        }
    }    
}
