using System;

namespace Targil6
{
	[Serializable]
	public class AEdge
	{
		public ANode from;
		public ANode to;

		public AEdge(ANode f, ANode t)
		{
			this.from = f;
			this.to = t;
		}
	}
}