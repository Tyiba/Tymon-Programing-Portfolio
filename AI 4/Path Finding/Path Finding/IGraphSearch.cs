using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_Finding
{
    internal interface IGraphSearch
    {
        void Update(float pSeconds);
        void DrawShapes(ShapeBatcher pShapeBatcher);

        void DrawSprites(SpriteBatch pSpriteBatcher, SpriteFont pFont, float pHeight);
    }
}
