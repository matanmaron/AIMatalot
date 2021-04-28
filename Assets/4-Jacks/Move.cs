using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Targil4
{
    public class Move : MonoBehaviour
    {
        public Transform goal;
        public float speed = 0.01f;
        public float rotationSpeed = 0.03f;
        public float distance = 2.5f;

        void Update()
        {
            GetComponent<Animator>().SetBool("near", false);
            Vector3 realGoal = new Vector3(goal.position.x,
                transform.position.y, goal.position.z);
            Vector3 direction = realGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), rotationSpeed);

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