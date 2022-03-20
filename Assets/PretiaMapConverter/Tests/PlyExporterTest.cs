using NUnit.Framework;

namespace PretiaMapConverter.Tests
{
    public class PlyExporterTest
    {
        [Test]
        public void TestExporter()
        {
            PlyExporter.ConvertPointCloudToPlyBinary(null);
        }
    }
}