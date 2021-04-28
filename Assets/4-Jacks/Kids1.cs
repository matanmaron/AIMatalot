using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Targil4
{
    public class Kids1 : MonoBehaviour
    {
        public GameObject prefab;
        GameObject[] jacks;
        public GameObject goal;
        const int NUM_OF_JACKS = 100;
        const int NUM_OF_CIRCLES = 5;
        const int RADIUS = 20;

        void Start()
        {
            for (int k = 0; k < NUM_OF_CIRCLES; k++)
            {
                jacks = new GameObject[NUM_OF_JACKS];
                for (int i = 0; i < NUM_OF_JACKS; i++)
                {
                    jacks[i] = Object.Instantiate(prefab);
                    jacks[i].transform.RotateAround(Vector3.zero, Vector3.up, i * (Random.Range(1, 361)));
                    jacks[i].transform.Translate(RADIUS + 3 * k + Random.Range(1, k + 1), 0, 0, Space.Self);
                    jacks[i].transform.LookAt(goal.transform);
                    jacks[i].GetComponent<Move>().goal = goal.transform;
                    jacks[i].GetComponent<Move>().speed = 0.02f;
                    jacks[i].GetComponent<Move>().distance = 5 + k;
                }
            }

        }
    }
}