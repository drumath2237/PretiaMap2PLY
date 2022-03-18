using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace PretiaMapConverter.Tests
{
    public class MapDataTest
    {
        [Test]
        public void TestTryGetPointCloud()
        {
            const string dummyMapString = @"
{
    ""points"": [
        {
            ""pos"": [
                0.8327481673996817,
                1.098004865080084,
                2.0721062812379247
            ]
        },
        {
            ""pos"": [
                27.467811869918926,
                -5.880828693286802,
                0.38660625632018614
            ]
        },
        {
            ""pos"": [
                0.763541953108527,
                1.0657030286564193,
                2.6546483865325117
            ]
        }
    ]
}";

            var dummyPointCloud = new List<Vector3>()
            {
                new Vector3(0.8327481673996817f, 1.098004865080084f, 2.0721062812379247f),
                new Vector3(27.467811869918926f, -5.880828693286802f, 0.38660625632018614f),
                new Vector3(0.763541953108527f, 1.0657030286564193f, 2.6546483865325117f)
            };

            var pcxList = new List<Vector3>();

            var (isSuccess, _) = MapDataConverter.TryGetPointCloudDataFromMap(dummyMapString, out pcxList);
            
            Assert.That(isSuccess);
            Assert.That(pcxList.Equals(dummyPointCloud));
        }
    }
}