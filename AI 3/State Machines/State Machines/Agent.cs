using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using State_Machines;


namespace IntroductionLab
{
    abstract internal class Agent
    {
        public Vector2 Position { get; private set; } //where it is
        public Vector2 Velocity { get; private set; } //where it is going
        public Vector2 SteeringForce { get; private set; } //force pushing it towards desired velocity
        public Vector2 DesiredVelocity { get; protected set; } //where it wants to be going
        public float MaxSpeed { get; private set; } //to stop going to fast
        public float Radius { get; private set; } //how big it is
        public float Mass { get; private set; } //how heavy it is (other advanced calcs, friction etc.)
        public Color Colour { get; private set; } //colour of object
        private List<Wall> Walls;
        



        //AGENT CONSTRUCTOR
        public Agent(Vector2 pPosition, Vector2 pVelocity,float pMaxSpeed,float pRadius, float pMass, Color pColour, List<Wall> pWalls)
        {
            Position = pPosition;
            Velocity = pVelocity;
            MaxSpeed = pMaxSpeed;
            Mass = pMass;
            Colour = pColour;
            Radius = pRadius;
            Walls = pWalls;
        }

        

        public virtual void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(Position, Radius, 32, 2, Colour);
            pShapeBatcher.DrawArrow(Position, Velocity, 2, 5, Color.Green);

        }

        public virtual void Update(float pSeconds)
        {
            SteeringForce = DesiredVelocity - Velocity;

            for (int i = 0; i < Walls.Count; i++)
            {
                if (Walls[i].Intersects(Position, Radius+5))
                {
                    Vector2 startToCircle = Position - Walls[i].Start;
                    Vector2 lineDirection = Walls[i].End - Walls[i].Start;
                    float wallLenght = lineDirection.Length();
                    lineDirection.Normalize();
                    float adjacentLength = Vector2.Dot(lineDirection, startToCircle);

                    Vector2 closestPoint = Walls[i].Start + (lineDirection * adjacentLength);
                    SteeringForce = Position - closestPoint;

                }
            }
            Vector2 acceleration = Vector2.Divide(SteeringForce, Mass);
            Velocity += acceleration * pSeconds;
            Position += Velocity * pSeconds;
            Vector2 newPosition = Position + Velocity * pSeconds;
            Velocity = Velocity.Clamp(MaxSpeed);
            bool intersect = false;

            for(int i = 0; i < Walls.Count; i++)
            {
                if(Walls[i].Intersects(newPosition,Radius))
                {
                    intersect = true;

                }
            }
            if (!intersect)
            {
                Position = newPosition;
            }

            
        }




    }
}
