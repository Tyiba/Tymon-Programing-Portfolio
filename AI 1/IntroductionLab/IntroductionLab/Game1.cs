using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IntroductionLab
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //1 works fine
        private Texture2D _PNGTexture;
        private Vector2 _PNGPosition;
        //2 works fine
        int _score;
        SpriteFont _scoreFont;
        Vector2 _scorePosition;
        //3 works fine
        KeyboardState _lastKeyboardState;
        //4 works fine
        private ShapeBatcher _shapeBatcher;
        //5 works fine
        private Vector2 _circlePosition;
        private Vector2 _circleVelocity;
        //6
        private float _circleRadius;
        //7
        private Vector2 _circle2Position;
        private float _circle2Radius;
        private Color _circle2Colour;


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
            //1
            _PNGTexture = Content.Load<Texture2D>("PNGG");
            _PNGPosition = new Vector2(50, 50);
            //2
            _score = 0;
            _scoreFont = Content.Load<SpriteFont>("ScoreFont");
            _scorePosition = Vector2.Zero;
            //3 
            _circlePosition = new Vector2(300, 400); // circle
            //4 
            _circleVelocity = new Vector2(40, 40);
            //5 
            _circleRadius = 20;
            //6 New Circle

            _circle2Position = new Vector2(600, 250);
            _circle2Radius = 30f;
            _circle2Colour = Color.Blue;


        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //1
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !_lastKeyboardState.IsKeyDown(Keys.S))
            {
                _score += 10;
                _scorePosition.X = _graphics.GraphicsDevice.Viewport.Width - _scoreFont.MeasureString(_score.ToString()).X - 5;
            }

            _lastKeyboardState = Keyboard.GetState();
            //2 time 
            float seconds = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            _circlePosition = _circlePosition + _circleVelocity * seconds;

            //3 New Circle Update location
            if (Utility.IsInsideCircle(Mouse.GetState().Position.FlipY(_graphics.GraphicsDevice.Viewport.Height), _circle2Position, _circle2Radius))
            {
                _circle2Colour = Color.Green;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    _circle2Colour = Color.Blue;
                    _circle2Position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.FlipY(_graphics.GraphicsDevice.Viewport.Height).Y);
                }
                
            }
            else
            {
                _circle2Colour = Color.Red;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            
            _spriteBatch.Draw(_PNGTexture, _PNGPosition.FlipY(_graphics.GraphicsDevice.Viewport.Height).CentreTexture(_PNGTexture.Width, _PNGTexture.Height), Color.White);

            //1 works fine 
            _scorePosition.X = _graphics.GraphicsDevice.Viewport.Width - _scoreFont.MeasureString(_score.ToString()).X - 5;
            _spriteBatch.DrawString(_scoreFont, _score.ToString(), _scorePosition, Color.White);
            _spriteBatch.End();

            

            _shapeBatcher.Begin();
            //2 works fine
            _shapeBatcher.DrawLine(new Vector2(20, 20), new Vector2(_graphics.GraphicsDevice.Viewport.Width - 20, _graphics.GraphicsDevice.Viewport.Height - 20), 5, Color.OrangeRed);
            
            //4 works fine
            _shapeBatcher.DrawArrow(_circlePosition, _circleVelocity, 2, 10, Color.Red);
            //5 works fine
            _shapeBatcher.DrawCircle(_circlePosition, _circleRadius, 16, 3, Color.Goldenrod);

            if (_circlePosition.X - _circleRadius < 0 || _circlePosition.X + _circleRadius > _graphics.GraphicsDevice.Viewport.Width)
            {
                _circleVelocity.X = -_circleVelocity.X;
            }

            if (_circlePosition.Y - _circleRadius < 0 || _circlePosition.Y + _circleRadius > _graphics.GraphicsDevice.Viewport.Height)
            {
                _circleVelocity.Y = -_circleVelocity.Y;
            }


            _shapeBatcher.End();
            //6 New Circle
            _shapeBatcher.Begin();
            _shapeBatcher.DrawCircle(_circle2Position, _circle2Radius, 16, 3, _circle2Colour);
            
            _shapeBatcher.End();

            base.Draw(gameTime);
        }
    }
}