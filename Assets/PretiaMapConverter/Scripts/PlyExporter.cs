using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PretiaMapConverter
{
    public class PlyExporter
    {
        public static byte[] ConvertPointCloudToPlyBinary(List<Vector3> pointCloud)
        {
            var bytes = CreatePlyHeader(3);
            _ = OutputContent(bytes);


            return null;
        }

        private static async Task<bool> OutputContent(byte[] data)
        {
            var basePath = Path.Combine(Application.dataPath, "PretiaMapConverter/Maps");
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            var filePath = Path.Combine(basePath, "map.ply");

            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await fs.WriteAsync(data, 0, data.Length);

            return true;
        }

        private static byte[] CreatePlyHeader(int vertexCount)
        {
            var header = @$"ply
format binary_little_endian 1.0
element vertex {vertexCount}
property float x
property float y
property float z
end_header
";

            return Encoding.UTF8.GetBytes(header);
        }
    }
}