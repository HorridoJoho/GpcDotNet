#include "PolygonFactoryImpl.h"

#include "Polygon.h"

namespace Gpc
{
	PolygonFactoryImpl::PolygonFactoryImpl()
	{}

	IPolygon^ PolygonFactoryImpl::_Create()
	{
		return gcnew Polygon();
	}
}