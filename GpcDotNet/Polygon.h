#pragma once

#include "gpc.h"

#include "AbstractDisposable.h"
#include "Tristrip.h"

using namespace System;
using namespace System::Drawing;
using namespace System::IO;

namespace Gpc
{
	public ref class Polygon sealed : IPolygon, AbstractDisposable
	{
	private:
		gpc_polygon* mNativePolygon;
	internal:
		Polygon(gpc_polygon* nativePolygon);
		Polygon();
	public:
		~Polygon();
		!Polygon();

		virtual void Write(TextWriter^ writer, Boolean includeHoleFlags);

		virtual void AddContour(array<PointF>^ contour, Boolean isHole);

		virtual void AddContour(PointF p1, PointF p2, PointF p3, Boolean isHole);

		virtual void AddContour(PointF p1, PointF p2, PointF p3, PointF p4, Boolean isHole);
		virtual void AddContour(RectangleF contour, Boolean isHole);

		virtual IPolygon^ ClipPolygon(IPolygon^ clipPolygon, ClipOp op);
		virtual ITristrip^ ClipTristrip(IPolygon^ clipPolygon, ClipOp op);

		virtual ITristrip^ ToTristrip();

		virtual GraphicsPath^ ToGraphicsPath(ContourType contourType, GraphicsPathType pathType);

		virtual IntPtr GetNativeHandle();
	};
}