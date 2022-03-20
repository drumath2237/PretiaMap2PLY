using System;
using System.Collections.Generic;
using System.Linq;
using PretiaArCloud.Pcx;
using UnityEngine;
using Utf8Json;

namespace PretiaMapConverter
{
    public class MapDataConverter
    {
        public static (bool isSuccess, Exception err) TryGetPointCloudDataFromMap(
            string mapData,
            out List<Vector3> pointCloud
        )
        {
            var pretiaMapData = JsonSerializer.Deserialize<PretiaPointCloudData>(mapData);

            if (pretiaMapData == null)
            {
                pointCloud = null;
                return (false, new InvalidOperationException("invalid map data"));
            }

            pointCloud = pretiaMapData
                .GetVertices()
                .Select(point => new Vector3(point.x, -point.y, point.z))
                .ToList();
            return (true, null);
        }
    }
}