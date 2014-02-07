#include "PolygonFactoryImpl.h"

#include "Polygon.h"

using namespace System::Text;

namespace Gpc
{
	PolygonFactoryImpl::PolygonFactoryImpl()
	{}

	IPolygon^ PolygonFactoryImpl::_Create()
	{
		return gcnew Polygon();
	}

	IPolygon^ PolygonFactoryImpl::_Read(TextReader^ reader, Boolean readHoleFlags)
	{
		gpc_polygon* nativePolygon = new gpc_polygon;

		TextReaderExtensions::SkipWhitespaces(reader);
		TextReaderExtensions::ScanInt32(reader, nativePolygon->num_contours);

		nativePolygon->contour = new gpc_vertex_list[nativePolygon->num_contours];
		nativePolygon->hole = new int[nativePolygon->num_contours];

		for (int iContour = 0; iContour < nativePolygon->num_contours;++ iContour)
		{
			gpc_vertex_list& nativeVertexList = nativePolygon->contour[iContour];
			int& hole = nativePolygon->hole[iContour];

			TextReaderExtensions::SkipWhitespaces(reader);
			TextReaderExtensions::ScanInt32(reader, nativeVertexList.num_vertices);
			hole = 0;
			if (readHoleFlags)
			{
				Boolean isHole;
				TextReaderExtensions::SkipWhitespaces(reader);
				TextReaderExtensions::ScanBoolean(reader, isHole);
				if (isHole)
				{
					hole = 1;
				}
			}

			nativeVertexList.vertex = new gpc_vertex[nativeVertexList.num_vertices];

			for (int iVertex = 0; iVertex < nativeVertexList.num_vertices; ++iVertex)
			{
				gpc_vertex& nativeVertex = nativeVertexList.vertex[iVertex];
				TextReaderExtensions::SkipWhitespaces(reader);
				TextReaderExtensions::ScanDouble(reader, nativeVertex.x);
				TextReaderExtensions::SkipWhitespaces(reader);
				TextReaderExtensions::ScanDouble(reader, nativeVertex.y);
			}
		}

		return gcnew Polygon(nativePolygon);
	}
}