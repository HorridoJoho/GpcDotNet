#pragma once

#include "gpc.h"

#include "AbstractDisposable.h"

using namespace System::Drawing::Drawing2D;

namespace Gpc
{
	public ref class Tristrip sealed : ITristrip, AbstractDisposable
	{
	private:
		gpc_tristrip* mNativeTristrip;
	internal:
		Tristrip(gpc_tristrip* nativeTristrip);
	public:
		~Tristrip();
		!Tristrip();

		virtual GraphicsPath^ ToGraphicsPath(GraphicsPathType type);
	};
}