using UnityEngine;

namespace Scripts.Core.Utils
{
    public class Math2Utils
    {
        public const float Pi2 = Mathf.PI*2;

        public static Vector2 ToCartesian(Vector2 center, float angle, float radius)
        {
            var nodeX = center.x + radius * Mathf.Cos(angle);
            var nodeY = center.y + radius * Mathf.Sin(angle);
            return new Vector2(nodeX, nodeY);
        }

        public static Vector3 GetNormal(Vector3[] points, int i)
        {
            Vector3 r;

            if (i == 0)
            {
                r = (points[i] - points[i + 1]).normalized;
            }
            else if (i == points.Length - 1)
            {
                r = (points[i - 1] - points[i]).normalized;
            }
            else
            {
                r = (points[i - 1] - points[i + 1]).normalized;
            }

            return new Vector3(r.y, -r.x);
        }
    }
}
