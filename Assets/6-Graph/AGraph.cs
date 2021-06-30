using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Targil6
{
	[Serializable]
	public class AGraph
	{
		List<AEdge> graphEdges = new List<AEdge>();
		List<ANode> graphNodes = new List<ANode>();
		List<ANode> pathList = new List<ANode>();

		public AGraph() { }

		public void AddNode(Vector3 pos, int id)
		{
			ANode node = new ANode(pos, id);
			graphNodes.Add(node);

		}
		public void AddNode(GameObject go, int id)
		{
			ANode node = new ANode(go, id);
			graphNodes.Add(node);

		}

		public void AddEdge(int from, int to)
		{
			//TODO: Make sure nodes exist...
			AddEdge(GetNodeById(from), GetNodeById(to));
		}

		public void AddEdge(ANode from, ANode to, bool twoWay = true)
		{
			AEdge edge = new AEdge(from, to);
			graphEdges.Add(edge);
			from.edgesFromMe.Add(edge);
			if (twoWay)
			{
				AEdge edge2 = new AEdge(to, from);
				graphEdges.Add(edge2);
				to.edgesFromMe.Add(edge2);
			}
		}

		public void AddEdge(GameObject fromNode, GameObject toNode)
		{
			ANode from = GetNodeByGameObject(fromNode);
			ANode to = GetNodeByGameObject(toNode);

			if (from != null && to != null)
			{
				AEdge e = new AEdge(from, to);
				graphEdges.Add(e);
				from.edgesFromMe.Add(e);
			}
		}

		public ANode GetNodeById(int id)
		{
			foreach (ANode node in graphNodes)
			{
				if (node.id == id)
					return node;
			}
			return null;
		}

		public ANode GetNodeByGameObject(GameObject go)
		{
			foreach (ANode node in graphNodes)
			{
				if (node.gameObject == go)
					return node;
			}
			return null;
		}


		public int GetPathLength()
		{
			return pathList.Count;
		}

		public void PrintPath()
		{
			foreach (ANode node in pathList)
			{
				Debug.Log(node.id);
			}
		}


		public List<ANode> CalculateAStar(ANode start, ANode end)
		{
			List<ANode> openList = new List<ANode>();
			List<ANode> closedList = new List<ANode>();
			float newG = 0;
			bool shouldUpdateG;

			start.g = 0;
			float distance = Vector3.Distance(start.position, end.position);
			start.h = Mathf.Pow(distance, 2);
			start.f = start.h;
			openList.Add(start);

			while (openList.Count > 0)
			{
				int i = CalcLowestF(openList);
				ANode current = openList[i];
				if (current.id == end.id)  //path found
				{

					return BuildPath(start, end);
				}

				openList.RemoveAt(i);
				closedList.Add(current);

				ANode neighbour;
				foreach (AEdge e in current.edgesFromMe)
				{
					neighbour = e.to;
					/*neighbour.g = current.g + 
						Mathf.Pow(Vector3.Distance(current.position,neighbour.position),2);
					*/
					if (closedList.Contains(neighbour))
						continue;

					newG = current.g +
						Mathf.Pow(Vector3.Distance(current.position, neighbour.position), 2);

					if (!openList.Contains(neighbour))
					{
						openList.Add(neighbour);
						shouldUpdateG = true;
					}
					else if (newG < neighbour.g)
					{
						shouldUpdateG = true;
					}
					else
					{
						shouldUpdateG = false;
					}

					if (shouldUpdateG)
					{
						neighbour.originInPath = current;
						neighbour.g = newG;
						neighbour.h = Mathf.Pow(Vector3.Distance(current.position, end.position), 2);
						neighbour.f = neighbour.g + neighbour.h;
					}
				}

			}

			return null;
		}

		public List<ANode> BuildPath(ANode startId, ANode endId)
		{
			pathList.Clear();
			pathList.Add(endId);

			var p = endId.originInPath;
			while (p != startId && p != null)
			{
				pathList.Insert(0, p);
				p = p.originInPath;
			}
			pathList.Insert(0, startId);
			return pathList;
		}



		int CalcLowestF(List<ANode> l)
		{
			float lowestf = 0;
			int count = 0;
			int index = 0;

			for (int i = 0; i < l.Count; i++)
			{
				if (i == 0)
				{
					lowestf = l[i].f;
					index = 0;
				}
				else if (l[i].f <= lowestf)
				{
					lowestf = l[i].f;
					index = count;
				}
				count++;
			}
			return index;
		}

		public void DebugDraw()
		{
			//draw edges
			for (int i = 0; i < graphEdges.Count; i++)
			{
				Debug.DrawLine(graphEdges[i].from.gameObject.transform.position, graphEdges[i].to.gameObject.transform.position, Color.red);

			}
			//draw directions
			for (int i = 0; i < graphEdges.Count; i++)
			{
				Vector3 to = (graphEdges[i].from.gameObject.transform.position - graphEdges[i].to.gameObject.transform.position) * 0.05f;
				Debug.DrawRay(graphEdges[i].to.gameObject.transform.position, to, Color.blue);
			}
		}

		public List<ANode> CalculateAStar(GameObject startId, GameObject endId)
		{
			ANode start = GetNodeByGameObject(startId);
			ANode end = GetNodeByGameObject(endId);

			return CalculateAStar(start, end);
		}


		public override string ToString()
		{
			string result = "Graph: ";
			Debug.Log(graphNodes.Count);
			foreach (ANode node in graphNodes)
			{
				result += Environment.NewLine + "node " + node.id + "(" + node.position + ")";
				foreach (AEdge edge in node.edgesFromMe)
				{
					result += Environment.NewLine + "..." + edge.from.id + "->" + edge.to.id;
				}
			}
			Debug.Log(result);
			return result;
		}
	}
}