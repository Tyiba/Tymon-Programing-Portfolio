using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGui;
using System.Collections.Generic; 

namespace IntroductionLab
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ShapeBatcher _shapeBatcher;
        private ImGuiRenderer _guiRenderer;

        

        private List<Agent> agents = new List<Agent>();

        bool _paused = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            _guiRenderer = new ImGuiRenderer(this).Initialize().RebuildFontAtlas();

            _shapeBatcher = new ShapeBatcher(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);



            //
            // ARRIVE AGENT CODE 
            //

            //agents.Add(new ArriveAgent(new Vector2(500, 300), 350, new Vector2(50, 50), new Vector2(20, 0), 150, 15, 1, Color.Crimson));

            //agents.Add(new SeekAgent(new Vector2(500,300), new Vector2(50,50), new Vector2(20,0), 150, 15 ,1, Color.GreenYellow));  

            //
            // PATH FOLLOWING AGENT CODE
            //

            PathFollowingAgent follower = new PathFollowingAgent(new Vector2(200, 100), new Vector2(50, 50), new Vector2(20, 0), 100, 10, 1, Color.Black);

            follower.AddTargetToPath(new Vector2(500, 200));
            follower.AddTargetToPath(new Vector2(650, 300));
            follower.AddTargetToPath(new Vector2(500, 450));
            follower.AddTargetToPath(new Vector2(300, 400));
            follower.AddTargetToPath(new Vector2(200, 300));
            

            agents.Add(follower);

            PursueAgent PoPo = new PursueAgent(follower, new Vector2 (25,25), new Vector2 (20,0), 80, 9,1, Color.White);

            agents.Add(PoPo);  

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!_paused)
            {
                float seconds = gameTime.ElapsedGameTime.Milliseconds / 1000f;
                foreach (Agent agent in agents)
                {
                    agent.Update(seconds);
                }

            }
            // TODO: Add your update logic here


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.End();

            _shapeBatcher.Begin();

            foreach (Agent agent in agents)
            {
                agent.Draw(_shapeBatcher);
            }

            _shapeBatcher.End();

            base.Draw(gameTime);

            _guiRenderer.BeginLayout(gameTime);
            ImGui.Begin("Debug Interface");

            if (_paused)
            {
                if(ImGui.Button("Start"))
                {
                    _paused = false;
                }
            }
            else
            {

                if (ImGui.Button("Pause"))
                {
                    _paused = true;
                }
            }
            ImGui.End();
            _guiRenderer.EndLayout();
        }
    }
}