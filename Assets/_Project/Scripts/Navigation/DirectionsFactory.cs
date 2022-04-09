using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Directions;
using Mapbox.Unity;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Unity.MeshGeneration.Modifiers;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Navigation
{
    /// <summary>
    /// Class that created a navigation line which navigates the user to the asked destination.
    /// </summary>
    public class DirectionsFactory : MonoBehaviour
    {
        [SerializeField] private LineMeshModifier _lineMeshModifier;
        
        [SerializeField] private Material _material;

        [SerializeField] private Transform _navigatedObject;
        
        [SerializeField] private Transform _destination;

        private bool _initialized = false;

        private bool _mapInitialized = false;
        
        private AbstractMap _map;
        
        private Directions _directions;

        private GameObject _navigationLine;
        
        public void Initialize(AbstractMap map)
        {
            if (map != null)
            {
                _map = map;
                map.OnInitialized+=() => _mapInitialized = true;
            }

            _directions = MapboxAccess.Instance.Directions;
            
            _lineMeshModifier.SetProperties(new LineGeometryOptions());
            _lineMeshModifier.Initialize();
            _initialized = true;
        }
        
        private void Query()
        {
            Vector2d[] wp = new Vector2d[2];
            wp[0] = _navigatedObject.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
            wp[1] = _destination.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
            DirectionResource directionResource = new DirectionResource(wp, RoutingProfile.Walking)
            {
                Steps = true
            };
            _directions.Query(directionResource, HandleDirectionsResponse);
        }
        
        void HandleDirectionsResponse(DirectionsResponse response)
        {
            if (response == null || null == response.Routes || response.Routes.Count < 1)
            {
                return;
            }
            var meshData = new MeshData();
            var dat = new List<Vector3>();
            
            foreach (Vector2d point in response.Routes[0].Geometry)
            {
                dat.Add(Conversions.GeoToWorldPosition(point.x, point.y, _map.CenterMercator, 
                    _map.WorldRelativeScale).ToVector3xz());
            }
            
            var feat = new VectorFeatureUnity();
            feat.Points.Add(dat);
            
            _lineMeshModifier.Run(feat, meshData, _map.WorldRelativeScale);

            _navigationLine = CreateGameObject(meshData);
        }

        private GameObject CreateGameObject(MeshData data)
        {
            GameObject directionsGO = new GameObject("direction waypoint " + " entity");
            var mesh = directionsGO.AddComponent<MeshFilter>().mesh;
            mesh.subMeshCount = data.Triangles.Count;
            mesh.SetVertices(data.Vertices);
            for (int i = 0; i < data.Triangles.Count; i++)
            {
                var triangle = data.Triangles[i];
                mesh.SetTriangles(triangle, i);
            }
            for (int i = 0; i < data.UV.Count; i++)
            {
                var uv = data.UV[i];
                mesh.SetUVs(i, uv);
            }
            mesh.RecalculateNormals();
            directionsGO.AddComponent<MeshRenderer>().material = _material;
            return directionsGO;
        }

        public void CallQuery()
        {
            if (_initialized && _mapInitialized)
            {
                Query();
            }
        }

        public void DestroyNavigationLine()
        {
            if (_navigationLine == null)
            {
                return;
            }
            
            _navigationLine.Destroy();
            _navigationLine = null;
        }
    }
}
