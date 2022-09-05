using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteChat.Core.Utils
{
    /// <summary>
    /// Typed glyph, can be used interchangeably with an <see cref="int"/>
    /// </summary>
    public struct Glyph
    {
        int glyphValue;
        public int Value { get { return glyphValue; } set { glyphValue = value; } }

        public Glyph(int value)
        {
            glyphValue = value;
        }

        public static implicit operator int(Glyph g) => g.glyphValue;
        public static implicit operator Glyph(int i) => new Glyph(i);
    }
}
