using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Targil2
{
    public class Move : MonoBehaviour
    {
        public Transform goal;
        public float speed = 0.01f;
        public float distance = 1.5f;
        public TextMeshProUGUI txt;
        bool done = false;

        void Update()
        {
            GetComponent<Animator>().SetBool("near", false);
            Vector3 realGoal = new Vector3(goal.position.x,
                transform.position.y, goal.position.z);
            Vector3 direction = realGoal - transform.position;
            Debug.DrawRay(transform.position, direction, Color.green);
            if (direction.magnitude >= distance)
            {
                Vector3 pushVector = direction.normalized * speed;
                transform.Translate(pushVector, Space.World);
            }
            else
            {
                if (!done)
                {
                    txt.text += "DONE!";
                    done = true;
                }
                GetComponent<Animator>().SetBool("near", true);
            }
        }
    }
}