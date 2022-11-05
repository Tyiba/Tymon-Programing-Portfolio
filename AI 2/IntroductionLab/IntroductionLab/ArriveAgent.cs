using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace IntroductionLab
{
    internal class ArriveAgent : Agent
    {
        public float Threshold { get; private set; }
        public Vector2 Target { get; private set; }


        public ArriveAgent(Vector2 pTarget, float pThreshold, Vector2 pPosition, Vector2 pVelocity, float pMaxSpeed, float pRadius, float pMass, Color pColor)
            : base(pPosition, pVelocity, pMaxSpeed, pRadius, pMass, pColor)
        {
            Target = pTarget;
            Threshold = pThreshold;
        }

        public override void Draw(ShapeBatcher pShapeBatcher)
        {

            base.Draw(pShapeBatcher);
            pShapeBatcher.DrawCircle(Target, 5, 20, 2, Color.Red);
        }


        public override void Update(float pSeconds)
        {
            DesiredVelocity = Target - Position;
            float length = DesiredVelocity.Length();
            if(length < Threshold)
            {
                float scaleFactor = length / Threshold;
                DesiredVelocity = Vector2.Multiply(DesiredVelocity, scaleFactor);
            }
            base.Update(pSeconds);
        }
    }
}
