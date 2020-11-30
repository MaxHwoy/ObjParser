using System;
using System.IO;
using Linear.Vectors;



namespace ObjParser
{
	public class LineReader
	{
		private static string[] empty = new string[0];
		private StreamReader _sr;
		private bool _disposed;

		private int _currentLine;
		private string _current;

		private int _splitPos;
		private string[] _splits;

		public bool IsOutOfBounds => this._splitPos >= this._splits.Length;

		public string Current => this._current;

		public string[] Splits => this._splits;

		public LineReader(Stream stream)
		{
			this._current = String.Empty;
			this._splits = empty;
			this._sr = new StreamReader(stream);
		}

		public LineReader(StreamReader sr)
		{
			this._current = String.Empty;
			this._splits = empty;
			this._sr = sr;
		}

		public bool ReadNext()
		{
			if (this._sr.EndOfStream) return false;
			this._current = this._sr.ReadLine();
			this.SplitCurrentByWhitespace();
			++this._currentLine;
			this._splitPos = 0;
			return true;
		}

		private void SplitCurrentByWhitespace()
		{
			if (String.IsNullOrWhiteSpace(this._current)) this._splits = empty;
			else this._splits = this._current?.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
		}

		public string ReadString()
		{
			if (this.IsOutOfBounds) return null;
			return this._splits[this._splitPos++];
		}

		public int ReadInt32()
		{
			if (this.IsOutOfBounds) return 0;
			if (Int32.TryParse(this._splits[this._splitPos++], out int result)) return result;
			else return 0;
		}

		public uint ReadUInt32()
		{
			if (this.IsOutOfBounds) return 0u;
			if (UInt32.TryParse(this._splits[this._splitPos++], out uint result)) return result;
			else return 0u;
		}

		public float ReadSingle()
		{
			if (this.IsOutOfBounds) return 0.0f;
			if (Single.TryParse(this._splits[this._splitPos++], out float result)) return result;
			else return 0.0f;
		}

		public double ReadDouble()
		{
			if (this.IsOutOfBounds) return 0.0d;
			if (Double.TryParse(this._splits[this._splitPos++], out double result)) return result;
			else return 0.0d;
		}

		public Vector3 ReadVector3()
		{
			var x = this.ReadSingle();
			var y = this.ReadSingle();
			var z = this.ReadSingle();
			return new Vector3(x, y, z);
		}

		public Vector2 ReadVector2()
		{
			var x = this.ReadSingle();
			var y = this.ReadSingle();
			return new Vector2(x, y);
		}
	}
}
