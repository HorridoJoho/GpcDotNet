#include "Polygon.h"

using namespace System::Text;

namespace Gpc
{
	Polygon::Polygon(gpc_polygon* nativePolygon)
		: mNativePolygon(nativePolygon)
	{}

	Polygon::Polygon()
		: mNativePolygon(new gpc_polygon)
	{
		mNativePolygon->num_contours = 0;
		mNativePolygon->hole = nullptr;
		mNativePolygon->contour = nullptr;
	}

	Polygon::~Polygon()
	{
		gpc_free_polygon(mNativePolygon);
	}

	void Polygon::Write(TextWriter^ writer, Boolean includeHoleFlags)
	{
		writer->WriteLine(mNativePolygon->num_contours);
		for (int iContour = 0;iContour < mNativePolygon->num_contours;++ iContour)
		{
			gpc_vertex_list& nativeContour = mNativePolygon->contour[iContour];
			writer->WriteLine(nativeContour.num_vertices);
			if (includeHoleFlags)
				writer->WriteLine(mNativePolygon->hole[iContour]);
			for (int iVertex = 0;iVertex < nativeContour.num_vertices;++ iVertex)
			{
				gpc_vertex& nativeVertex = nativeContour.vertex[iVertex];
				writer->Write(nativeVertex.x);
				writer->Write((Char)' ');
				writer->WriteLine(nativeVertex.y);
			}
		}
		writer->Flush();
	}

	void Polygon::AddContour(array<PointF>^ contour, Boolean isHole)
	{
		ThrowIfDisposed();
		if (contour->Length < 3)
		{
			throw gcnew ArgumentException("At least 3 vertices required.");
		}

		gpc_vertex_list nativeVertexList;
		nativeVertexList.num_vertices = contour->Length;
		nativeVertexList.vertex = new gpc_vertex[contour->Length];

		for (int iVertex = 0;iVertex < contour->Length;++ iVertex)
		{
			nativeVertexList.vertex[iVertex].x = contour[iVertex].X;
			nativeVertexList.vertex[iVertex].y = contour[iVertex].Y;
		}

		gpc_add_contour(mNativePolygon, &nativeVertexList, isHole ? 1 : 0);
		delete[] nativeVertexList.vertex;
	}

	void Polygon::AddContour(PointF p1, PointF p2, PointF p3, Boolean isHole)
	{
		ThrowIfDisposed();
		gpc_vertex nativeVertices[3];
		gpc_vertex_list nativeVertexList;
		nativeVertexList.num_vertices = 3;
		nativeVertexList.vertex = nativeVertices;

		nativeVertexList.vertex[0].x = p1.X;
		nativeVertexList.vertex[0].y = p1.Y;
		nativeVertexList.vertex[1].x = p2.X;
		nativeVertexList.vertex[1].y = p2.Y;
		nativeVertexList.vertex[2].x = p3.X;
		nativeVertexList.vertex[2].y = p3.Y;

		gpc_add_contour(mNativePolygon, &nativeVertexList, isHole ? 1 : 0);
	}

	void Polygon::AddContour(PointF p1, PointF p2, PointF p3, PointF p4, Boolean isHole)
	{
		ThrowIfDisposed();
		gpc_vertex nativeVertices[4];
		gpc_vertex_list nativeVertexList;
		nativeVertexList.num_vertices = 4;
		nativeVertexList.vertex = nativeVertices;

		nativeVertexList.vertex[0].x = p1.X;
		nativeVertexList.vertex[0].y = p1.Y;
		nativeVertexList.vertex[1].x = p2.X;
		nativeVertexList.vertex[1].y = p2.Y;
		nativeVertexList.vertex[2].x = p3.X;
		nativeVertexList.vertex[2].y = p3.Y;
		nativeVertexList.vertex[3].x = p4.X;
		nativeVertexList.vertex[3].y = p4.Y;

		gpc_add_contour(mNativePolygon, &nativeVertexList, isHole ? 1 : 0);
	}

	void Polygon::AddContour(RectangleF contour, Boolean isHole)
	{
		ThrowIfDisposed();
		gpc_vertex nativeVertices[4];
		gpc_vertex_list nativeVertexList;
		nativeVertexList.num_vertices = 4;
		nativeVertexList.vertex = nativeVertices;

		nativeVertexList.vertex[0].x = contour.Location.X;
		nativeVertexList.vertex[0].y = contour.Location.Y;
		nativeVertexList.vertex[1].x = contour.Left - 1;
		nativeVertexList.vertex[1].y = contour.Location.Y;
		nativeVertexList.vertex[2].x = contour.Left - 1;
		nativeVertexList.vertex[2].y = contour.Bottom - 1;
		nativeVertexList.vertex[3].x = contour.Location.X;
		nativeVertexList.vertex[3].y = contour.Bottom - 1;

		gpc_add_contour(mNativePolygon, &nativeVertexList, isHole ? 1 : 0);
	}

	IPolygon^ Polygon::ClipPolygon(IPolygon^ clipPolygon, ClipOp op)
	{
		ThrowIfDisposed();
		gpc_polygon* nativePolygon = new gpc_polygon;
		gpc_polygon_clip((gpc_op)op, mNativePolygon, (gpc_polygon*)clipPolygon->GetNativeHandle().ToPointer(), nativePolygon);
		return gcnew Polygon(nativePolygon);
	}

	ITristrip^ Polygon::ClipTristrip(IPolygon^ clipPolygon, ClipOp op)
	{
		ThrowIfDisposed();
		gpc_tristrip* nativeTrisstrip = new gpc_tristrip;
		gpc_tristrip_clip((gpc_op)op, mNativePolygon, (gpc_polygon*)clipPolygon->GetNativeHandle().ToPointer(), nativeTrisstrip);
		return gcnew Tristrip(nativeTrisstrip);
	}

	ITristrip^ Polygon::ToTristrip()
	{
		ThrowIfDisposed();
		gpc_tristrip* nativeTristrip = new gpc_tristrip;
		gpc_polygon_to_tristrip(mNativePolygon, nativeTristrip);
		return gcnew Tristrip(nativeTristrip);
	}

	GraphicsPath^ Polygon::ToGraphicsPath(ContourType contourType, GraphicsPathType pathType)
	{
		GraphicsPath^ path = gcnew GraphicsPath();
		array<PointF>^ managedVertices = nullptr;
		for (int iContour = 0;iContour < mNativePolygon->num_contours;++ iContour)
		{
			bool isHole = mNativePolygon->hole[iContour] != 0;
			if (contourType != ContourType::All && ((isHole && contourType != ContourType::Hollow) || (!isHole && contourType != ContourType::Filled)))
				continue;

			gpc_vertex_list& nativeVertexList = mNativePolygon->contour[iContour];

			if (pathType == GraphicsPathType::Polygons && (managedVertices == nullptr || managedVertices->Length != nativeVertexList.num_vertices))
				managedVertices = gcnew array<PointF>(nativeVertexList.num_vertices);

			for (int iVertex = 0;iVertex < nativeVertexList.num_vertices;++iVertex)
			{
				gpc_vertex* nativeVertex;
				if (isHole)
					nativeVertex = &nativeVertexList.vertex[nativeVertexList.num_vertices - 1 - iVertex];
				else
					nativeVertex = &nativeVertexList.vertex[iVertex];

				if (pathType == GraphicsPathType::Polygons)
				{
					nativeVertex = &nativeVertexList.vertex[iVertex];
					if (isHole)
						managedVertices[nativeVertexList.num_vertices - 1 - iVertex] = PointF(nativeVertex->x, nativeVertex->y);
					else
						managedVertices[iVertex] = PointF(nativeVertex->x, nativeVertex->y);
				}
				else
				{
					gpc_vertex* nativeVertexSecond;
					if (isHole)
					{
						Int32 iVertexReverse = nativeVertexList.num_vertices - 1 - iVertex;
						if (iVertexReverse == 0)
							nativeVertexSecond = &nativeVertexList.vertex[nativeVertexList.num_vertices - 1];
						else
							nativeVertexSecond = &nativeVertexList.vertex[iVertexReverse - 1];
					}
					else
					{
						nativeVertex = &nativeVertexList.vertex[iVertex];
						if (iVertex == nativeVertexList.num_vertices - 1)
							nativeVertexSecond = &nativeVertexList.vertex[0];
						else
							nativeVertexSecond = &nativeVertexList.vertex[iVertex + 1];
					}

					path->AddLine((float)nativeVertex->x, nativeVertex->y, nativeVertexSecond->x, nativeVertexSecond->y);
				}
			}

			if (pathType == GraphicsPathType::Polygons)
				path->AddPolygon(managedVertices);
		}

		return path;
	}

	IntPtr Polygon::GetNativeHandle()
	{
		return IntPtr(mNativePolygon);
	}
}