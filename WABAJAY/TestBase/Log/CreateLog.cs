using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestBase.Log
{
    public static class CreateLog
    {
        public static void CreateApplicationLog(string testDataIndex, string message)
        {
            string methodName = "";
            string className = "";

            GetClassAndMethodName(new StackTrace(), out methodName, out className);


            string testProjectFolder = ConfigurationManager.AppSettings["testProjectFolder"];
            string testResultslogFolder = Path.Combine(testProjectFolder, "Log_Folder");

            string testLogFile = Path.Combine(testResultslogFolder, "TestLog_" + DateTime.Today.ToString("dd-MM-yyyy") + ".txt");

            if (!Directory.Exists(testResultslogFolder))
                Directory.CreateDirectory(testResultslogFolder);

            if (!File.Exists(testLogFile))
            {
                CreateRecordFile(testLogFile, testDataIndex, message, methodName, className, FileMode.Create);

            }
            else
            {
                CreateRecordFile(testLogFile, testDataIndex, message, methodName, className, FileMode.Append);
            }
        }

        private static void CreateRecordFile(string resultFolderTxtFile, string index, string message, string methodName, string className, FileMode fileMode)
        {
            using (FileStream aFile = new FileStream(resultFolderTxtFile, fileMode, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(aFile))
            {
                sw.WriteLine("**********************************************************");
                sw.WriteLine();
                sw.WriteLine("Test Error for : " + index);
                sw.WriteLine("Error Time : " + DateTime.Now);
                sw.WriteLine("Error Class : " + methodName);
                sw.WriteLine("Error Method: " + className);
                sw.WriteLine("Error : " + message);
                sw.WriteLine();
                sw.WriteLine("**********************************************************");
                sw.WriteLine();
                sw.Close();
                aFile.Close();
            }
        }

        public static void GetClassAndMethodName(StackTrace stackTrace, out string methodName, out string className)
        {
            MethodBase method;

            if (stackTrace.GetFrame(1).GetMethod().Name == stackTrace.GetFrame(0).GetMethod().Name)
                method = stackTrace.GetFrame(2).GetMethod();
            else
                method = stackTrace.GetFrame(1).GetMethod();

            methodName = method.ReflectedType.Name;
            className = method.Name;
        }
    }
}
