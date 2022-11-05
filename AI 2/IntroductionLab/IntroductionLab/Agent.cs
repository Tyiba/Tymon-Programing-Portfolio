using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;


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



        //AGENT CONSTRUCTOR
        public Agent(Vector2 pPosition, Vector2 pVelocity,float pMaxSpeed,float pRadius, float pMass, Color pColour)
        {
            Position = pPosition;
            Velocity = pVelocity;
            MaxSpeed = pMaxSpeed;
            Mass = pMass;
            Colour = pColour;
            Radius = pRadius;
        }
        

        public virtual void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(Position, Radius, 32, 2, Colour);
            pShapeBatcher.DrawArrow(Position, Velocity, 2, 5, Color.Green);
        }

        public virtual void Update(float pSeconds)
        {
            SteeringForce = DesiredVelocity - Velocity;
            Vector2 acceleration = Vector2.Divide(SteeringForce, Mass);
            Velocity += acceleration * pSeconds;
            Position += Velocity * pSeconds;

            Velocity = Velocity.Clamp(MaxSpeed);
        }




    }
}
