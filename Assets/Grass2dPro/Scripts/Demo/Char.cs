using UnityEngine;

namespace Scripts.Demo
{
    public class Char : MonoBehaviour 
    {
        private Rigidbody2D drop;

        private void Awake()
        {
            drop = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                drop.AddForce(Vector3.up * 200, ForceMode2D.Force);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                drop.AddForce(Vector3.left * 10, ForceMode2D.Force);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                drop.AddForce(Vector3.right * 5, ForceMode2D.Force);
            }

            if (transform.position.y < -6)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0;
                transform.position = new Vector3(5.5f, 0);
            }
        }
    }
}
