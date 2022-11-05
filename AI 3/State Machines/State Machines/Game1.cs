
using IntroductionLab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace State_Machines
{
    public class Game1 : Game           
    {
        //added
        private List<Agent> _agents;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ShapeBatcher _shapeBatcher;

        private SpriteFont _scoreFont;
        private Hero _hero;
        private McGuffin _mcGuffin;
        private List<Wall> _walls;

        private List<Vector2> _wayPoints;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _shapeBatcher = new ShapeBatcher(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            _scoreFont = Content.Load<SpriteFont>("scoreFont");

            _walls = new List<Wall>();
            int w = _graphics.GraphicsDevice.Viewport.Width;
            int h = _graphics.GraphicsDevice.Viewport.Height;

            Vector2 topLeft = new Vector2(5, h - 5);
            Vector2 topRight = new Vector2(w - 5, h - 5);
            Vector2 bottomLeft = new Vector2(5, 5);
            Vector2 bottomRight = new Vector2(w - 5, 5);

            _walls.Add(new Wall(topLeft, topRight));
            _walls.Add(new Wall(bottomLeft, bottomRight));
            _walls.Add(new Wall(topLeft, bottomLeft));
            _walls.Add(new Wall(bottomRight, topRight));

            _walls.Add(new Wall(new Vector2(80, 80), new Vector2(80, h - 80)));
            _walls.Add(new Wall(new Vector2(80, 80), new Vector2(300, 80)));
            _walls.Add(new Wall(new Vector2(80, h - 80), new Vector2(300, h - 80)));

            _walls.Add(new Wall(new Vector2(w - 80, 80), new Vector2(w - 80, h - 80)));
            _walls.Add(new Wall(new Vector2(w - 80, 80), new Vector2(w - 300, 80)));
            _walls.Add(new Wall(new Vector2(w - 80, h - 80), new Vector2(w -300, h - 80)));

            _walls.Add(new Wall(new Vector2(150, h - 150), new Vector2(w - 150, h - 150)));
            _walls.Add(new Wall(new Vector2(150, 150), new Vector2(w - 150, 150)));

            //MCGUFFIN = COIN
            _mcGuffin = new McGuffin(new Vector2(w / 2, h / 2), 5, Content.Load<SoundEffect>("smw_coin"));

            _mcGuffin.AddNewSpawnLocation(new Vector2(125, 125));
            _mcGuffin.AddNewSpawnLocation(new Vector2(125, h - 125));
            _mcGuffin.AddNewSpawnLocation(new Vector2(w - 125, h - 125));
            _mcGuffin.AddNewSpawnLocation(new Vector2(w - 125, 125));

            _mcGuffin.AddNewSpawnLocation(new Vector2(25, 25));
            _mcGuffin.AddNewSpawnLocation(new Vector2(25, h - 25));
            _mcGuffin.AddNewSpawnLocation(new Vector2(w - 25, h - 25));
            _mcGuffin.AddNewSpawnLocation(new Vector2(w - 25, 25));

            
            _hero = new Hero(new Vector2(25, 25), 5, 10, _walls, _mcGuffin);

            //guard and path added
            _agents = new List<Agent>();
            enumGuard guard = new enumGuard(new Vector2(750,440), new Vector2(750,440), Vector2.Zero, 30, 10, 1, Color.Red, _walls, _hero, _wayPoints);
            guard.AddTargetToPath(new Vector2(50, 440));
            guard.AddTargetToPath(new Vector2(50, 40));
            guard.AddTargetToPath(new Vector2(750, 40));
            guard.AddTargetToPath(new Vector2(750, 440));

            _agents.Add(guard);

            //waypoints

            _wayPoints = new List<Vector2>();

            _wayPoints.Add(new Vector2(750,440));
            _wayPoints.Add(new Vector2(400,440));
            _wayPoints.Add(new Vector2(50,440));
            _wayPoints.Add(new Vector2(50,40));
            _wayPoints.Add(new Vector2(400,40));
            _wayPoints.Add(new Vector2(750,40));
            _wayPoints.Add(new Vector2(400,110));
            _wayPoints.Add(new Vector2(400,440));
            _wayPoints.Add(new Vector2(110,110));
            _wayPoints.Add(new Vector2(680,110));
            _wayPoints.Add(new Vector2(110,230));
            _wayPoints.Add(new Vector2(680,230));
            _wayPoints.Add(new Vector2(110,360));
            _wayPoints.Add(new Vector2(680,360));
            _wayPoints.Add(new Vector2(400,360));



        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _hero.Update(Keyboard.GetState());

            float seconds = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            foreach(Agent agent in _agents)
            {
                agent.Update(seconds);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            Vector2 scoreSize = _scoreFont.MeasureString(_hero.Score.ToString());
            Vector2 scorePosition = new Vector2(_graphics.GraphicsDevice.Viewport.Width - scoreSize.X - 10,
                _graphics.GraphicsDevice.Viewport.Height - 10);

            _spriteBatch.DrawString(_scoreFont, _hero.Score.ToString(), scorePosition.FlipY(_graphics.GraphicsDevice.Viewport.Height), Color.Black);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            _shapeBatcher.Begin();

            _hero.Draw(_shapeBatcher);

            foreach(Wall wall in _walls)
            {
                wall.Draw(_shapeBatcher);
            }
            foreach(Agent agent in _agents)
            {
                agent.Draw(_shapeBatcher);
            }
            _mcGuffin.Draw(_shapeBatcher);

            foreach(Vector2 waypoint in _wayPoints)
            {
                _shapeBatcher.DrawCircle(waypoint, 8, 18, 3, Color.Purple);
            }

            _shapeBatcher.End();

            base.Draw(gameTime);
        }
    }
}