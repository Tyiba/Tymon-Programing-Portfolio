using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace IntroductionLab
{
    internal class PursueAgent : Agent
    {
        public Agent TargetAgent { get; private set; }


        public PursueAgent(Agent pTargetAgent, Vector2 pPosition, Vector2 pVelocity, float pMaxSpeed, float pRadius, float pMass, Color pColour)
            : base(pPosition, pVelocity, pMaxSpeed, pRadius, pMass, pColour)
        {
            TargetAgent = pTargetAgent;
        }


        public override void Draw(ShapeBatcher pShapeBatcher)
        {

            base.Draw(pShapeBatcher);
            
        }

        public override void Update(float pSeconds)
        {
            DesiredVelocity = TargetAgent.Position - Position;
            base.Update(pSeconds);
            //
        }
    }
}
