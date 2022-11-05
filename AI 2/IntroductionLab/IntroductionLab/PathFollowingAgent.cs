using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace IntroductionLab
{
    internal class PathFollowingAgent : Agent
    {

        private List<Vector2> _path;
        private int _currentPathIndex;
        private bool _reverse = false;

        public PathFollowingAgent(Vector2 pTarget, Vector2 pPosition, Vector2 pVelocity, float pMaxSpeed, float pRadius, float pMass, Color pColor)
            : base(pPosition, pVelocity, pMaxSpeed, pRadius, pMass, pColor)
        {
            _path = new List<Vector2>();
            _path.Add(pTarget);
            _currentPathIndex = 0;

        }
        public void AddTargetToPath(Vector2 pTarget)
        {
            _path.Add(pTarget);
        }

        public override void Draw(ShapeBatcher pShapeBatcher)
        {
            base.Draw(pShapeBatcher);

            if (_reverse == false) 
            {
                for (int i = 0; i < _path.Count - 1; i++)
                {
                    pShapeBatcher.DrawArrow(_path[i], _path[i + 1] - _path[i], 2, 5, Color.Red);
                }
            }

            // WANTING TO SHOW PATH IN REAL TIME BY REVERSING THE ARROWS BUT DONT WORK ??
            if (_reverse == true)
            {
                for (int i = 4; i > _path.Count - 1; i--)
                {
                    pShapeBatcher.DrawArrow(_path[i], _path[i] - _path[i - 1], 2, 5, Color.Red);
                }
            }
            /*pShapeBatcher.DrawArrow(_path[5], _path[0] - _path[5], 2, 5, Color.Red); */ //Loop arrow

            if (_currentPathIndex <= _path.Count - 1)
            { 
                pShapeBatcher.DrawCircle(_path[_currentPathIndex], 5, 20, 2, Color.Gold);
            }

            



            

        }

        public override void Update(float pSeconds)
        {
            //
            // REVERSE AND LOOP
            //
            //
            DesiredVelocity = _path[_currentPathIndex] - Position;

            if (_reverse == false)
            {
                
                if (_currentPathIndex < _path.Count - 1)
                {
                    if ((Position - _path[_currentPathIndex]).Length() < 30f)
                    {
                      _currentPathIndex++;  
                    }
                }
                if(_currentPathIndex == _path.Count - 1)
                {   
                    _reverse = true;
                    
                }
            }



            if (_reverse == true)
            { 
                if (-(_currentPathIndex) < 1)
                {
                    if ((Position - _path[_currentPathIndex]).Length() < 25f)
                    {
                        _currentPathIndex--;
                    }
                }
                //Delete this if below to make it reverse just once
                if (_currentPathIndex == 0)
                {
                    _reverse = false;
                }

            }




            //
            // LOOPING SYSTEM - Change _path.Count-1 to _path.Count due to the 'new' node / restarting of loop
            //                - add if current Index = path count resent the index which resets the loop
            //

            //DesiredVelocity = _path[_currentPathIndex] - Position;
            //if (_currentPathIndex < _path.Count)
            //{
            //    if ((Position - _path[_currentPathIndex]).Length() < 25f)
            //    {
            //        _currentPathIndex++;
            //    }
            //}

            //if (_currentPathIndex == _path.Count)
            //{
            //    _currentPathIndex = 0;
            //}

            base.Update(pSeconds);
        }
    }
}
