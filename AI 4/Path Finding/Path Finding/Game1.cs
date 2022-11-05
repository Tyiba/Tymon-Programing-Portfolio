using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGui;
using System;
using System.Linq;

namespace Path_Finding
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ShapeBatcher _shapeBatcher;
        private ImGuiRenderer _guiRenderer;

        private SpriteFont _labelFont;

        private IGraphSearch _pathFinder;

        const int NODE_RADIUS = 25;

        private Graph _graph;

        enum MouseFunction { NONE, DRAGGING, JOINING };

        MouseFunction _MouseFunction = MouseFunction.NONE;

        int _hoverOverNode = -1;
        int _activeNode = -1;
        int _startNode = -1;
        int _endNode = -1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _guiRenderer = new ImGuiRenderer(this).Initialize().RebuildFontAtlas();
            _shapeBatcher = new ShapeBatcher(this);

            _graph = new Graph();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _labelFont = Content.Load<SpriteFont>("Label");

            // TODO: use this.Content to load your game content here
            for(int column = 50; column < 500; column+= 100)
            { 
                for (int row = 50; row < 500; row+=100)
                {
                    _graph.AddNode(new Vector2(column, row));
                }
            }

            Random rng = new Random();

        }

        protected override void Update(GameTime pGameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Vector2 flippedMouse = Utility.FlipY(Mouse.GetState().Position, _graphics.GraphicsDevice.Viewport.Height);

            if (!ImGui.GetIO().WantCaptureMouse)
            {
                switch (_MouseFunction)
                {
                    case MouseFunction.NONE:

                        _hoverOverNode = -1;
                        foreach (Node node in _graph.Nodes)
                        {
                            if (Utility.IsInsideCircle(flippedMouse, node.Position, NODE_RADIUS))
                            {
                                _hoverOverNode = node.ID;

                                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                                {
                                    _hoverOverNode = -1;
                                    _activeNode = node.ID;

                                    if(_endNode == -1)
                                    {
                                        _endNode = node.ID;
                                    }
                                    else
                                    {
                                        _startNode = _endNode;
                                        _endNode = node.ID;
                                    }

                                    _MouseFunction = MouseFunction.DRAGGING;
                                }
                                else if (Mouse.GetState().RightButton == ButtonState.Pressed)
                                {
                                    _hoverOverNode = -1;
                                    _activeNode = node.ID;
                                    _MouseFunction = MouseFunction.JOINING;
                                }
                            }
                        }

                        if (_hoverOverNode == -1 && _MouseFunction == MouseFunction.NONE && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            _graph.AddNode(Mouse.GetState().Position.ToVector2().FlipY(_graphics.GraphicsDevice.Viewport.Height));
                        }

                        break;
                    case MouseFunction.DRAGGING:

                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            _graph.GetNode(_activeNode).Position = Mouse.GetState().Position.ToVector2().FlipY(_graphics.GraphicsDevice.Viewport.Height);
                        }
                        else
                        {
                            _MouseFunction = MouseFunction.NONE;
                        }

                        break;
                    case MouseFunction.JOINING:

                        _hoverOverNode = -1;

                        foreach (Node node in _graph.Nodes)
                        {
                            if (Utility.IsInsideCircle(flippedMouse, node.Position, NODE_RADIUS))
                            {
                                _hoverOverNode = node.ID;

                            }
                        }

                        if (Mouse.GetState().RightButton == ButtonState.Released)
                        {
                            if (_hoverOverNode != -1 && _hoverOverNode != _activeNode)
                            {
                                _graph.AddEdge(_activeNode, _hoverOverNode);
                            }

                            _MouseFunction = MouseFunction.NONE;
                        }

                        break;
                }
            }

            float seconds = pGameTime.ElapsedGameTime.Milliseconds / 1000f;

            if(_pathFinder != null)
            {
                _pathFinder.Update(seconds);
            }

            base.Update(pGameTime);
        }

        private void DrawEdgeLabel(Edge edge)
        {
            Vector2 Position = (_graph.GetNode(edge.From).Position + _graph.GetNode(edge.To).Position) * 0.5f;
            Position = Position.FlipY(_graphics.GraphicsDevice.Viewport.Height);

            _spriteBatch.DrawString(_labelFont, _graph.GetEdgeCost(edge.ID).ToString(), Position, Color.BlanchedAlmond);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _shapeBatcher.Begin();

            foreach (Edge edge in _graph.Edges)
            {
                _shapeBatcher.DrawLine(_graph.GetNode(edge.From).Position,
                    _graph.GetNode(edge.To).Position, 6, Color.Black);
            }

            if(_MouseFunction == MouseFunction.JOINING)
            {
                _shapeBatcher.DrawLine(_graph.GetNode(_activeNode).Position,
                    Mouse.GetState().Position.ToVector2().FlipY(_graphics.GraphicsDevice.Viewport.Height), 6, Color.Red);
            }

            Color fillColour, outlineColour;

            foreach (Node node in _graph.Nodes)
            {
                if(node.ID == _hoverOverNode)
                {
                    outlineColour = Color.White;
                }
                else
                {
                    outlineColour = Color.Black;
                }

                if(node.ID == _startNode)
                {
                    fillColour = Color.Green;
                }
                else if(node.ID == _endNode)
                {
                    fillColour = Color.Red;
                }
                else
                {
                    fillColour = Color.DodgerBlue;
                }

                _shapeBatcher.DrawFilledCircle(node.Position, NODE_RADIUS, 16, fillColour);
                _shapeBatcher.DrawCircle(node.Position, NODE_RADIUS, 16, 2, outlineColour);
            }

            if(_pathFinder != null)
            {
                _pathFinder.DrawShapes(_shapeBatcher);
            }

            _shapeBatcher.End();

            _spriteBatch.Begin();

            foreach(Edge edge in _graph.Edges)
            {
                DrawEdgeLabel(edge);
            }

            if(_pathFinder != null)
            {
                _pathFinder.DrawSprites(_spriteBatch, _labelFont, _graphics.GraphicsDevice.Viewport.Height);
            }

            _spriteBatch.End();

            base.Draw(gameTime);

            _guiRenderer.BeginLayout(gameTime);


            ImGui.Begin("Debug Interface");
            ImGui.SetWindowFontScale(1.25f);

            if (_activeNode != -1)
            {
                if(ImGui.Button("Delete Node " + _activeNode))
                {
                    _graph.RemoveNode(_activeNode);
                    _pathFinder = null;
                    _activeNode = -1;
                }

                foreach(Edge edge in _graph.Edges)
                {
                    if (edge.From == _activeNode || edge.To == _activeNode)
                    {
                        if (ImGui.Button("Delete Edge " + edge.ID + " - " + edge.From + " -> " + edge.To))
                        {
                            _graph.RemoveEdge(edge.ID);
                            _pathFinder = null;
                            break;
                        }
                    }
                }

                if(_startNode != -1 && _endNode != -1)
                {
                    if (ImGui.Button("Dijkstra Path from " + _startNode + " to " + _endNode))
                    {
                        _pathFinder = new Dijkstra(_graph, _startNode, _endNode);
                    }
                    if (ImGui.Button("A* Path from " + _startNode + " to " + _endNode))
                    {
                        _pathFinder = new AStarSearch(_graph, _startNode, _endNode);
                    }
                }
            }

            ImGui.End();
            _guiRenderer.EndLayout();
        }
    }
}