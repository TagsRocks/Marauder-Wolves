using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Tools.Curves
{
    public abstract class Curve : MonoBehaviour
    {
        public abstract List<Vector3> GetCurve();
    }
}
