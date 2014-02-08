#include "Tristrip.h"

using namespace System::Drawing;

namespace Gpc
{
	Tristrip::Tristrip(gpc_tristrip* nativeTristrip)
		: mNativeTristrip(nativeTristrip)
	{}

	Tristrip::~Tristrip()
	{
		this->!Tristrip();
	}

	Tristrip::!Tristrip()
	{
		gpc_free_tristrip(mNativeTristrip);
		delete mNativeTristrip;
		mNativeTristrip = nullptr;
	}

	GraphicsPath^ Tristrip::ToGraphicsPath(GraphicsPathType type)
	{
		ThrowIfDisposed();

		GraphicsPath^ path = gcnew GraphicsPath();

		array<PointF>^ managedVertices;
		if (type == GraphicsPathType::Polygons)
		{
			managedVertices = gcnew array<PointF>(3);
		}

		for (int iStrip = 0;iStrip < mNativeTristrip->num_strips;++ iStrip)
		{
			gpc_vertex_list& nativeVertexList = mNativeTristrip->strip[iStrip];
			for (int iVertex = 0;iVertex < nativeVertexList.num_vertices - 2;++ iVertex)
			{
				gpc_vertex& nativeV1 = nativeVertexList.vertex[iVertex];
				gpc_vertex& nativeV2 = nativeVertexList.vertex[iVertex + 1];
				gpc_vertex& nativeV3 = nativeVertexList.vertex[iVertex + 2];

				if (type == GraphicsPathType::Polygons)
				{
					managedVertices[0] = PointF(nativeV1.x, nativeV1.y);
					managedVertices[1] = PointF(nativeV2.x, nativeV2.y);
					managedVertices[2] = PointF(nativeV3.x, nativeV3.y);
					path->AddPolygon(managedVertices);
				}
				else
				{
					path->AddLine((float)nativeV1.x, nativeV1.y, nativeV2.x, nativeV2.y);
					path->AddLine((float)nativeV2.x, nativeV2.y, nativeV3.x, nativeV3.y);
					path->AddLine((float)nativeV3.x, nativeV3.y, nativeV1.x, nativeV1.y);
				}
			}
		}

		return path;
	}
}