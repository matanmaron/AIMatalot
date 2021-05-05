using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Targil5
{
    public class Move : MonoBehaviour
    {
        public Transform goal;
        public float speed = 0.1f;
        public float angleSpeed = 0.1f;
        public float distance = 1.5f;

        void Update()
        {
            GetComponent<Animator>().SetBool("near", false);
            Vector3 realGoal = new Vector3(goal.position.x,
                transform.position.y, goal.position.z);

            Vector3 direction = realGoal - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), angleSpeed);

            Debug.DrawRay(transform.position, direction, Color.green);
            if (direction.magnitude >= distance)
            {
                Vector3 pushVector = direction.normalized * speed;
                transform.Translate(pushVector, Space.World);
            }
            else
            {
                GetComponent<Animator>().SetBool("near", true);
            }
        }
    }
}