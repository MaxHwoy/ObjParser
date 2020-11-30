using System;



namespace ObjParser
{
	public static class Utils
	{
		public static int StringToInt32(string value)
		{
			if (String.IsNullOrEmpty(value)) return 0;

			bool isNegative = (value[0] == '-');
			int result = 0;

			for (int i = (isNegative) ? 1 : 0; i < value.Length; i++)
			{

				char c = value[i];
				if (c < '0' || c > '9') throw new Exception("Unexpected character met when parsing string to System.Int32");
				if ((uint)result >= 0x7FFFFF6) throw new OverflowException("System.Int32 overflow when parsing");
				result *= 10;
				result += c - '0';

			}

			return (isNegative) ? -result : result;
		}
	}
}
