using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroductionLab
{
    internal class SeekAgent : Agent
    {
        public Vector2 Target { get; private set; }

        public SeekAgent(Vector2 pTarget, Vector2 pPosition, Vector2 pVelocity, float pMaxSpeed, float pRadius, float pMass, Color pColour)
            : base(pPosition, pVelocity, pMaxSpeed, pRadius, pMass, pColour)
        {
            Target = pTarget;
        }

        public override void Draw(ShapeBatcher pShapeBatcher)
        {

            base.Draw(pShapeBatcher);
            pShapeBatcher.DrawCircle(Target, 5, 20, 2, Color.Red);
        }

        public override void Update(float pSeconds)
        {
            DesiredVelocity = Target - Position;
            base.Update(pSeconds);

        }

    }
}
