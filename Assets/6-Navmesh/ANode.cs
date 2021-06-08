using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Targil6
{
    [Serializable]
    public class ANode
    {
        public int id;
        public List<AEdge> edgesFromMe = new List<AEdge>();

        public ANode originInPath = null;
        public ANode path = null;

        public GameObject gameObject;
        public Vector3 position;

        public float f;
        public float g;
        public float h;

        public ANode(Vector3 pos, int i)
        {
            gameObject = null;
            id = i;
            position = pos;
            path = null;
        }

        public ANode(GameObject go, int i)
        {
            gameObject = go;
            id = i;
            position = go.transform.position;
            path = null;
        }

        public ANode(float x, float y, float z, int i)
        {
            gameObject = null;
            id = i;
            position = new Vector3(x, y, z);
            path = null;
        }
    }
}