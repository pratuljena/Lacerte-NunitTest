using NUnit.Engine;
using System;
using System.Reflection;
using System.Xml;

namespace Lacerte.TestRunner
{
    class TestDriver
    {
       
        public string AssemblyPath { get; set; }
        public ITestRunner TestRunner { get; set; }
        public TestFilter EmptyFilter { get; set; }

        public TestDriver(string path)
        {
            TestPackage package = new TestPackage(path);
            ITestEngine engine = TestEngineActivator.CreateInstance();
            var _filterService = engine.Services.GetService<ITestFilterService>();
            ITestFilterBuilder builder = _filterService.GetTestFilterBuilder();
            EmptyFilter = builder.GetFilter();
            TestRunner = engine.GetRunner(package);
        }

        public XmlNode RunTest()
        {
            return TestRunner.Run(null, EmptyFilter); ;
        }
        
    }
}
