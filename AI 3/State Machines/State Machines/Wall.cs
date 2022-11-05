using IntroductionLab;
using Microsoft.Xna.Framework;

namespace State_Machines
{
    internal class Wall
    {
        public Vector2 Start { get; private set; }
        public Vector2 End { get; private set; }

        public Wall(Vector2 pStart, Vector2 pEnd)
        {
            Start = pStart;
            End = pEnd;
        }

        public void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawLine(Start, End, 2, Color.Black);
        }
    }
}
