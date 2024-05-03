using Cobalt.Graphics;
using Cosmos.System.Graphics;
using Cobalt.Cobalt;
using Cobalt.TTF;
using CosmosTTF;

namespace Cobalt.System
{
	public static class Boot
	{
		public static void onBoot()
		{
			GUI.Wallpaper = new Bitmap(Resources.CobaltBackgroundRaw);
			GUI.Cursor = new Bitmap(Resources.RadianceOSCursorRaw);
			CosmosTTF.TTFManager.RegisterFont("KMB", Resources.KodeMonoBold);
			GUI.StartGUI();
			TTFCache.CacheAllFonts();
		}
	}
}
