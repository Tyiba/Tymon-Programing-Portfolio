using Microsoft.Xna.Framework;
using State_Machines;
using System;
using System.Collections.Generic;

namespace IntroductionLab
{
    /// <summary>
    /// This class is a place to put methods that we might want to use throughout the program.
    /// Probably not great practice but acknowledging this will avoid overengineering when it's not appropriate.
    /// </summary>
    internal static class Utility
    {
        private static Random _rng = new Random();

        /// <summary>
        /// Returns a random number between pMin (inclusive) and pMax (exclusive)
        /// </summary>
        /// <param name="pMin">Inclusive minimum value</param>
        /// <param name="pMax">Exclusive maximum value</param>
        /// <returns>random number between pMin (inclusive) and pMax (exclusive)</returns>
        public static int GetRandomInteger(int pMin, int pMax)
        {
            return _rng.Next(pMin, pMax);
        }

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

        public static bool Intersects(this Wall pWall, Vector2 pTarget1, Vector2 pTarget2)
        {
            // adapted from https://ideone.com/PnPJgb

            Vector2 CmP = new Vector2(pTarget1.X - pWall.Start.X, pTarget1.Y - pWall.Start.Y);
            Vector2 r = new Vector2(pWall.End.X - pWall.Start.X, pWall.End.Y - pWall.Start.Y);
            Vector2 s = new Vector2(pTarget2.X - pTarget1.X, pTarget2.Y - pTarget1.Y);

            float CmPxr = CmP.X * r.Y - CmP.Y * r.X;
            float CmPxs = CmP.X * s.Y - CmP.Y * s.X;
            float rxs = r.X * s.Y - r.Y * s.X;

            if (CmPxr == 0f)
            {
                // Lines are collinear, and so intersect if they have any overlap

                return ((pTarget1.X - pWall.Start.X < 0f) != (pTarget1.X - pWall.End.X < 0f))
                    || ((pTarget1.Y - pWall.Start.Y < 0f) != (pTarget1.Y - pWall.End.Y < 0f));
            }

            if (rxs == 0f)
                return false; // Lines are parallel.

            float rxsr = 1f / rxs;
            float t = CmPxs * rxsr;
            float u = CmPxr * rxsr;

            return (t >= 0f) && (t <= 1f) && (u >= 0f) && (u <= 1f);
        }

        public static bool Intersects(this Wall pWall, Vector2 pCircle, float pRadius)
        {
            Vector2 startToCircle = pCircle - pWall.Start;
            Vector2 lineDirection = pWall.End - pWall.Start;
            float wallLength = lineDirection.Length();
            lineDirection.Normalize();
            float adjacentLength = Vector2.Dot(lineDirection, startToCircle);

            Vector2 closestPoint = pWall.Start + (lineDirection * adjacentLength);


            if(adjacentLength < -pRadius)
            {
                return false;
            }

            if(adjacentLength > wallLength + pRadius)
            {
                return false;
            }

            float closestDistance = (closestPoint - pCircle).Length();

            if (closestDistance < pRadius)
            {
                return true;
            }

            return false;
        }

        public static bool IntersectingCircles(Vector2 pPosition1, float pRadius1, Vector2 pPosition2, float pRadius2)
        {
            return (pPosition1 - pPosition2).Length() < pRadius1 + pRadius2;
        }

        public static Vector2 Clamp(this Vector2 pVector, float pMaxLength)
        {
            float length = pVector.Length();

            if (length > pMaxLength)
            {
                float scaleFactor = pMaxLength / length;
                pVector.X *= scaleFactor;
                pVector.Y *= scaleFactor;
            }
            return pVector;
        }
    }
}
