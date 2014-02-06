using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Gpc
{
    public interface IPolygon : IDisposable
    {
        void Write(Stream output, Boolean includeHoleFlags);

        void AddContour(PointF[] contour, Boolean isHole);
        void AddContour(PointF p1, PointF p2, PointF p3, Boolean isHole);
        void AddContour(PointF p1, PointF p2, PointF p3, PointF p4, Boolean isHole);
        void AddContour(RectangleF contour, Boolean isHole);

		IPolygon ClipPolygon(IPolygon clipPolygon, ClipOp op);
		ITristrip ClipTristrip(IPolygon clipPolygon, ClipOp op);

		ITristrip ToTristrip();

		GraphicsPath ToGraphicsPath(ContourType contourType, GraphicsPathType pathType);

        IntPtr GetNativeHandle();
    }
}
