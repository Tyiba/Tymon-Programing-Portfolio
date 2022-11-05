using System;
using System.Collections.Generic;
using UnityEngine;

public class MyMatrix
{
    private float[,] values; 
    //comment to commit
    public MyMatrix(float pRow0Column0,
        float pRow0Column1,
        float pRow0Column2,
        float pRow0Column3,
        float pRow1Column0,
        float pRow1Column1,
        float pRow1Column2,
        float pRow1Column3,
        float pRow2Column0,
        float pRow2Column1,
        float pRow2Column2,
        float pRow2Column3,
        float pRow3Column0,
        float pRow3Column1,
        float pRow3Column2,
        float pRow3Column3)
    {
        values = new float[4,4] {{pRow0Column0,pRow0Column1,pRow0Column2,pRow0Column3},
        {pRow1Column0,pRow1Column1,pRow1Column2,pRow1Column3},
        {pRow2Column0,pRow2Column1,pRow2Column2,pRow2Column3},
        {pRow3Column0,pRow3Column1,pRow3Column2,pRow3Column3}};
        
    }


    public float GetElement(int pRow, int pColumn)
    {
        float element = values[pRow,pColumn];
        return element;
    }
    
    public static MyMatrix CreateIdentity()
    {
       MyMatrix Identity = new MyMatrix(1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1);
       return Identity;
    }

    public static MyMatrix CreateTranslation(MyVector pTranslation)
    {
        //temp variables
        //float tX = pTranslation.X;
        //float tY = pTranslation.Y;
        //float tZ = pTranslation.Z;
        //float tW = pTranslation.W;
      
        MyMatrix MyMatrix = MyMatrix.CreateIdentity();
        MyMatrix.values[0,3] = pTranslation.X;
        MyMatrix.values[1,3] = pTranslation.Y;
        MyMatrix.values[2,3] = pTranslation.Z;
        MyMatrix.values[3,3] = pTranslation.W;

        return MyMatrix;

    }

    public static MyMatrix CreateScale(MyVector pScale)
    {
        MyMatrix MyMatrix = MyMatrix.CreateIdentity();

        float[] vScales = new float[3]{pScale.X,pScale.Y,pScale.Z};
        for(int i=0; i<3; i++)
        {
            MyMatrix.values[i,i] = MyMatrix.values[i,i] * vScales[i];
        }
        return MyMatrix;
    }

    public static MyMatrix CreateRotationX(float pAngle)
    {
        MyMatrix MyMatrix = MyMatrix.CreateIdentity();
        MyMatrix.values[1,1] = MathF.Cos(pAngle);
        MyMatrix.values[1,2] = -MathF.Sin(pAngle);
        MyMatrix.values[2,1] = MathF.Sin(pAngle);
        MyMatrix.values[2,2] = MathF.Cos(pAngle);

        
        return MyMatrix;
    }

    public static MyMatrix CreateRotationY(float pAngle)
    {
        MyMatrix MyMatrix = MyMatrix.CreateIdentity();
        MyMatrix.values[0,0] = MathF.Cos(pAngle);
        MyMatrix.values[0,2] = MathF.Sin(pAngle);
        MyMatrix.values[2,0] = -MathF.Sin(pAngle);
        MyMatrix.values[2,2] = MathF.Cos(pAngle);

        return MyMatrix;
    }
    public static MyMatrix CreateRotationZ(float pAngle)
    {
        MyMatrix MyMatrix = MyMatrix.CreateIdentity();
        MyMatrix.values[0,0] = MathF.Cos(pAngle);
        MyMatrix.values[0,1] = -MathF.Sin(pAngle);
        MyMatrix.values[1,0] = MathF.Sin(pAngle);
        MyMatrix.values[1,1] = MathF.Cos(pAngle);

        return MyMatrix;
    }

    public MyVector Multiply(MyVector pVector)
    {
        //Req, makes vector, makes transMat, transMat x vector
        float checkingW = pVector.W;
        float tempX = this.values[0,0] * pVector.X + this.values[0,1] * pVector.Y + this.values[0,2] * pVector.Z + this.values[0,3] * pVector.W;
        float tempY = this.values[1,0] * pVector.X + this.values[1,1] * pVector.Y + this.values[1,2] * pVector.Z + this.values[1,3] * pVector.W;
        float tempZ = this.values[2,0] * pVector.X + this.values[2,1] * pVector.Y + this.values[2,2] * pVector.Z + this.values[2,3] * pVector.W;
        float tempW = this.values[3,0] * pVector.X + this.values[3,1] * pVector.Y + this.values[3,2] * pVector.Z + this.values[3,3] * pVector.W;

        //4X4 matrix * 4x1 Vector = 4X1 Vector

        //reverse testing // WRONG SOLUTION // 
        //float tempX = this.values[0,0] * pVector.X + this.values[1,0] * pVector.Y + this.values[2,0] * pVector.Z + this.values[3,0] * pVector.W;
        //float tempY = this.values[0,1] * pVector.X + this.values[1,1] * pVector.Y + this.values[2,1] * pVector.Z + this.values[3,1] * pVector.W;
        //float tempZ = this.values[0,2] * pVector.X + this.values[1,2] * pVector.Y + this.values[2,2] * pVector.Z + this.values[3,2] * pVector.W;
        //float tempW = this.values[0,3] * pVector.X + this.values[1,3] * pVector.Y + this.values[2,3] * pVector.Z + this.values[3,3] * pVector.W;

        MyVector vectorProduct = new MyVector(tempX,tempY,tempZ,tempW);
        return vectorProduct;
        
       
    }

    

    public MyMatrix Multiply(MyMatrix pMatrix)
    {
        float m00 = this.values[0, 0] * pMatrix.values[0, 0] + this.values[0, 1] * pMatrix.values[1, 0] + this.values[0, 2] * pMatrix.values[2, 0] + this.values[0, 3] * pMatrix.values[3, 0];
        float m01 = this.values[0, 0] * pMatrix.values[0, 1] + this.values[0, 1] * pMatrix.values[1, 1] + this.values[0, 2] * pMatrix.values[2, 1] + this.values[0, 3] * pMatrix.values[3, 1];
        float m02 = this.values[0, 0] * pMatrix.values[0, 2] + this.values[0, 1] * pMatrix.values[1, 2] + this.values[0, 2] * pMatrix.values[2, 2] + this.values[0, 3] * pMatrix.values[3, 2];
        float m03 = this.values[0, 0] * pMatrix.values[0, 3] + this.values[0, 1] * pMatrix.values[1, 3] + this.values[0, 2] * pMatrix.values[2, 3] + this.values[0, 3] * pMatrix.values[3, 3];
        
        float m10 = this.values[1, 0] * pMatrix.values[0, 0] + this.values[1, 1] * pMatrix.values[1, 0] + this.values[1, 2] * pMatrix.values[2, 0] + this.values[1, 3] * pMatrix.values[3, 0];
        float m11 = this.values[1, 0] * pMatrix.values[0, 1] + this.values[1, 1] * pMatrix.values[1, 1] + this.values[1, 2] * pMatrix.values[2, 1] + this.values[1, 3] * pMatrix.values[3, 1];
        float m12 = this.values[1, 0] * pMatrix.values[0, 2] + this.values[1, 1] * pMatrix.values[1, 2] + this.values[1, 2] * pMatrix.values[2, 2] + this.values[1, 3] * pMatrix.values[3, 2];
        float m13 = this.values[1, 0] * pMatrix.values[0, 3] + this.values[1, 1] * pMatrix.values[1, 3] + this.values[1, 2] * pMatrix.values[2, 3] + this.values[1, 3] * pMatrix.values[3, 3];
        
        float m20 = this.values[2, 0] * pMatrix.values[0, 0] + this.values[2, 1] * pMatrix.values[1, 0] + this.values[2, 2] * pMatrix.values[2, 0] + this.values[2, 3] * pMatrix.values[3, 0];
        float m21 = this.values[2, 0] * pMatrix.values[0, 1] + this.values[2, 1] * pMatrix.values[1, 1] + this.values[2, 2] * pMatrix.values[2, 1] + this.values[2, 3] * pMatrix.values[3, 1];
        float m22 = this.values[2, 0] * pMatrix.values[0, 2] + this.values[2, 1] * pMatrix.values[1, 2] + this.values[2, 2] * pMatrix.values[2, 2] + this.values[2, 3] * pMatrix.values[3, 2];
        float m23 = this.values[2, 0] * pMatrix.values[0, 3] + this.values[2, 1] * pMatrix.values[1, 3] + this.values[2, 2] * pMatrix.values[2, 3] + this.values[2, 3] * pMatrix.values[3, 3];

        float m30 = this.values[3, 0] * pMatrix.values[0, 0] + this.values[3, 1] * pMatrix.values[1, 0] + this.values[3, 2] * pMatrix.values[2, 0] + this.values[3, 3] * pMatrix.values[3, 0];
        float m31 = this.values[3, 0] * pMatrix.values[0, 1] + this.values[3, 1] * pMatrix.values[1, 1] + this.values[3, 2] * pMatrix.values[2, 1] + this.values[3, 3] * pMatrix.values[3, 1];
        float m32 = this.values[3, 0] * pMatrix.values[0, 2] + this.values[3, 1] * pMatrix.values[1, 2] + this.values[3, 2] * pMatrix.values[2, 2] + this.values[3, 3] * pMatrix.values[3, 2];
        float m33 = this.values[3, 0] * pMatrix.values[0, 3] + this.values[3, 1] * pMatrix.values[1, 3] + this.values[3, 2] * pMatrix.values[2, 3] + this.values[3, 3] * pMatrix.values[3, 3];
        



        MyMatrix MultipliedMatrix = new MyMatrix(m00,m01,m02,m03,m10,m11,m12,m13,m20,m21,m22,m23,m30,m31,m32,m33);
        return MultipliedMatrix;

    }

    public MyMatrix Inverse()
    {
       //STEP BY STEP OF HOW TO INVERSE
       //A-1 = 1/|A| * _A  , Inverse of A = 1/recirocal of A * Adjugate of A

        

        //THE MATRIX 
        float m11 = this.values[0, 0];
        float m12 = this.values[0, 1];
        float m13 = this.values[0, 2];
        float m14 = this.values[0, 3];
        float m21 = this.values[1, 0];
        float m22 = this.values[1, 1];
        float m23 = this.values[1, 2];
        float m24 = this.values[1, 3];
        float m31 = this.values[2, 0];
        float m32 = this.values[2, 1];
        float m33 = this.values[2, 2];
        float m34 = this.values[2, 3];
        float m41 = this.values[3, 0];
        float m42 = this.values[3, 1];
        float m43 = this.values[3, 2];
        float m44 = this.values[3, 3];

        //FINDING DETERMINANT

        float determinant = m11 * (m22 * m33 * m44 + m23 * m34 * m42 + m24 * m32 * m43 - m24 * m33 * m42 - m23 * m32 * m44 - m22 * m34 * m43)
            - m21 * (m12 * m33 * m44 + m13 * m34 * m42 + m14 * m32 * m43 - m14 * m33 * m42 - m13 * m32 * m44 - m12 * m34 * m43) +
            m31 * (m12 * m23 * m44 + m13 * m24 * m42 + m14 * m22 * m43 - m14 * m23 * m42 - m13 * m22 * m44 - m12 * m24 * m43) -
            m41 * (m12 * m23 * m34 + m13 * m24 * m32 + m14 * m22 * m33 - m14 * m23 * m32 - m12 * m24 * m33 - m13 * m22 * m34);

        //FINDING RECIPRICOL = 1/det
        float recipricol = 1 / determinant;

        //FINDING ADJUGATE WHICH IS VERY VERY VERY VERY LONG FOR MATRICES ABOVE 3X3 :)
        float a11 = (m22 * m33 * m44) + (m23 * m34 * m42) + (m24 * m32 * m43) - (m24 * m33 * m42) - (m23 * m32 * m44) - (m22 * m34 * m43);

        float a12 = -(m12 * m33 * m44) - (m13 * m34 * m42) - (m14 * m32 * m43) + (m14 * m33 * m42) + (m13 * m32 * m44) + (m12 * m34 * m43);
        
        float a13 = (m12 * m23 * m44) + (m13 * m24 * m42) + (m14 * m22 * m43) - (m14 * m23 * m42) - (m13 * m22 * m44) - (m12 * m24 * m43);
        
        float a14 = -(m12 * m23 * m34) - (m13 * m24 * m32) - (m14 * m22 * m33) + (m14 * m23 * m32) + (m13 * m22 * m34) + (m12 * m24 * m33);
        

        //good


        float a21 = -(m21 * m33 * m44) - (m23 * m34 * m41) - (m24 * m31 * m43) + (m24 * m33 * m41) + (m23 * m31 * m44) + (m21 * m34 * m43);
        
        float a22 = (m11 * m33 * m44) + (m13 * m34 * m41) + (m14 * m31 * m43) - (m14 * m33 * m41) - (m13 * m31 * m44) - (m11 * m34 * m43);
        
        float a23 = -(m11 * m23 * m44) - (m13 * m24 * m41) - (m14 * m21 * m43) + (m14 * m23 * m41) + (m13 * m21 * m44) + (m11 * m24 * m43);
        
        float a24 = (m11 * m23 * m34) + (m13 * m24 * m31) + (m14 * m21 * m33) - (m14 * m23 * m31) - (m13 * m21 * m34) - (m11 * m24 * m33);
        

        //good


        float a31 = (m21 * m32 * m44) + (m22 * m34 * m41) + (m24 * m31 * m42) - (m24 * m32 * m41) - (m22 * m31 * m44) - (m21 * m34 * m42);
        
        float a32 = -(m11 * m32 * m44) - (m12 * m34 * m41) - (m14 * m31 * m42) + (m14 * m32 * m41) + (m12 * m31 * m44) + (m11 * m34 * m42);
        
        float a33 = (m11 * m22 * m44) + (m12 * m24 * m41) + (m14 * m21 * m42) - (m14 * m22 * m41) - (m12 * m21 * m44) - (m11 * m24 * m42);
        
        float a34 = -(m11 * m22 * m34) - (m12 * m24 * m31) - (m14 * m21 * m32) + (m14 * m22 * m31) + (m12 * m21 * m34) + (m11 * m24 * m32);
        
        //good


        float a41 = -(m21 * m32 * m43) - (m22 * m33 * m41) - (m23 * m31 * m42) + (m23 * m32 * m41) + (m22 * m31 * m43) + (m21 * m33 * m42);
        
        float a42 = (m11 * m32 * m43) + (m12 * m33 * m41) + (m13 * m31 * m42) - (m13 * m32 * m41) - (m12 * m31 * m43) - (m11* m33 * m42);
        
        float a43 = -(m11 * m22 * m43) - (m12 * m23 * m41) - (m13 * m21 * m42) + (m13 * m22 * m41) + (m12 * m21 * m43) + (m11 * m23 * m42);
        
        float a44 = (m11 * m22 * m33) + (m12 * m23 * m31) + (m13 * m21 * m32) - (m13 * m22 * m31) - (m12 * m21 * m33) - (m11 * m23 * m32);






        //FINDING INVERSE CORDS
        a11 = a11 * recipricol;
        a12 = a12 * recipricol;
        a13 = a13 * recipricol;
        a14 = a14 * recipricol;
        a21 = a21 * recipricol;
        a22 = a22 * recipricol;
        a23 = a23 * recipricol;
        a24 = a24 * recipricol;
        a31 = a31 * recipricol;
        a32 = a32 * recipricol;
        a33 = a33 * recipricol;
        a34 = a34 * recipricol;
        a41 = a41 * recipricol;
        a42 = a42 * recipricol;
        a43 = a43 * recipricol;
        a44 = a44 * recipricol;





        MyMatrix HUGEINVERSEDMATRIX = new MyMatrix(a11, a12, a13, a14, a21, a22, a23, a24, a31, a32, a33, a34, a41, a42, a43, a44);
        return HUGEINVERSEDMATRIX;


        


    }

    public override string ToString()
    {
        string result = GetElement(0, 0) + GetElement(0, 1) + GetElement(0, 2) + GetElement(0, 3) + "\n" +
            GetElement(1, 0) + GetElement(1, 1) + GetElement(1, 2) + GetElement(1, 3) + "\n" +
            GetElement(2, 0) + GetElement(2, 1) + GetElement(2, 2) + GetElement(2, 3) + "\n" +
            GetElement(3, 0) + GetElement(3, 1) + GetElement(3, 2) + GetElement(3, 3) + "\n";
        return result;
    }

    //                                                         SET TRANSFORM

    public void SetTransform(GameObject pObj)
    {
        
        Transform transformObject = pObj.transform;  //Sets the transform properties to our object

        SetPosition(transformObject);
        SetRotation(transformObject);
        SetScale(transformObject);
    }
    //                                                         SET POSITION 

    private void SetPosition(Transform pTransform)
    {
        Vector3 positionVector;
        positionVector.x = GetElement(0, 3);
        positionVector.y = GetElement(1, 3);
        positionVector.z = GetElement(2, 3);
        pTransform.position = positionVector;


    }
    
    

    //                                                          SET SCALE
    private void SetScale(Transform pTransform)
    {
        Vector3 scale;

        MyVector xColumn = new MyVector(GetElement(0, 0), GetElement(1, 0), GetElement(2, 0), GetElement(3, 0));
        float xScale = xColumn.Magnitude();
        MyVector yColumn = new MyVector(GetElement(0, 1), GetElement(1, 1), GetElement(2, 1), GetElement(3, 1));
        float yScale = yColumn.Magnitude();
        MyVector zColumn = new MyVector(GetElement(0, 2), GetElement(1, 2), GetElement(2, 2), GetElement(3, 2));
        float zScale = zColumn.Magnitude();


        scale.x = xScale;
        scale.y = yScale;
        scale.z = zScale;

        pTransform.localScale = scale;

    }



    

    //                                                         SET ROTATION
    private void SetRotation(Transform pTransform) 
    {
        Vector3 forward;
        Vector3 upwards;
        

        forward.x = GetElement(0, 2);
        forward.y = GetElement(1, 2);
        forward.z = GetElement(2, 2);

        upwards.x = GetElement(0, 1);
        upwards.y = GetElement(1, 1);
        upwards.z = GetElement(2, 1);

        pTransform.rotation = Quaternion.LookRotation(forward, upwards);

    }


    //                                                         SET INVERSE
    //public GameObject SetInverse() { return null; }
}
