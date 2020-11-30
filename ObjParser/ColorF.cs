using System.Runtime.InteropServices;



namespace ObjParser
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ColorF
	{
		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }
		public float A { get; set; }

		public ColorF(float r, float g, float b, float a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"R: {this.R}, G: {this.G}, B: {this.B}, A: {this.A}";
		}
	}
}
