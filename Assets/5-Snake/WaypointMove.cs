using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Targil5
{
    public class WaypointMove : MonoBehaviour
    {
        private Transform goal;
        public float speed = 0.075f;
        public float angleSpeed = 0.075f;
        public float distance = 1.5f;
        public GameObject[] waypoints;
        int nextWaypoint = 0;

        void Start()
        {
            waypoints = GameObject.FindGameObjectsWithTag("waypoints");
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
                Vector3 pushVector = direction.normalized * speed;
                transform.Translate(pushVector, Space.World);
            }
            else
            {
                nextWaypoint++;
                if (nextWaypoint >= waypoints.Length)
                {
                    nextWaypoint = 0;
                }
                // nextWaypoint = Random.Range(0, waypoints.Length);
            }
        }
    }
}