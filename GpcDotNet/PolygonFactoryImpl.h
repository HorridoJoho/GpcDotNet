#pragma once

using namespace System;
using namespace System::IO;

namespace Gpc
{
	public ref class PolygonFactoryImpl sealed : PolygonFactory
	{
	public:
		PolygonFactoryImpl();
	protected:
		virtual IPolygon^ _Create() override;
		virtual IPolygon^ _Read(TextReader^ reader, Boolean readHoleFlags) override;
	};
}