using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace PretiaMapConverter
{
    public class PlyExporter
    {
        public static async Task Export(string directoryPath, List<Vector3> pointCloud)
        {
            var headerData = CreatePlyHeader(pointCloud.Count);
            var pointCloudData = CreatePlyBodyFromVectors(pointCloud);

            await WritePlyDataAsync(directoryPath, headerData, pointCloudData);
        }

        private static async Task WritePlyDataAsync(string directoryPath, byte[] header, byte[] body)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, "map.ply");

            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await fs.WriteAsync(header, 0, header.Length);
            await fs.WriteAsync(body, 0, body.Length);
        }

        private static byte[] CreatePlyBodyFromVectors(IEnumerable<Vector3> pointCloud)
        {
            var floatArray = pointCloud.Aggregate(new List<float>(), (list, point) =>
            {
                list.Add(point.x);
                list.Add(point.y);
                list.Add(point.z);
                return list;
            }).ToArray();

            var byteArray = new byte[floatArray.Length * 4];
            Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);

            return byteArray;
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