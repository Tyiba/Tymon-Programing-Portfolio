
using Microsoft.Xna.Framework;
using MonoGame.ImGui;

namespace Path_Finding
{
    internal class Node
    {
        static int _nextID = 0;

        public Vector2 Position { get; set; }
        public int ID { get; private set; }

        public Node(Vector2 pPosition)
        {
            Position = pPosition;
            ID = _nextID;
            _nextID++;
        }
    }
}
