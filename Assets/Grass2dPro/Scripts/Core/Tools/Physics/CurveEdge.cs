using Scripts.Core.Tools.Curves;
using UnityEngine;
#pragma warning disable 649

namespace Scripts.Core.Tools.Physics
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(EdgeCollider2D))]
    public class CurveEdge : MonoBehaviour
    {
        public Curve Curve;
        private EdgeCollider2D edge;

        private void Start()
        {
            edge = GetComponent<EdgeCollider2D>();
            UpdatePoints();
        }

        private void Update()
        {
            if (!Application.isEditor || Curve == null)
                return;

            UpdatePoints();
        }

        private void UpdatePoints()
        {
            if (Application.isPlaying || Curve == null)
                return;
            
            var points = Curve.GetCurve();
            var test = new Vector2[points.Count];

            for (var i = 0; i < points.Count; i++)
            {
                test[i] = points[i];
            }

            edge.points = test;
        }
    }
}
