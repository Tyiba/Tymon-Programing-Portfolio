using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SceneGraphNode
{
    private string _Name { get; set; }
    private MyMatrix _Transform { get; set; }
    private GameObject _GameObject { get; set; }
    
    private List<SceneGraphNode> _Children { get; set; }

    public SceneGraphNode(string pName, MyMatrix pTransform, GameObject pGameObject = null)
    {
        _Name = pName;  //Node Identifier
        _Transform = pTransform; //Nodes Transform Matrix
        _GameObject = pGameObject; //Object of the Node
        _Children = new List<SceneGraphNode>(); //List of all children
        
    }

    public int GetNumberOfChilderen()
    {
        return _Children.Count; 
    }

    public SceneGraphNode GetChildAt(int pIndex) 
    {
        return _Children[pIndex];
    }

    public void AddChild(SceneGraphNode pChild) 
    {
        _Children.Add(pChild);
    }

    public void Draw(MyMatrix pParentTransform)
    { 
        MyMatrix matrix = pParentTransform.Multiply(_Transform);
        // Updates Objects Postion, Rotation and Scale compared to its earlier state

        if (_GameObject != null)
        {
            matrix.SetTransform(_GameObject);
        }
        else
        {
            // If not a leaf node then call its children to draw 
            DrawChildren(matrix);
        }
    }

    public void DrawChildren(MyMatrix pParentTransform)
    {
        //Loop through list of children and draw them
        
        for(int i = 0; i < GetNumberOfChilderen(); i++)
        {
            GetChildAt(i).Draw(pParentTransform);
        }
    }



}
