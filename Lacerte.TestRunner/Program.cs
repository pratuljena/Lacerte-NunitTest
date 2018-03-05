using System.Xml;

namespace Lacerte.TestRunner
{
    class Program
    {
        static string AssemblyPath = @"C: \Users\pjena\source\repos\Test.Nunit3\FeatureTest\bin\Debug\FeatureTest.dll";

        static void Main(string[] args)
        {
            TestDriver testDriver = new TestDriver(AssemblyPath);
            XmlNode result = testDriver.RunTest();
            ResultFormater resultFormater = new ResultFormater();
            resultFormater.CreateXMLResult(result);
            resultFormater.GenerateTestReport(result);
        }
    }
}
