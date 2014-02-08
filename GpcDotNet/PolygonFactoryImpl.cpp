#include "PolygonFactoryImpl.h"

#include "Polygon.h"

#include "gpc.h"

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
		Int32 nContours;
		if (!TextReaderExtensions::ScanInt32(reader, nContours))
		{
			delete nativePolygon;
			throw gcnew Exception("Expected countour count!");
		}

		nativePolygon->num_contours = 0;
		nativePolygon->contour = new gpc_vertex_list[nContours];
		nativePolygon->hole = new int[nContours];

		for (int iContour = 0; iContour < nContours;++ iContour)
		{
			++ nativePolygon->num_contours;

			gpc_vertex_list& nativeVertexList = nativePolygon->contour[iContour];
			int& hole = nativePolygon->hole[iContour];

			nativeVertexList.vertex = nullptr;

			TextReaderExtensions::SkipWhitespaces(reader);
			if (!TextReaderExtensions::ScanInt32(reader, nativeVertexList.num_vertices))
			{
				gpc_free_polygon(nativePolygon);
				delete nativePolygon;
				throw gcnew Exception("Expected vertex count!");
			}

			hole = 0;
			if (readHoleFlags)
			{
				Boolean isHole;
				TextReaderExtensions::SkipWhitespaces(reader);
				if (!TextReaderExtensions::ScanBoolean(reader, isHole))
				{
					gpc_free_polygon(nativePolygon);
					delete nativePolygon;
					throw gcnew Exception("Expected hole flag!");
				}
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
				if (!TextReaderExtensions::ScanDouble(reader, nativeVertex.x))
				{
					gpc_free_polygon(nativePolygon);
					delete nativePolygon;
					throw gcnew Exception("Expected vertex x!");
				}
				TextReaderExtensions::SkipWhitespaces(reader);
				if (!TextReaderExtensions::ScanDouble(reader, nativeVertex.y))
				{
					gpc_free_polygon(nativePolygon);
					delete nativePolygon;
					throw gcnew Exception("Expected vertex y!");
				}
			}
		}

		return gcnew Polygon(nativePolygon);
	}
}