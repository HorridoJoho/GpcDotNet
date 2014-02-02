#pragma once

namespace Gpc
{
	public ref class AbstractDisposable abstract
	{
	private:
		bool mIsDisposed;
	protected:
		AbstractDisposable();
		virtual ~AbstractDisposable();
	public:
		void ThrowIfDisposed();
	};
}

