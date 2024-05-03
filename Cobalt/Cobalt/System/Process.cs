using Cosmos.System.Graphics;
using System.Drawing;

namespace Cobalt.System
{
	public class Process
	{
		public virtual void Run()
		{

		}
		public virtual void Start()
		{

		}
		public string Name;
		public WindowData WindowData = new WindowData();
	}
	public class WindowData
	{
		public Rectangle WinPos = new Rectangle { X = 100, Y = 100, Height = 100, Width = 100 };
		public bool MoveAble = true;
		public Bitmap CachedTop;
		public Bitmap CachedTopDark;
	}
}
