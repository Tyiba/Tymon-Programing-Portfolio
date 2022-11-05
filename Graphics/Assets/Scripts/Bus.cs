using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bus
{
    private MyVector _Position { get; set; }
    private MyVector _Rotation { get; set; }
    private MyVector _Scale { get; set; }
    private SceneGraphNode _RootNode { get; set; }

    public Bus(MyVector pPosition,MyVector pRotation,MyVector pScale)
    {
        _Scale = pScale;
        _Position = pPosition;
        _Rotation = pRotation;
 
    }
    public SceneGraphNode InitialiseBusSceneGraph()
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


        // Adds entirety of bus into root scale 
        RootScale.AddChild(BuildBus());

        return _RootNode;
        

    }
    public SceneGraphNode BuildBus()
    {
        //MyMatrix Scale = MyMatrix.CreateScale(_Scale);
        //MyMatrix Position = MyMatrix.CreateTranslation(_Position);
        //MyMatrix RotationX = MyMatrix.CreateRotationX(_Rotation.X);
        //MyMatrix RotationY = MyMatrix.CreateRotationY(_Rotation.Y);
        //MyMatrix RotationZ = MyMatrix.CreateRotationZ(_Rotation.Z);
        //MyMatrix ParentMatrix = Position.Multiply(RotationZ.Multiply(RotationY.Multiply(RotationX.Multiply(Scale))));

        SceneGraphNode BigRedBus = new SceneGraphNode("BusToHoldAllOfTheBus", MyMatrix.CreateIdentity());

        BigRedBus.AddChild(BuildBase());
        BigRedBus.AddChild(BuildRoof());
        BigRedBus.AddChild(BuildWheels());
        BigRedBus.AddChild(BuildSideWindows());
        BigRedBus.AddChild(BuildMainWindows());
        

        return BigRedBus;
    }

    public SceneGraphNode BuildBase()
    {
        //      SCENE GRAPH METHOD 

        GameObject busBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
        busBase.GetComponent<Renderer>().material.color = Color.red;
      
        MyVector bodyBottomScale = new MyVector(3, 2, 4);
        MyMatrix busBaseMatrix = MyMatrix.CreateScale(bodyBottomScale);


        //Hold Each Transform in separate node// this is reapeated in each component of bus
        SceneGraphNode busBaseNodeTranslation = new SceneGraphNode("busBaseNodeTranslation", MyMatrix.CreateIdentity());
        SceneGraphNode busBaseNodeRotation = new SceneGraphNode("busBaseNodeRotation", MyMatrix.CreateIdentity());
        SceneGraphNode busBaseNodeScale = new SceneGraphNode("busBaseNodeScale", busBaseMatrix ,busBase);

        
        busBaseNodeTranslation.AddChild(busBaseNodeRotation);
        busBaseNodeRotation.AddChild(busBaseNodeScale);

        return busBaseNodeTranslation;
    }

    public SceneGraphNode BuildRoof() 
    {
        GameObject busRoof = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        busRoof.GetComponent<Renderer>().material.color = Color.black; //Black Roof

        MyVector scale = new MyVector(2.9f, 1.9f, 1f); //shy of 2 to avoid weird overlap issues.
        MyMatrix roofScale = MyMatrix.CreateScale(scale);

        //Rotate Roof in direction of Body
        MyMatrix roofRotation = MyMatrix.CreateRotationX((float)(Math.PI/2f));
        
        //Move Roof On Top of Body
        MyVector roofTranslation = new MyVector(0, 1, 0);
        MyMatrix roofTransMat = MyMatrix.CreateTranslation(roofTranslation);

        SceneGraphNode busRoofTranslation = new SceneGraphNode("busRoofTranslation", roofTransMat);
        SceneGraphNode busRoofRotation = new SceneGraphNode("busRoofRotationX", roofRotation);
        SceneGraphNode busRoofScale = new SceneGraphNode("busRoofScale", roofScale, busRoof);

       
        busRoofTranslation.AddChild(busRoofRotation);
        busRoofRotation.AddChild(busRoofScale);

            
        return busRoofTranslation;
        
    }
    public SceneGraphNode BuildWheels() 
    {
        GameObject Wheel1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject Wheel2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject Wheel3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject Wheel4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        Wheel1.GetComponent<Renderer>().material.color = Color.black;
        Wheel2.GetComponent<Renderer>().material.color = Color.black;
        Wheel3.GetComponent<Renderer>().material.color = Color.black;
        Wheel4.GetComponent<Renderer>().material.color = Color.black;

        //Creating Common Scale Vector For Disk
        MyVector scale = new MyVector(1, 0.1f, 1);

        //Creating Translations Vectors for all wheels 
        MyVector translation1 = new MyVector(1.5f, -1, 1);
        MyVector translation2 = new MyVector(1.5f, -1, -1);
        MyVector translation3 = new MyVector(-1.5f, -1, 1);
        MyVector translation4 = new MyVector(-1.5f, -1, -1);

        MyMatrix wheelScale = MyMatrix.CreateScale(scale);

        MyMatrix wheel1Translation = MyMatrix.CreateTranslation(translation1);
        MyMatrix wheel2Translation = MyMatrix.CreateTranslation(translation2);
        MyMatrix wheel3Translation = MyMatrix.CreateTranslation(translation3);
        MyMatrix wheel4Translation = MyMatrix.CreateTranslation(translation4);

        // Rotation Matrix For Wheels
        MyMatrix wheelRotation = MyMatrix.CreateRotationZ((float)Math.PI/2);

        SceneGraphNode wheel1NodeTranslation = new SceneGraphNode("Wheel1Translation", wheel1Translation);
        SceneGraphNode wheel2NodeTranslation = new SceneGraphNode("Wheel2Translation", wheel2Translation);
        SceneGraphNode wheel3NodeTranslation = new SceneGraphNode("Wheel3Translation", wheel3Translation);
        SceneGraphNode wheel4NodeTranslation = new SceneGraphNode("Wheel4Translation", wheel4Translation);

        SceneGraphNode wheel1NodeRotation = new SceneGraphNode("Wheel1Rotation", wheelRotation);
        SceneGraphNode wheel2NodeRotation = new SceneGraphNode("Wheel2Rotation", wheelRotation);
        SceneGraphNode wheel3NodeRotation = new SceneGraphNode("Wheel3Rotation", wheelRotation);
        SceneGraphNode wheel4NodeRotation = new SceneGraphNode("Wheel4Rotation", wheelRotation);

        SceneGraphNode wheel1NodeScale = new SceneGraphNode("Wheel1Scale", wheelScale, Wheel1);
        SceneGraphNode wheel2NodeScale = new SceneGraphNode("Wheel2Scale", wheelScale, Wheel2);
        SceneGraphNode wheel3NodeScale = new SceneGraphNode("Wheel3Scale", wheelScale, Wheel3);
        SceneGraphNode wheel4NodeScale = new SceneGraphNode("Wheel4Scale", wheelScale, Wheel4);

        SceneGraphNode allWheels = new SceneGraphNode("WheelsRootNode", MyMatrix.CreateIdentity());

        allWheels.AddChild(wheel1NodeTranslation);
        allWheels.AddChild(wheel2NodeTranslation);
        allWheels.AddChild(wheel3NodeTranslation);
        allWheels.AddChild(wheel4NodeTranslation);

        wheel1NodeTranslation.AddChild(wheel1NodeRotation);
        wheel1NodeRotation.AddChild(wheel1NodeScale);

        wheel2NodeTranslation.AddChild(wheel2NodeRotation);
        wheel2NodeRotation.AddChild(wheel2NodeScale);

        wheel3NodeTranslation.AddChild(wheel3NodeRotation);
        wheel3NodeRotation.AddChild(wheel3NodeScale);

        wheel4NodeTranslation.AddChild(wheel4NodeRotation);
        wheel4NodeRotation.AddChild(wheel4NodeScale);



       return allWheels;

    }

    
    
    public SceneGraphNode BuildSideWindows() 
    {
        GameObject sideRight1Window = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject sideRight2Window = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject sideRight3Window = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject sideLeft1Window = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject sideLeft2Window = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject sideLeft3Window = GameObject.CreatePrimitive(PrimitiveType.Cube);

        sideRight1Window.GetComponent<Renderer>().material.color = Color.blue; //Blue
        sideRight2Window.GetComponent<Renderer>().material.color = Color.blue; //Blue
        sideRight3Window.GetComponent<Renderer>().material.color = Color.blue; //Blue

        sideLeft1Window.GetComponent<Renderer>().material.color = Color.blue; //Windows
        sideLeft2Window.GetComponent<Renderer>().material.color = Color.blue; //Windows
        sideLeft3Window.GetComponent<Renderer>().material.color = Color.blue; //Windows


        //Translation Vectors for each windows
        //X = 1.15 ,Y = 0.2, Z = 1.15,0,-1.15
        //Right
        //X = -X of Right

        //Right Windows
        MyVector translate1 = new MyVector(1.15f, 0.2f, 1.15f);
        MyVector translate2 = new MyVector(1.15f, 0.2f, 0);
        MyVector translate3 = new MyVector(1.15f, 0.2f, -1.15f);

        //Left Windows
        MyVector translate4 = new MyVector(-1.15f, 0.2f, 1.15f);
        MyVector translate5 = new MyVector(-1.15f, 0.2f, 0);
        MyVector translate6 = new MyVector(-1.15f, 0.2f, -1.15f);

        MyMatrix sideRightWindow1Translation = MyMatrix.CreateTranslation(translate1);
        MyMatrix sideRightWindow2Translation = MyMatrix.CreateTranslation(translate2);
        MyMatrix sideRightWindow3Translation = MyMatrix.CreateTranslation(translate3);

        MyMatrix sideLeftWindow1Translation = MyMatrix.CreateTranslation(translate4);
        MyMatrix sideLeftWindow2Translation = MyMatrix.CreateTranslation(translate5);
        MyMatrix sideLeftWindow3Translation = MyMatrix.CreateTranslation(translate6);

        // Create Side Window Nodes

        //R
        SceneGraphNode sidewindow1NodeTranslation = new SceneGraphNode("sidewindow1NodeTranslate", sideRightWindow1Translation);
        SceneGraphNode sidewindow2NodeTranslation = new SceneGraphNode("sidewindow2NodeTranslate", sideRightWindow2Translation);
        SceneGraphNode sidewindow3NodeTranslation = new SceneGraphNode("sidewindow3NodeTranslate", sideRightWindow3Translation);
        //L
        SceneGraphNode sidewindow4NodeTranslation = new SceneGraphNode("sidewindow4NodeTranslate", sideLeftWindow1Translation);
        SceneGraphNode sidewindow5NodeTranslation = new SceneGraphNode("sidewindow5NodeTranslate", sideLeftWindow2Translation);
        SceneGraphNode sidewindow6NodeTranslation = new SceneGraphNode("sidewindow6NodeTranslate", sideLeftWindow3Translation);

        //R
        SceneGraphNode sidewindow1NodeRotation = new SceneGraphNode("sidewindow1NodeRot", MyMatrix.CreateIdentity());
        SceneGraphNode sidewindow2NodeRotation = new SceneGraphNode("sidewindow2NodeRot", MyMatrix.CreateIdentity());
        SceneGraphNode sidewindow3NodeRotation = new SceneGraphNode("sidewindow3NodeRot", MyMatrix.CreateIdentity());
        //L
        SceneGraphNode sidewindow4NodeRotation = new SceneGraphNode("sidewindow4NodeRot", MyMatrix.CreateIdentity());
        SceneGraphNode sidewindow5NodeRotation = new SceneGraphNode("sidewindow5NodeRot", MyMatrix.CreateIdentity());
        SceneGraphNode sidewindow6NodeRotation = new SceneGraphNode("sidewindow6NodeRot", MyMatrix.CreateIdentity());

        //R
        SceneGraphNode sidewindow1NodeScale = new SceneGraphNode("sidewindow1NodeScale", MyMatrix.CreateIdentity(), sideRight1Window);
        SceneGraphNode sidewindow2NodeScale = new SceneGraphNode("sidewindow2NodeScale", MyMatrix.CreateIdentity(), sideRight2Window);
        SceneGraphNode sidewindow3NodeScale = new SceneGraphNode("sidewindow3NodeScale", MyMatrix.CreateIdentity(), sideRight3Window);
        //L
        SceneGraphNode sidewindow4NodeScale = new SceneGraphNode("sidewindow4NodeScale", MyMatrix.CreateIdentity(), sideLeft1Window);
        SceneGraphNode sidewindow5NodeScale = new SceneGraphNode("sidewindow5NodeScale", MyMatrix.CreateIdentity(), sideLeft2Window);
        SceneGraphNode sidewindow6NodeScale = new SceneGraphNode("sidewindow6NodeScale", MyMatrix.CreateIdentity(), sideLeft3Window);

        SceneGraphNode allSideWindows = new SceneGraphNode("WindowsNode", MyMatrix.CreateIdentity());

        allSideWindows.AddChild(sidewindow1NodeTranslation);
        allSideWindows.AddChild(sidewindow2NodeTranslation);
        allSideWindows.AddChild(sidewindow3NodeTranslation);
        allSideWindows.AddChild(sidewindow4NodeTranslation);
        allSideWindows.AddChild(sidewindow5NodeTranslation);
        allSideWindows.AddChild(sidewindow6NodeTranslation);


        sidewindow1NodeTranslation.AddChild(sidewindow1NodeRotation);
        sidewindow1NodeRotation.AddChild(sidewindow1NodeScale);

        sidewindow2NodeTranslation.AddChild(sidewindow2NodeRotation);
        sidewindow2NodeRotation.AddChild(sidewindow2NodeScale);

        sidewindow3NodeTranslation.AddChild(sidewindow3NodeRotation);
        sidewindow3NodeRotation.AddChild(sidewindow3NodeScale);

        sidewindow4NodeTranslation.AddChild(sidewindow4NodeRotation);
        sidewindow4NodeRotation.AddChild(sidewindow4NodeScale);

        sidewindow5NodeTranslation.AddChild(sidewindow5NodeRotation);
        sidewindow5NodeRotation.AddChild(sidewindow5NodeScale);

        sidewindow6NodeTranslation.AddChild(sidewindow6NodeRotation);
        sidewindow6NodeRotation.AddChild(sidewindow6NodeScale);


        return allSideWindows;
    }

    public SceneGraphNode BuildMainWindows()
    {
        GameObject frontWindow = GameObject.CreatePrimitive(PrimitiveType.Cube);
        frontWindow.GetComponent<Renderer>().material.color = Color.blue;

        MyVector scale = new MyVector(2.8f, 1.2f, 0.2f);
        MyMatrix windowScale = MyMatrix.CreateScale(scale);

        MyVector translation = new MyVector(0, 0, 2);
        MyMatrix windowTranslation = MyMatrix.CreateTranslation(translation);   

        SceneGraphNode mainWindowNodeTranslation = new SceneGraphNode("FrontWindowTranslation", windowTranslation);
        SceneGraphNode mainWindowNodeRotation = new SceneGraphNode("FrontWindowNodeRot", MyMatrix.CreateIdentity());
        SceneGraphNode mainWindowNodeScale = new SceneGraphNode("FrontWindowNodeScl", windowScale, frontWindow);

        mainWindowNodeTranslation.AddChild(mainWindowNodeRotation);
        mainWindowNodeRotation.AddChild(mainWindowNodeScale);

        return mainWindowNodeTranslation;



    }






}
