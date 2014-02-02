using System;
using System.Drawing.Drawing2D;

namespace Gpc
{
    public interface ITristrip : IDisposable
    {
        GraphicsPath ToGraphicsPath(GraphicsPathType type);
    }
}
