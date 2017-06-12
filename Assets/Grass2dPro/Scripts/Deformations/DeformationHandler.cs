using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

namespace Scripts.Deformations
{
    public struct DeformItem
    {
        public Vector3 Point;
        public Vector3 Impulse;

        public DeformItem(Vector3 point, Vector3 impulse)
        {
            Point = point;
            Impulse = impulse;
        }
    }

    public class DeformationHandler : DeformationBase
    {
        [SerializeField] private bool autoRepair;

        private List<DeformItem> items = new List<DeformItem>();
        private int timer;

        private void OnCollisionEnter2D(Collision2D coll)
        {
            var item = new DeformItem
            {
                Point = coll.transform.position, 
                Impulse = -coll.relativeVelocity
            };

            items.Add(item);
        }

        private void OnCollisionStay2D(Collision2D coll)
        {
            if(timer > 0 || IsNearpoint(coll.transform.position))
                return;

            timer = 3;

            var item = new DeformItem
            {
                Point = coll.transform.position,
                Impulse = coll.gameObject.GetComponent<Rigidbody2D>().velocity * 3
            };

            items.Add(item);
        }

        private bool IsNearpoint(Vector3 point)
        {
            for (var i = 0; i < items.Count; i++)
            {
                var magnitude = (point - items[i].Point).sqrMagnitude;

                if (magnitude < Mathf.Pow(0.4f, 2))
                    return true;
            }

            return false;
        }

        public override Vector3 ByPoint(Vector3 position)
        {
            var result = Vector3.zero;

            foreach (var item in items)
            {
                var dist = Vector3.SqrMagnitude(position - item.Point);
                var pover = Mathf.Max(0, 1f - dist);

                result += item.Impulse*pover;
            }
            
            return result;
        }

        private void Update()
        {
            timer--;

            if(!autoRepair)
                return;

            for (var i = items.Count - 1; i >= 0; i--)
            {
                var item = items[i];
                if (item.Impulse.sqrMagnitude < 0.1f)
                {
                    items.Remove(item);
                    continue;
                }    
                    
                items[i] = new DeformItem(item.Point, item.Impulse*0.9f);
            }
        }
    }
}
