#pragma once

namespace Gpc
{
	public ref class PolygonFactoryImpl sealed : PolygonFactory
	{
	public:
		PolygonFactoryImpl();
	protected:
		 virtual IPolygon^ _Create() override;
	};
}