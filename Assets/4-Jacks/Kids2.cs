using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Targil4
{
    public class Kids2 : MonoBehaviour
    {
        public GameObject prefab;
        GameObject[] jacks;
        public GameObject goal;
        const int NUM_OF_JACKS = 20;
        const int FOLLOW_DELTA = 1;
        const int RADIUS = 10;

        void Start()
        {
            jacks = new GameObject[NUM_OF_JACKS];
            for (int i = 0; i < NUM_OF_JACKS; i++)
            {
                jacks[i] = Object.Instantiate(prefab);
                jacks[i].transform.RotateAround(Vector3.zero, Vector3.up, i * (360 / NUM_OF_JACKS));
                jacks[i].transform.Translate(RADIUS, 0, 0, Space.Self);
                jacks[i].GetComponent<Move>().speed = 0.03f;
                jacks[i].GetComponent<Move>().distance = 1;
            }
            jacks[0].GetComponent<Move>().goal = goal.transform;
            jacks[0].transform.LookAt(goal.transform);
        }

        void Update()
        {
            for (int i = 0; i < NUM_OF_JACKS; i++)
            {
                int toFollow = (i + FOLLOW_DELTA) % NUM_OF_JACKS;
                jacks[i].GetComponent<Move>().goal = jacks[toFollow].transform;
                jacks[i].transform.LookAt(jacks[toFollow].transform);
            }
        }
    }
}