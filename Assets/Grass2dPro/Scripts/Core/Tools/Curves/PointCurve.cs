using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Tools.Curves
{
    public class PointCurve : Curve
    {
        public List<Vector3> Points = new List<Vector3>{new Vector3(-1,0,0), new Vector3(1,0,0)}; 

        public override List<Vector3> GetCurve()
        {
            return Points;
        }
    }
}
