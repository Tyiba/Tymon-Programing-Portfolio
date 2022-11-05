using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_Finding
{
    internal class AStarSearch : IGraphSearch
    {
        private List<NodeInfo> _visitedNodes;
        private List<NodeInfo> _nodeQueue;
        private List<Edge> _shortestPathTree;



        class NodeInfo : IComparable<NodeInfo>
        {        
            public float Heuristic { get; private set; }
            public NodeInfo(int pID, float pCostToNode, float pHeuristic)
            {
                ID = pID;
                LowestCostToNode = pCostToNode;
                Heuristic = pHeuristic;
            }

            public int ID { get; private set; }
            public float LowestCostToNode { get; set; }

            public int CompareTo(NodeInfo pOther)
            {
                if (LowestCostToNode + Heuristic > pOther.LowestCostToNode + pOther.Heuristic)
                    return -1;
                else if (LowestCostToNode + Heuristic < pOther.LowestCostToNode + pOther.Heuristic)
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

        public AStarSearch(Graph pGraph, int pFrom, int pTo)
        {
            _graph = pGraph;
            From = pFrom;
            To = pTo;
            _timeTillStep = stepInterval;
            _visitedNodes = new List<NodeInfo>(_graph.NodeCount);
            _nodeQueue = new List<NodeInfo>(_graph.NodeCount);
            
            _shortestPathTree = new List<Edge>();

            float distanteToGoal = (_graph.GetNode(From).Position - _graph.GetNode(To).Position).Length();
            _nodeQueue.Add(new NodeInfo(From, 0, distanteToGoal));

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

                       if (_nodeQueue.Count > 0)
                       {
                           _nodeQueue.Sort();
                           NodeInfo currentNode = _nodeQueue[_nodeQueue.Count - 1];
                           _nodeQueue.RemoveAt(_nodeQueue.Count - 1);

                           if (currentNode.ID == To)
                           {
                               _visitedNodes.Add(currentNode);
                               IsFinished = true;
                           }

                           foreach (Edge edge in _graph.Edges)
                           {
                               int candidateID = -1;
                               if (edge.To == currentNode.ID)
                               {
                                   candidateID = edge.From;
                               }
                               else if (edge.From == currentNode.ID)
                               {
                                   candidateID = edge.To;
                               }

                               if (candidateID >= 0)
                               {
                                   bool visited = false;
                                   foreach (NodeInfo visitedNode in _visitedNodes)
                                   {
                                       if (visitedNode.ID == candidateID)
                                       {
                                           visited = true;
                                       }
                                   }

                                   bool queued = false;
                                   for (int i = 0; i < _nodeQueue.Count; i++)
                                   {
                                       if (_nodeQueue[i].ID == candidateID)
                                       {
                                           queued = true;
                                           float newCost = currentNode.LowestCostToNode + _graph.GetEdgeCost(candidateID);
                                           if (_nodeQueue[i].LowestCostToNode > newCost)
                                           {
                                               _nodeQueue[i].LowestCostToNode = newCost;

                                               for (int j = 0; j < _shortestPathTree.Count; j++)
                                               {
                                                   if (_shortestPathTree[i].To == candidateID)
                                                   {
                                                       _shortestPathTree.RemoveAt(i);
                                                       _shortestPathTree.Add(new Edge(currentNode.ID, candidateID));
                                                   }
                                               }

                                           }
                                       }
                                   }

                                   if (!queued && !visited)
                                   {
                                       float distanceToGoal = (_graph.GetNode(candidateID).Position - _graph.GetNode(To).Position).Length();
                                       _nodeQueue.Add(new NodeInfo(candidateID, currentNode.LowestCostToNode + _graph.GetEdgeCost(edge.ID), distanceToGoal));
                                       _shortestPathTree.Add(new Edge(currentNode.ID, candidateID));
                                   }
                               }
                           }

                           _visitedNodes.Add(currentNode);
                       }
            
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
