using System;
using System.IO;
using System.Xml;

namespace Lacerte.TestRunner
{
    class ResultFormater
    {
        public string ResultFile { get; set; }
        public string ResultLog { get; set; }

        public ResultFormater()
        {
            string mydocumentspath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ResultFile = mydocumentspath + "\\result1.xml";
            ResultLog = mydocumentspath + "\\result1.log";
        }
        public void CreateXMLResult(XmlNode result)
        {
            // Delete the result xml file if already exists
            if (File.Exists(ResultFile))
                File.Delete(ResultFile);

            // Store the result into a xml file for refrence
            using (StreamWriter fs = File.AppendText(ResultFile))
            {
                fs.Write(result.InnerXml);
            }
        }
        public void GenerateTestReport(XmlNode result)
        {
            // Extract the test summary
            int totaltests = Convert.ToInt16(result.LastChild.Attributes["total"].Value);
            int passedtest = Convert.ToInt16(result.LastChild.Attributes["passed"].Value);
            int failedtest = Convert.ToInt16(result.LastChild.Attributes["failed"].Value);

            // Extract the detail test result and write into log
            XmlNode testcase = result.LastChild.LastChild.LastChild;

            if (File.Exists(ResultLog))
                File.Delete(ResultLog);

            //  Write the result into log file
            using (StreamWriter fs = File.AppendText(ResultLog))
            {
                fs.Write("\n #####    SUMMARY    ##### \n \n");
                fs.Write(string.Format("{0,-20} {1,-21}", "Total Tests", $" : {totaltests}"));
                fs.Write("\n");
                fs.Write(string.Format("{0,-20} {1,-21}", "Passed Tests", $" : {passedtest}"));
                fs.Write("\n");
                fs.Write(string.Format("{0,-20} {1,-21}", "Failed Tests", $" : {failedtest}"));
                fs.Write("\n\n");

                fs.Write("##### Test Case Detail ##### \n \n");
                fs.Write(string.Format("{0,-20} {1,-21}", "TestCase Name", "Result"));
                fs.Write("\n---------------------------- \n");

                for (int i = 1; i <= totaltests; i++)
                {
                    fs.Write(string.Format("{0,-20} {1,-21}", $"{i}. {testcase.ChildNodes[i].Attributes["methodname"].Value}", $" : {testcase.ChildNodes[i].Attributes["result"].Value}"));

                    // For failed tests write the failure message and stack trace
                    if (Convert.ToString(testcase.ChildNodes[i].Attributes["result"].Value).Equals("Failed"))
                    {
                        fs.Write("\n" + testcase.ChildNodes[i].FirstChild.FirstChild.InnerText + "\n");
                        fs.Write(testcase.ChildNodes[i].FirstChild.LastChild.InnerText);
                    }
                    fs.Write("\n");
                }
            }

        }
    }
}
