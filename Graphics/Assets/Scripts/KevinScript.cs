using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class KevinScript : MonoBehaviour
{ 
    void Start()
    {
        SceneGraphNode theRootNodeToRuleThemAll = new SceneGraphNode("RootOfAllRoots",MyMatrix.CreateIdentity());

        Bus bus = new Bus(new MyVector(0, 0, 0), new MyVector(0, MathF.PI, 0), new MyVector(1, 1, 1));
        Road road = new Road(new MyVector(0, 0, 0), new MyVector(0, 0, 0), new MyVector(1, 1, 1));
        Bus bus2 = new Bus(new MyVector(4f, 0, -3), new MyVector(0, 0, 0), new MyVector(1, 1, 1));



        theRootNodeToRuleThemAll.AddChild(bus.InitialiseBusSceneGraph());
        theRootNodeToRuleThemAll.AddChild(bus2.InitialiseBusSceneGraph());
        theRootNodeToRuleThemAll.AddChild(road.InitialiseRoadSceneGraph());



        MyVector translate = new MyVector(0, 0, 0);

        MyMatrix Identity = MyMatrix.CreateIdentity();

        MyMatrix ParentMatrix = MyMatrix.CreateTranslation(translate);
        theRootNodeToRuleThemAll.DrawChildren(Identity);
        //GameObject KevinTheCube = GameObject.Find("Kevin");
        //MyVector Vector = new MyVector(50, 30, 10);
        //MyMatrix transMatrix = MyMatrix.CreateTranslation(Vector);
        //MyMatrix scaleMatrix = MyMatrix.CreateScale(new MyVector(2, 1, 1));
        //MyMatrix funkyMatrix = transMatrix.Multiply(scaleMatrix);

        //funkyMatrix.SetTransform(KevinTheCube);





    }
    void Update()
    {
    }
}

    // Update is called once per frame
   
