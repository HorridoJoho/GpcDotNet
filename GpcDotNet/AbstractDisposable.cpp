#include "AbstractDisposable.h"

using System::ObjectDisposedException;

namespace Gpc
{
	AbstractDisposable::AbstractDisposable()
		: mIsDisposed(false)
	{}

	AbstractDisposable::~AbstractDisposable()
	{
		mIsDisposed = true;
	}

	void AbstractDisposable::ThrowIfDisposed()
	{
		if (mIsDisposed)
		{
			throw gcnew ObjectDisposedException(GetType()->Name);
		}
	}
}
