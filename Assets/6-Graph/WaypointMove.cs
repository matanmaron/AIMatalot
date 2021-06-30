using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Targil6
{
    public class WaypointMove : MonoBehaviour
    {
        private Transform goal;
        public float speed = 0.075f;
        public float angleSpeed = 0.075f;
        public float distance = 1.5f;
        public GameObject[] waypoints;
        int nextWaypoint = 0;
        GameObject Glow;

        void Start()
        {
            Glow = GameObject.Find("GlowHolder");
            for (int i = 0; i < waypoints.Length; i++)
            {
                Debug.Log(waypoints[i].name + " " + waypoints[i].transform.position);
            }
        }

        // Update is called once per frame
        void Update()
        {
            //GetComponent<Animator>().SetBool("near", false);
            goal = waypoints[nextWaypoint].transform;

            Vector3 realGoal = new Vector3(goal.position.x,
                transform.position.y, goal.position.z);

            Vector3 direction = realGoal - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), angleSpeed);

            Debug.DrawRay(transform.position, direction, Color.green);
            if (direction.magnitude >= distance)
            {
                Glow.transform.position = waypoints[nextWaypoint].transform.position;
                Vector3 pushVector = direction.normalized * speed;
                transform.Translate(pushVector, Space.World);
            }
            else
            {
                nextWaypoint++;
                if (nextWaypoint >= waypoints.Length)
                {
                    Destroy(this);
                    return;
                }
                Glow.transform.position = waypoints[nextWaypoint].transform.position;
            }
        }
    }
}