#region usings
using System;
using System.ComponentModel.Composition;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;
#endregion usings

namespace VVVV.Nodes
{
	
	#region PluginInfo
	[PluginInfo(Name = "IsPrime", Category = "Boolean", Version = "v1", Help = "Basic template with one boolean in/out", Tags = "c#")]
	#endregion PluginInfo
	public class v1BooleanIsPrimeNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("Input")]
		public ISpread<int> FInput;

		[Output("bool")]
		public ISpread<bool> FOutput;

		[Import()]
		public ILogger FLogger;
		#endregion fields & pins
		
		public static bool IsPrime(int number)
		{
			if (number == 1) return true;
			if (number == 2) return true;
			if (number % 2 == 0)  return false;
			
			var boundary = (int)Math.Floor(Math.Sqrt(number));
			
			for (int i = 3; i <= boundary; i+=2)
    		{
    		    if (number % i == 0)  return false;
    		}
			
			return true;
		}

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{
			FOutput.SliceCount = SpreadMax;

			for (int i = 0; i < SpreadMax; i++) {
				FOutput[i] = IsPrime(FInput[i]);
			}
			//FLogger.Log(LogType.Debug, "hi TTY");
		}
	}
}
