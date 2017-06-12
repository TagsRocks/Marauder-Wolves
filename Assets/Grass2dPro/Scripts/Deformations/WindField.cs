using UnityEngine;

namespace Scripts.Deformations
{
    public class WindField : DeformationBase
    {
        public float Pover = 0.02f;

        private float a = 0.13f;
        private float a2 = 0.12f;
        private float a3 = 0.17f;

        private void Update()
        {
            a3 += 0.0015f;
            a2 += Mathf.Cos(a3)*0.001f;
            a += Mathf.Cos(a2)*Pover;
        }

        public override Vector3 ByPoint(Vector3 position)
        {
            var amplitude = Mathf.Cos(a + position.x*20);
            return new Vector3(amplitude, 0, 0);
        }
    }
}
