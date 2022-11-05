using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Path_Finding
{
    internal class Dijkstra : IGraphSearch
    {
        private List<NodeInfo> _visitedNodes;
        private List<NodeInfo> _nodeQueue;
        private List<Edge> _shortestPathTree;


        //Enables sorting of nodes in a list 

        class NodeInfo : IComparable<NodeInfo>
        {
            public NodeInfo(int pID, float pCostToNode)
            {
                ID = pID;
                LowestCostToNode = pCostToNode;
            }

            public int ID { get; private set; }
            public float LowestCostToNode { get; set; }

            public int CompareTo(NodeInfo pOther)
            {
                if (LowestCostToNode > pOther.LowestCostToNode)
                    return -1;
                else if (LowestCostToNode < pOther.LowestCostToNode)
                    return 1;
                return 0;
            }
        }

        private Graph _graph;

        private float _timeTillStep;

        public float stepInterval { get; set; } = 0.5f;
        
        public int From { get; private set; }
        public int To { get; private set; }
        
        public bool IsFinished { get; private set; }

        public List<Edge> ShortestPath { get { return _shortestPathTree; } }

        public Dijkstra(Graph pGraph, int pFrom, int pTo)
        {
            _graph = pGraph;

            From = pFrom;
            To = pTo;
            _timeTillStep = stepInterval;
            _visitedNodes = new List<NodeInfo>(_graph.NodeCount);
            _nodeQueue = new List<NodeInfo>(_graph.NodeCount);
            _nodeQueue.Add(new NodeInfo(From, 0));
            _shortestPathTree = new List<Edge>();

            IsFinished = false;
        }

        public void Update(float pSeconds)
        {
            if (IsFinished)
            {
                return;
            }

            _timeTillStep -= pSeconds;

            if (_timeTillStep > 0)
            {
                return;
            }

            _timeTillStep = stepInterval;


            //if nodes in node queue sort the queue.
            //this will select the closest node - this taken from list and treat it like priority queue

            if(_nodeQueue.Count > 0)
            {
                _nodeQueue.Sort();
                NodeInfo currentNode = _nodeQueue[_nodeQueue.Count - 1];
                _nodeQueue.RemoveAt(_nodeQueue.Count - 1); 

                // next see if this is our target node
                // if yes, add to list visitedNodes 
                // finish search

                if(currentNode.ID == To)
                {
                    _visitedNodes.Add(currentNode);
                    IsFinished = true;
                }


                // search through all edges to find any edges that start or end with found node
                foreach(Edge edge in _graph.Edges)
                {
                    int candidateID = -1;

                    // if current edge begins or ends with current node
                    //begins>?
                    if(edge.To == currentNode.ID)
                    {
                        candidateID = edge.From;
                    }
                    //ends>?
                    else if(edge.From == currentNode.ID)
                    {
                        candidateID = edge.To;
                    }

                    // checks if node is already in visitedNode List or nodeQueue List

                    if(candidateID >= 0)
                    {

                        // visitedNode List
                        bool visited = false;

                        foreach( NodeInfo visitedNode in _visitedNodes)
                        {
                            if(visitedNode.ID == candidateID)
                            {
                                visited = true;
                            }
                        }

                        // nodeQueue List
                        bool queued = false;

                        for(int i =0; i< _nodeQueue.Count; i++)
                        {
                            if(_nodeQueue[i].ID == candidateID)
                            {
                                queued = true;

                                // if node in queue it could be a faster way to get to that node,
                                // so we need to update the lowest cost to reach that node and the SPT
                                
                                float newCost = currentNode.LowestCostToNode + _graph.GetEdgeCost(edge.ID);
                                if (_nodeQueue[i].LowestCostToNode > newCost)
                                {
                                    _nodeQueue[i].LowestCostToNode = newCost;

                                    for(int j=0; j < _shortestPathTree.Count; j++)
                                    {
                                        if(_shortestPathTree[i].To == candidateID)
                                        {
                                            _shortestPathTree.RemoveAt(i);
                                            _shortestPathTree.Add(new Edge(currentNode.ID, candidateID));
                                        }
                                    }
                                }
                            }
                        }

                        // if node not visited or queued add node to queue and SPT
                        // adding node to queue assign cost
                        // cost in this case = lowest cost of current node (node which was lead from past node)  + cost of edge from currentNode (lenght) to other node

                        if(!queued && !visited)
                        {
                            NodeInfo newNode = new NodeInfo(candidateID, currentNode.LowestCostToNode + _graph.GetEdgeCost(edge.ID));
                            _nodeQueue.Add(newNode);
                            _shortestPathTree.Add(new Edge(currentNode.ID,candidateID));   

                        }

                        _visitedNodes.Add(currentNode);
                    }
                }

               
            }








            // new code goes here!
        }

        public void DrawShapes(ShapeBatcher pShapeBatcher)
        {
            for (int i = 0; i < _shortestPathTree.Count; i++)
            {
                int From = _shortestPathTree[i].From;
                int To = _shortestPathTree[i].To;

                pShapeBatcher.DrawLine(_graph.GetNode(From).Position, _graph.GetNode(To).Position, 2, Color.Green);
            }

            for (int i = 0; i < _visitedNodes.Count; i++)
            {
                pShapeBatcher.DrawFilledCircle(_graph.GetNode(_visitedNodes[i].ID).Position, 20, 32, Color.Orange);
            }

            for (int i = 0; i < _nodeQueue.Count; i++)
            {
                pShapeBatcher.DrawFilledCircle(_graph.GetNode(_nodeQueue[i].ID).Position, 20, 32, Color.Blue);
            }
        }

        public void DrawSprites(SpriteBatch pSpriteBatcher, SpriteFont pFont, float pHeight)
        {
            for (int i = 0; i < _visitedNodes.Count; i++)
            {
                string label;
                if (_visitedNodes[i].LowestCostToNode > 1000000)
                {
                    label = "INF";
                }
                else
                {
                    label = MathF.Round(_visitedNodes[i].LowestCostToNode, 1).ToString();
                }
                Vector2 position = _graph.GetNode(_visitedNodes[i].ID).Position;
                position = position.FlipY(pHeight) - pFont.MeasureString(label) * 0.5f;
                pSpriteBatcher.DrawString(pFont, label, position, Color.Black);
            }

            for (int i = 0; i < _nodeQueue.Count; i++)
            {
                string label;
                if (_nodeQueue[i].LowestCostToNode > 1000000)
                {
                    label = "INF";
                }
                else
                {
                    label = MathF.Round(_nodeQueue[i].LowestCostToNode, 1).ToString();
                }
                Vector2 position = _graph.GetNode(_nodeQueue[i].ID).Position;
                position = position.FlipY(pHeight) - pFont.MeasureString(label) * 0.5f;
                pSpriteBatcher.DrawString(pFont, label, position, Color.Black);
            }
        }
    }
}
