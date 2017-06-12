using System;
using System.Collections.Generic;
using Scripts.Core.Tools.Curves;
using Scripts.Core.Tools.Render;
using Scripts.Core.Utils;
using Scripts.Deformations;
using UnityEngine;

namespace Scripts.Grass
{
    public class GrassRenderer : LineMesh
    {
        private const float Accuracy = 0.5f;

        public Curve Curve;

        public DeformationBase[] Deformations;
        public float HardnessX = 0.1f;
        public float HardnessY = 0.1f;

        protected override void Update()
        {
            if (!Application.isPlaying && Curve != null)
            {
                var points = TotalPoints(Curve.GetCurve());
                SetVertexCount(points.Count);

                for (var i = 0; i < points.Count; i++)
                {
                    SetPosition(i, points[i]);
                }
            }

            base.Update();
        }

        private List<Vector3> TotalPoints(List<Vector3> points)
        {
            var result = new List<Vector3>();

            for (var i = 1; i < points.Count; i++)
            {
                var a = points[i - 1];
                var b = points[i];

                var magnitude = (b - a).magnitude;
                var stepsFloor = Mathf.FloorToInt(magnitude / Accuracy);
                var steps = Math.Max(stepsFloor, 1);
                var accuracy = magnitude / steps;
                var vector = (b - a).normalized;

                for (var j = 0; j < steps; j++)
                {
                    var c = a + vector * j * accuracy;
                    result.Add(c);
                }

                if (i == points.Count - 1)
                    result.Add(b);
            }

            return result;
        }

        protected override void CalculateVertices()
        {
            if (Positions == null)
                return;

            if (!Application.isPlaying || Vertices == null)
            {
                InitVertices();
            }

            for (var i = 0; i < Positions.Length; i++)
            {
                var index = i*2 + 1;
                var n = Math2Utils.GetNormal(Positions, i);
                var top = Positions[i] + n * Width;
                var d = top + Deformation(top);
                var v = Vertices[index];

                Vertices[index] = v + (d - v)*0.1f;
            }

            Mesh.vertices = Vertices;
        }

        private void InitVertices()
        {
            Vertices = new Vector3[Positions.Length * 2];

            for (var i = 0; i < Positions.Length; i++)
            {
                var n = Math2Utils.GetNormal(Positions, i);
                Vertices[i * 2] = Positions[i];
                Vertices[i * 2 + 1] = Positions[i] + n * Width;
            }
        }

        private Vector3 Deformation(Vector3 point)
        {
            var result = Vector3.zero;
            var p = transform.TransformPoint(point);

            for (var j = 0; j < Deformations.Length; j++)
            {
                var deformation = Deformations[j];
                if (deformation != null)
                {
                    var d = deformation.ByPoint(p);

                    var x = XCorect(d) * HardnessX;
                    var y = YCorect(d) * HardnessY; 

                    result += new Vector3(x, y);
                }
            }

            return result;
        }

        private float XCorect(Vector3 value)
        {
            return value.x;
        }

        private float YCorect(Vector3 value)
        {
            var diff = Mathf.Abs(value.x) * 0.1f;
            return Mathf.Min(0, value.y - diff);
        }
    }
}
