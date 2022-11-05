using System;
using System.Collections.Generic;

public class MyVector
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }
    public float W {get; private set;}


    public MyVector(float pX, float pY, float pZ, float pW = 1)
    {
        X = pX; 
        Y = pY; 
        Z = pZ;   
        W = pW;
    }
    public MyVector Copy()
    {
        return null;
    }
    public MyVector Add(MyVector pVector)
    {
        float tempX = this.X + pVector.X;
        float tempY = this.Y + pVector.Y;
        float tempZ = this.Z + pVector.Z;

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;
    }
    public MyVector Subtract(MyVector pVector)
    {
        float tempX = this.X - pVector.X;
        float tempY = this.Y - pVector.Y;
        float tempZ = this.Z - pVector.Z;

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;
    }
    public MyVector Multiply(float pScalar)
    {
        float tempX = this.X * pScalar;
        float tempY = this.Y * pScalar;
        float tempZ = this.Z * pScalar;

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;
    }
    public MyVector Divide(float pScalar)
    {
        float tempX = this.X / pScalar;
        float tempY = this.Y / pScalar;
        float tempZ = this.Z / pScalar;

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;
    }

    public float Magnitude()
    {
        float magnitude = (float)Math.Sqrt(Math.Pow(this.X,2) + Math.Pow(this.Y,2) + Math.Pow(this.Z,2));
        return magnitude;
    }

    public MyVector Normalise()
    {
        float magnitude = Magnitude();
        
        float tempX = this.X / magnitude;
        float tempY = this.Y / magnitude;
        float tempZ = this.Z / magnitude;

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;
    }

    public float DotProduct(MyVector pVector)
    {
        float tempX = this.X * pVector.X;
        float tempY = this.Y * pVector.Y;
        float tempZ = this.Z * pVector.Z;

        float dotProduct = tempX + tempY + tempZ;
        return dotProduct;
    }
    public MyVector RotateX(float pRadians)
    {
        float tempX = this.X;
        float tempY = this.Y;
        float tempZ = this.Z;

        
        tempY = (float)(Math.Cos(pRadians)*this.Y - Math.Sin(pRadians)*this.Z);
        tempZ = (float)(Math.Sin(pRadians)*this.Y - Math.Cos(pRadians)*this.Z);

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;
    }
    public MyVector RotateY(float pRadians)
    {
        float tempX = this.X;
        float tempY = this.Y;
        float tempZ = this.Z;

        tempX = (float)(Math.Cos(pRadians)*this.X +Math.Sin(pRadians)*this.Z);
        
        tempZ = (float)(-Math.Sin(pRadians)*this.X +Math.Cos(pRadians)*this.Z);

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;


    }
    public MyVector RotateZ(float pRadians)
    {
        float tempX = this.X;
        float tempY = this.Y;
        float tempZ = this.Z;

        tempX = (float)(-Math.Sin(pRadians)*this.Y - Math.Cos(pRadians)*this.X); 
        tempY = (float)(Math.Sin(pRadians)*this.X + Math.Cos(pRadians)*this.Y);
        

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;
    }
    public MyVector LimitTo(float pMax)
    {
        float tempX = this.X;
        float tempY = this.Y;
        float tempZ = this.Z;
        float magnitude = Magnitude();
        if (magnitude > pMax)
        {
            float ratio = pMax / magnitude;
            magnitude = pMax;
            tempX = this.X * ratio;
            tempY = this.Y * ratio;
            tempZ = this.Z * ratio;
            
        }

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;

    }
    public MyVector Interpolate(MyVector pVector, float pInterpolation)
    {
        float tempX = this.X + (pVector.X - this.X) * pInterpolation;
        float tempY = this.Y + (pVector.Y - this.Y) * pInterpolation;
        float tempZ = this.Z + (pVector.Z - this.Z) * pInterpolation;

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;
        

    }
    public float AngleBetween(MyVector pVector)
    {
        float magnitude = Magnitude() * pVector.Magnitude();
        float tempX = this.X * pVector.X;
        float tempY = this.Y * pVector.Y;
        float tempZ = this.Z * pVector.Z;

        float dotProduct = tempX + tempY + tempZ;
        
        float angleBetween = (float)Math.Acos(dotProduct / magnitude);

        return angleBetween;
    }
    public MyVector CrossProduct(MyVector pVector)
    {
    
        float tempX = this.Y * pVector.Z - this.Z * pVector.Y;
        float tempY = this.Z * pVector.X - this.X * pVector.Z;
        float tempZ = this.X * pVector.Y - this.Y * pVector.X;

        MyVector NewVector = new MyVector(tempX,tempY,tempZ);
        return NewVector;

    }
    public override string ToString()
    {
        string result = "X: " + X + " " + "Y: " + Y + " " + "Z: " + Z;
        return result;
    }
}
