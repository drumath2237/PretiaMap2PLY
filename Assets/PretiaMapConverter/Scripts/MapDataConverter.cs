using System;
using System.Collections.Generic;
using PretiaArCloud.Pcx;
using UnityEngine;
using Utf8Json;

namespace PretiaMapConverter
{
    public class MapDataConverter
    {
        public static (bool isSuccess, Exception err) TryGetPointCloudDataFromMap(
            string mapData,
            out List<Vector3> pointCloud)
        {
            var pretiaMapData = JsonSerializer.Deserialize<PretiaPointCloudData>(mapData);
            
            if (pretiaMapData != null)
            {
                pointCloud = pretiaMapData.GetVertices();
                return (true, null);
            }

            pointCloud = null;
            return (false, new InvalidOperationException("invalid map data"));
        }
    }
}