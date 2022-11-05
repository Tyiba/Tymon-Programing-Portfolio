using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Path_Finding
{
    internal class Edge
    {
        static int _nextID = 0;
        public int From { get; private set; }
        public int To { get; private set; }

        public int ID { get; private set; }
 
        public Edge(int pFrom, int pTo)
        {
            From = pFrom;
            To = pTo;
            ID = _nextID;
            _nextID++;
        }
    }
}
