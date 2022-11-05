using IntroductionLab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace State_Machines
{
    internal class Hero
    {
        public Vector2 Position { get; private set; }
        public float MaxSpeed { get; private set; }
        public float Radius { get; private set; }

        public int Score { get; private set; }

        private List<Wall> _walls;

        private McGuffin _mcGuffin;

        public Hero(Vector2 pPosition, float pMaxSpeed, float pRadius, List<Wall> pWalls, McGuffin pMcGuffin)
        {
            Score = 0;
            Position = pPosition;
            MaxSpeed = pMaxSpeed;
            Radius = pRadius;
            _walls = pWalls;
            _mcGuffin = pMcGuffin;
        }

        public virtual void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(Position, Radius, 32, 2, Color.DarkSeaGreen);

            bool intersects = false;
            foreach(Wall wall in _walls)
            {
                if(wall.Intersects(Position, _mcGuffin.Position))
                {
                    intersects = true;
                }
            }
        }

        public virtual void Update(KeyboardState pKeyboardState)
        {
            Vector2 newPosition = Position;

            if (pKeyboardState.IsKeyDown(Keys.W))
            {
                newPosition.Y += MaxSpeed;
            }

            if (pKeyboardState.IsKeyDown(Keys.S))
            {
                newPosition.Y -= MaxSpeed;
            }

            if (pKeyboardState.IsKeyDown(Keys.A))
            {
                newPosition.X -= MaxSpeed;
            }

            if (pKeyboardState.IsKeyDown(Keys.D))
            {
                newPosition.X += MaxSpeed;
            }

            bool intersect = false;
            for (int i = 0; i < _walls.Count; i++)
            {
                if (_walls[i].Intersects(newPosition, Radius))
                {
                    intersect = true;
                }
            }

            if (!intersect)
            {
                Position = newPosition;
            }

            if (Utility.IntersectingCircles(Position, Radius, _mcGuffin.Position, _mcGuffin.Radius))
            {
                _mcGuffin.CollectMcGuffin();
                Score += 10;
            }
        }
    }
}
