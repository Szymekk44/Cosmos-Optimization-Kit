using Cobalt.System;
using Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System.Graphics.Fonts;
using Cobalt.Apps;
using Cobalt.Cobalt.Apps;

namespace Cobalt.Graphics
{
	public static class GUI
	{
		public static int ScreenSizeX = 1920, ScreenSizeY = 1080;
		public static SVGAIICanvas MainCanvas;
		public static Bitmap Wallpaper, Cursor;
		public static Colors colors = new Colors();
		public static bool Clicked;
		public static Process currentProcess;
		public static int MX, MY;
		static int oldX, oldY;
		public static PCScreenFont FontDefault = PCScreenFont.Default;
		public static void Update()
		{
			MX = (int)MouseManager.X;
			MY = (int)MouseManager.Y;
			MainCanvas.DrawImage(Wallpaper, 0, 0);
			Move();
			ProcessManager.Update();
			MainCanvas.DrawImageAlpha(Cursor, (int)MouseManager.X, (int)MouseManager.Y);
			if (MouseManager.MouseState == MouseState.Left)
				Clicked = true;
			else if (MouseManager.MouseState == MouseState.None && Clicked)
			{
				Clicked = false;
				currentProcess = null;
			}
			MainCanvas.Display();
		}
		public static void Move()
		{
			if (currentProcess != null)
			{
				currentProcess.WindowData.WinPos.X = (int)MouseManager.X - oldX;
				currentProcess.WindowData.WinPos.Y = (int)MouseManager.Y - oldY;
			}
			else if (MouseManager.MouseState == MouseState.Left && !Clicked)
			{
				foreach (var proc in ProcessManager.ProcessList)
				{
					if (!proc.WindowData.MoveAble)
						continue;
					if (MX > proc.WindowData.WinPos.X && MX < proc.WindowData.WinPos.X + proc.WindowData.WinPos.Width)
					{
						if (MY > proc.WindowData.WinPos.Y && MY < proc.WindowData.WinPos.Y + Window.TopSize)
						{
							currentProcess = proc;
							oldX = MX - proc.WindowData.WinPos.X;
							oldY = MY - proc.WindowData.WinPos.Y;
						}
					}
				}
			}
		}
		public static void StartGUI()
		{
			MainCanvas = new SVGAIICanvas(new Mode((uint)ScreenSizeX, (uint)ScreenSizeY, ColorDepth.ColorDepth32));
			MouseManager.ScreenWidth = (uint)ScreenSizeX;
			MouseManager.ScreenHeight = (uint)ScreenSizeY;
			MouseManager.X = (uint)ScreenSizeX / 2;
			MouseManager.Y = (uint)ScreenSizeY / 2;
			ProcessManager.Start(new TTFBox { WindowData = new WindowData { WinPos = new Rectangle(100, 100, 675, 175) }, Name = "Welcome to Cobalt!" });
			ProcessManager.Start(new AlphaImageRenderer { WindowData = new WindowData { WinPos = new Rectangle(120, 300, 475, 400) }, Name = "Alpha Image Viewer" });
			ProcessManager.Start(new ResizedImageRenderer { WindowData = new WindowData { WinPos = new Rectangle(150, 700, 350, 235) }, Name = "Resized Image Viewer" });
			ProcessManager.Start(new OptimizationTest { WindowData = new WindowData { WinPos = new Rectangle(GUI.ScreenSizeX-170, GUI.ScreenSizeY-70, 160, 60) }, Name = "Optimization Test" });
			ProcessManager.Start(new FPSCunter { WindowData = new WindowData { WinPos = new Rectangle(10, 10, 120, 60) }, Name = "FPS Counter" });
		}
	}
}
