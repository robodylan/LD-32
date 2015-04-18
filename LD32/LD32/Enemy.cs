using Dunn_2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD32
{
    class Enemy : Entity
    {
        public Enemy(string filename, int x, int y, bool hasPhysics)
            : base(filename, x, y, hasPhysics)
        {
        }

        public override void Move(float x, float y)
        {

        }
    }
}
