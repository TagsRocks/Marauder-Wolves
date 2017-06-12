using UnityEngine;

namespace Scripts.Deformations
{
    public abstract class DeformationBase : MonoBehaviour
    {
        public abstract Vector3 ByPoint(Vector3 position);
    }
}
