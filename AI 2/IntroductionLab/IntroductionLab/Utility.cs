using Microsoft.Xna.Framework;

namespace IntroductionLab
{
    /// <summary>
    /// This class is a place to put methods that we might want to use throughout the program.
    /// Probably not great practice but acknowledging this will avoid overengineering when it's not appropriate.
    /// </summary>
    internal static class Utility
    {
        public static Vector2 CentreTexture(this Vector2 pCurrentPosition, int pTextureWidth, int pTextureHeight)
        {
            return new Vector2(pCurrentPosition.X - pTextureWidth * 0.5f, pCurrentPosition.Y - pTextureHeight * 0.5f);
        }

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

        public static Vector2 Clamp(this Vector2 pVector, float pMaxLength)
        {
            float lenght = pVector.Length();
            if (lenght > pMaxLength)
            {
                float scaleFactor = pMaxLength / lenght;
                pVector.X *= scaleFactor;
                pVector.Y *= scaleFactor;

            }
            return pVector;
        }
    }
}
