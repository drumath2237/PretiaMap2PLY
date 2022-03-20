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
        public static byte[] ConvertPointCloudToPlyBinary(List<Vector3> pointCloud)
        {
            var dummy = new List<Vector3>();

            var random = new Random();
            for (var i = 0; i < 1000; i++)
            {
                var point = new Vector3((float)random.Next(100) / 100.0f, (float)random.Next(100) / 100.0f,
                    (float)random.Next(100) / 100.0f);
                dummy.Add(point);
            }

            var headerData = CreatePlyHeader(dummy.Count);
            var pointCloudData = CreatePlyPointCloudBytes(dummy);

            _ = OutputContent(headerData, pointCloudData);

            return null;
        }

        private static async Task<bool> OutputContent(byte[] header, byte[] body)
        {
            var basePath = Path.Combine(Application.dataPath, "PretiaMapConverter/Maps");
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            var filePath = Path.Combine(basePath, "map.ply");

            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await fs.WriteAsync(header, 0, header.Length);
            await fs.WriteAsync(body, 0, body.Length);

            return true;
        }

        public static byte[] CreatePlyPointCloudBytes(List<Vector3> pointCloud)
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