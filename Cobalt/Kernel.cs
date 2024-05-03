using Cobalt.Cobalt;
using Cobalt.Graphics;
using Cobalt.System;
using Sys = Cosmos.System;

namespace Cobalt
{
	public class Kernel : Sys.Kernel
	{
		protected override void BeforeRun()
		{
			Boot.onBoot();
		}

		protected override void Run()
		{
			GUI.Update();
			CobaltCore.CallCore();
		}
	}
}
