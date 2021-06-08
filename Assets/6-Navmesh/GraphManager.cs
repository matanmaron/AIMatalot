using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Targil6
{
    public class GraphManager : MonoBehaviour
    {
        public GameObject lamberjack;
        public GameObject start;
        public GameObject end;
        public List<GameObject> aNodes;
        public List<GameObjects> aEdges;
        private List<GameObject> lines = new List<GameObject>();
        List<ANode> foundPath = null;
        bool begin = false;
        bool walk = false;
        public TextMeshProUGUI txt;

        private void Update()
        {
            if (!begin && Input.GetKeyUp(KeyCode.Space))
            {
                begin = true;
                Astar();
                txt.text = "Press SPACE to walk the path";
                return;
            }
            else if (!walk && Input.GetKeyUp(KeyCode.Space))
            {
                var jack = Instantiate(lamberjack);
                var wp = jack.GetComponent<WaypointMove>();
                var wplist = new List<GameObject>();
                foreach (var path in foundPath)
                {
                    wplist.Add(path.gameObject);
                }
                wp.waypoints = wplist.ToArray();
                jack.transform.position = wplist[0].transform.position;
                walk = true;
            }
        }

        private void Astar()
        {
            AGraph myGraph = new AGraph();
            int i = 0;
            foreach (var node in aNodes)
            {
                myGraph.AddNode(node, i++);
            }
            foreach (var edge in aEdges)
            {
                myGraph.AddEdge(edge.From, edge.To);
                var line = new GameObject();
                var lineRenderer = line.AddComponent<LineRenderer>();
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.blue;
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.SetPosition(0, edge.From.transform.position);
                lineRenderer.SetPosition(1, edge.To.transform.position);
                lines.Add(line);
            }

            Debug.Log(myGraph);
            start.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            end.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            foundPath = myGraph.CalculateAStar(start, end);
            if (foundPath == null)
            {
                Debug.Log("No path...");
            }
            else
            {
                Debug.Log("Found path: ");
                myGraph.PrintPath();
                foreach (var path in foundPath)
                {
                    if (path.gameObject == start || path.gameObject == end)
                    {
                        continue;
                    }
                    path.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                }
            }
        }
    }
}