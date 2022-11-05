using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road
{

    private MyVector _Position { get; set; }
    private MyVector _Rotation { get; set; }
    private MyVector _Scale { get; set; }
    private SceneGraphNode _RootNode { get; set; }

    public Road(MyVector pPosition, MyVector pRotation, MyVector pScale)
    {
        _Scale = pScale;
        _Position = pPosition;
        _Rotation = pRotation;

    }

    public SceneGraphNode InitialiseRoadSceneGraph()
    {

        MyMatrix Position = MyMatrix.CreateTranslation(_Position);
        // "Root" node for bus // Starts off by using given position, scale and rotation to create nodes for each
        _RootNode = new SceneGraphNode("RootTranslationNode", Position);

        MyMatrix RotationX = MyMatrix.CreateRotationX(_Rotation.X);
        SceneGraphNode RootRotationX = new SceneGraphNode("RootRotationX", RotationX);
        _RootNode.AddChild(RootRotationX);

        MyMatrix RotationY = MyMatrix.CreateRotationY(_Rotation.Y);
        SceneGraphNode RootRotationY = new SceneGraphNode("RootRotationY", RotationY);
        RootRotationX.AddChild(RootRotationY);

        MyMatrix RotationZ = MyMatrix.CreateRotationZ(_Rotation.Z);
        SceneGraphNode RootRotationZ = new SceneGraphNode("RootRotationZ", RotationZ);
        RootRotationY.AddChild(RootRotationZ);

        MyMatrix Scale = MyMatrix.CreateScale(_Scale);
        SceneGraphNode RootScale = new SceneGraphNode("RootScale", Scale);
        RootRotationZ.AddChild(RootScale);


        RootScale.AddChild(BuildRoad());

        return _RootNode;
    }
    
    public SceneGraphNode BuildRoad()
    {
        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Cube);
        road.GetComponent<Renderer>().material.color = Color.grey;

        MyVector roadScale = new MyVector(8f, 0.3f, 20f);
        MyMatrix roadScaleMatrix = MyMatrix.CreateScale(roadScale);

        MyVector roadTranslation = new MyVector(2f, -1.65f, 0);
        MyMatrix roadTranslationMatrix = MyMatrix.CreateTranslation(roadTranslation);

        SceneGraphNode roadNodeScale = new SceneGraphNode("roadScaleNode", roadScaleMatrix, road);
        SceneGraphNode roadNodeTranslation = new SceneGraphNode("roadTranslationNode", roadTranslationMatrix);

        roadNodeTranslation.AddChild(roadNodeScale);

        return roadNodeTranslation;
;    }
}
