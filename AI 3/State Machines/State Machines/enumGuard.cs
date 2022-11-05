using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using IntroductionLab;

namespace State_Machines
{
    internal class enumGuard : Agent
    {
        //States
        private enum State { PATROLLING, PURSUE_AGENT, GOTO_WAYPOINT };
        private State _state;
        //PathFollowing
        private List<Vector2> _path;
        private int _currentPathIndex;
        private float _targetRadius = 30f;
        private List<Wall> _walls;
        private Hero _hero;
        private List<Vector2> _wayPoints;
        private int _wayPointIndex;

        //PathFollowingGuardAgent - From IntroLab Work
        public enumGuard(Vector2 pTarget, Vector2 pPosition, Vector2 pVelocity, float pMaxSpeed, float pRadius, float pMass, Color pColor, List<Wall> pWalls, Hero pHero, List<Vector2> pWayPoints)
            : base(pPosition, pVelocity, pMaxSpeed, pRadius, pMass, pColor, pWalls)
        {
            _state = State.PATROLLING;
            _path = new List<Vector2>();
            _path.Add(pTarget);
            _currentPathIndex = 0;
            _walls = pWalls;
            _hero = pHero;
            _wayPoints = pWayPoints;
            _wayPointIndex = 0;
        }

        public void AddTargetToPath(Vector2 pTarget)
        {
            _path.Add(pTarget);
        }

        //DRAW METHOD
        public override void Draw(ShapeBatcher pShapeBatcher)
        {
            base.Draw(pShapeBatcher);

            switch ( _state )
            {
                case State.PATROLLING:
                    for(int i = 0; i < _path.Count- 1;i++)
                    {
                        pShapeBatcher.DrawArrow(_path[i], _path[i + 1] - _path[i], 2, 5, Color.Red);
                    }
                    pShapeBatcher.DrawCircle(_path[_currentPathIndex], _targetRadius, 20, 2, Color.Gold);
                    break;
                case State.PURSUE_AGENT:
                //  pShapeBatcher.DrawLine(Position, _hero.Position, 2, Color.Red);
                    break;
                case State.GOTO_WAYPOINT:
                //  pShapeBatcher.DrawLine(Position, _waypoints[_targetWaypointIndex],2, Color.Orange);
                    break;
            }
        }
        //UPDATE METHOD
        private void UpdatePatrolling(float pSeconds)
        {
            DesiredVelocity = _path[_currentPathIndex] - Position;
            base.Update(pSeconds);

            if ((Position - _path[_currentPathIndex]).Length() < _targetRadius)
            {
                _currentPathIndex++;
                if(_currentPathIndex == _path.Count)
                {
                    _currentPathIndex = 0;
                }
            }
            // REFRACTORING 
    
            if (CanSeePosition(_hero.Position))
            {
                _state = State.PURSUE_AGENT;
            }

        }

        private bool CanSeePosition(Vector2 pPosition)
        {
            bool canSeePosition = true;

            foreach (Wall wall in _walls)
            {
                if (wall.Intersects(Position, pPosition))
                {
                    canSeePosition = false;
                }
            }
            return canSeePosition;
        }
        private bool HasLineOfSight(Vector2 pWayPoint, Vector2 pPosition)
        {
            bool hasLineOfSight = true;

            foreach (Wall wall in _walls)
            {
                if (wall.Intersects(pWayPoint, pPosition))
                {
                     hasLineOfSight = false;
                }
            }
            return hasLineOfSight;
        }
        private void UpdatePursuing(float pSeconds)
        {
            DesiredVelocity = _hero.Position - Position;
            base.Update(pSeconds);

            
            if(!CanSeePosition(_hero.Position))
            {
                float _timeSearching = 0f;
                float closestDistance = float.MaxValue;

                for (int i = 0; i < _wayPoints.Count; i++)
                {
                    if(CanSeePosition(_wayPoints[i]) && HasLineOfSight(_wayPoints[i], _hero.Position))
                    { 
                        float distance = Vector2.Distance(_hero.Position, _wayPoints[i]);
                        if(distance < closestDistance)
                        {
                            closestDistance = distance;
                            _wayPointIndex = i;
                        }
                    }
                }
                _state = State.GOTO_WAYPOINT;
            }
        }
        private void UpdateSearching(float pSeconds)
        {
            DesiredVelocity = _wayPoints[_wayPointIndex] - Position;
            base.Update(pSeconds);
        }
        

        public override void Update(float pSeconds)
        {
            
           
           switch(_state)
           {
                case State.PATROLLING:
                    UpdatePatrolling(pSeconds);
                    break;
                case State.PURSUE_AGENT:
                    UpdatePursuing(pSeconds);
                    break;
                case State.GOTO_WAYPOINT:
                    UpdateSearching(pSeconds);
                    break;
           }
         

                    
            
            base.Update(pSeconds);
        }

    }
}
