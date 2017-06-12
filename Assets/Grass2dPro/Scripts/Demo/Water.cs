using Scripts.Core.Tools.Render;
using UnityEngine;

namespace Scripts.Demo
{
    [ExecuteInEditMode]
    public class Water : MonoBehaviour
    {
        public LineMesh line;

        private float a = 0f;

        private void Start()
        {
            line.SetVertexCount(10);
        }

        private void Update()
        {
            a += 0.05f;
            for (int i = 0; i < 10; i++)
            {
                line.SetPosition(i, new Vector3(i*1, Mathf.Cos(i*15f + a) *0.1f));
            }
        }

        private void OnTriggerStay2D(Collider2D other) 
        {
            //other.rigidbody2D.velocity = new Vector2(other.rigidbody2D.velocity.x, other.rigidbody2D.velocity.y + 0.5f);
        }        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<Rigidbody2D>().gravityScale = -0.5f;
            other.GetComponent<Rigidbody2D>().drag = 3;
        }
        
        private void OnTriggerExit2D(Collider2D other) 
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
            other.GetComponent<Rigidbody2D>().drag = 0;
        }
    }
}
