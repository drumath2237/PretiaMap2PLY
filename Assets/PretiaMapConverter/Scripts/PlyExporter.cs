using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PretiaMapConverter
{
    public class PlyExporter
    {
        public static byte[] ConvertPointCloudToPlyBinary(List<Vector3> pointCloud)
        {
            var str = "ply\nformat little_endian";
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);


            foreach (var c in str.ToCharArray())
            {
                var code = (int)c;
            }

            return bytes;
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