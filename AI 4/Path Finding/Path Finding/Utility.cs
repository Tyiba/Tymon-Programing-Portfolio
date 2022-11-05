using Microsoft.Xna.Framework;

namespace Path_Finding
{
    internal static class Utility
    {
        public static Vector2 FlipY(this Point pPoint, float pScreenHeight)
        {
            return new Vector2(pPoint.X, pScreenHeight * (1f - pPoint.Y / pScreenHeight));
        }

        public static Vector2 FlipY(this Vector2 pVector, float pScreenHeight)
        {
            pVector.Y = pScreenHeight * (1f - (pVector.Y / pScreenHeight));
            return pVector;
        }

        public static bool IsInsideCircle(Vector2 pPoint, Vector2 pCircle, float pRadius)
        {
            return Vector2.DistanceSquared(pPoint, pCircle) < pRadius * pRadius;
        }
    }
}
