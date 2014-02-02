using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gpc
{
	public enum ClipOp
	{
		Diff,
		Int,
		Xor,
		Union
	}

	public enum GraphicsPathType
	{
		Polygons,
		Lines
	}
    
    public enum ContourType
	{
		Filled,
		Hollow,
		All
	};
}
