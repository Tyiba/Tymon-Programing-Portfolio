using IntroductionLab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_Machines
{
    internal class McGuffin
    {
        public Vector2 Position { get { return _spawnLocations[_spawnLocationIndex]; } }
        public float Radius { get; private set; }

        private int _spawnLocationIndex;

        private List<Vector2> _spawnLocations;

        private SoundEffectInstance _soundEffectInstance;

        public McGuffin(Vector2 pPosition, float pRadius, SoundEffect pSoundEffect)
        {
            _soundEffectInstance = pSoundEffect.CreateInstance();
            _spawnLocations = new List<Vector2>();
            _spawnLocations.Add(pPosition);
            _spawnLocationIndex = 0;
            Radius = pRadius;
        }

        public void AddNewSpawnLocation(Vector2 pSpawnLocation)
        {
            _spawnLocations.Add(pSpawnLocation);
        }

        public virtual void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(Position, Radius, 32, 2, Color.Gold);
        }

        public void CollectMcGuffin()
        {
            _soundEffectInstance.Play();
            if (_spawnLocations.Count <= 1)
            {
                return;
            }

            int currentIndex = _spawnLocationIndex;

            do
            {
                _spawnLocationIndex = Utility.GetRandomInteger(0, _spawnLocations.Count);
            } while(_spawnLocationIndex == currentIndex);
        }
    }
}
