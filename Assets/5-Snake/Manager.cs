using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Targil5
{
    public class Manager : MonoBehaviour
    {
        public GameObject prefab;
        GameObject[] jacks;
        public GameObject goal;
        const int NUM_OF_JACKS = 15;
        const int NUM_OF_CIRCLES = 2;
        const int RADIUS = 5;

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
                    goal = jacks[i];
                }
            }

        }
    }
}