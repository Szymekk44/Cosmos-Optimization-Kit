using System.Collections.Generic;

namespace Cobalt.System
{
	public static class ProcessManager
	{
		public static List<Process> ProcessList = new List<Process>();
		public static void Update()
		{
			foreach (Process process in ProcessList)
			{
				process.Run();
			}
		}
		public static void Start(Process process)
		{
			ProcessList.Add(process);
			process.Start();
		}
		public static void Stop(Process process)
		{
			ProcessList.Remove(process);
		}
	}
}
