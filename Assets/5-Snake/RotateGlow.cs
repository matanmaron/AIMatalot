using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Targil5
{
    public class RotateGlow : MonoBehaviour
    {
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, 1), 1);
        }
    }
}