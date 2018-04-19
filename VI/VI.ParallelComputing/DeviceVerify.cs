using System;
using System.Globalization;
using ILGPU;
using ILGPU.Runtime;

namespace VI.ParallelComputing
{
	public static class DeviceVerify
	{
		public static void ListDevices()
		{
			foreach (var ac in Accelerator.Accelerators)
			{
				Console.WriteLine($"Device: {ac.AcceleratorType}");		
			}
		}
	}
}