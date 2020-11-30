using System;
using System.Collections.Generic;
using Linear.Vectors;




namespace ObjParser
{
	public class OBJMesh
	{
		public struct Triangle
		{
			public int Index1 { get; }
			public int Index2 { get; }
			public int Index3 { get; }

			public Triangle(int i1, int i2, int i3)
			{
				this.Index1 = i1;
				this.Index2 = i2;
				this.Index3 = i3;
			}

			public override bool Equals(object obj)
			{
				if (obj is Triangle face)
				{

					return this.Index1 == face.Index1 && this.Index2 == face.Index2 && this.Index3 == face.Index3;
				
				}

				return false;
			}
			public override int GetHashCode() => Tuple.Create(this.Index1, this.Index2, this.Index3).GetHashCode();
			public override string ToString() => $"({this.Index1}, {this.Index2}, {this.Index3})";
		}

		public struct MeshVertex
		{
			public int VertexIndex { get; set; }
			public int UVIndex { get; set; }
			public int NormalIndex { get; set; }
			public int SmoothingGroup { get; set; }

			public MeshVertex(int smoothing, int vertex, int uv = 0, int normal = 0)
			{
				this.VertexIndex = vertex;
				this.UVIndex = uv;
				this.NormalIndex = normal;
				this.SmoothingGroup = smoothing;
			}

			public override bool Equals(object obj)
			{
				if (obj is MeshVertex meshVertex)
				{

					return this.VertexIndex == meshVertex.VertexIndex &&
						   this.UVIndex == meshVertex.UVIndex &&
						   this.NormalIndex == meshVertex.NormalIndex &&
						   this.SmoothingGroup == meshVertex.SmoothingGroup;

				}

				return false;
			}
			public override int GetHashCode()
			{
				return Tuple.Create(this.VertexIndex, this.UVIndex, this.NormalIndex, this.SmoothingGroup).GetHashCode();
			}
			public override string ToString()
			{
				if (this.UVIndex != 0)
				{

					if (this.NormalIndex != 0)
					{

						return $"{this.VertexIndex}/{this.UVIndex}/{this.NormalIndex}";

					}

					return $"{this.VertexIndex}/{this.UVIndex}";

				}

				return this.VertexIndex.ToString();
			}
		}

		public class SubMesh
		{
			public OBJMesh ParentMesh { get; }
			public MTLMaterial Material { get; }
			public List<Triangle> Triangles { get; }
			public bool HasMissingNormals { get; private set; }
			
			public SubMesh(MTLMaterial material, OBJMesh parent)
			{
				this.ParentMesh = parent;
				this.Material = material;
				this.Triangles = new List<Triangle>();
			}
			public void EnableMissingNormals() => this.HasMissingNormals = true;
			public override string ToString() => this.Material.Name;
		}

		public string Name { get; }
		public List<Vector3> Vertices { get; }
		public List<Vector3> Normals { get; }
		public List<Vector2> UVs { get; }
		public List<int> SmoothingGroups { get; }
		public Dictionary<string, SubMesh> SubMeshes { get; }
		public int NumTriangles
		{
			get
			{
				int result = 0;
				foreach (var subMesh in this.SubMeshes.Values) result += subMesh.Triangles.Count;
				return result;
			}
		}
		private readonly Dictionary<MeshVertex, int> _meshVertexToIndex;

		public OBJMesh(string name)
		{
			this.Name = name ?? String.Empty;
			this.Vertices = new List<Vector3>();
			this.Normals = new List<Vector3>();
			this.UVs = new List<Vector2>();
			this.SmoothingGroups = new List<int>();
			this.SubMeshes = new Dictionary<string, SubMesh>();
			this._meshVertexToIndex = new Dictionary<MeshVertex, int>();
		}

		public override string ToString() => String.IsNullOrEmpty(this.Name) ? "No Name" : this.Name;

		public void ReadFace
		(
			LineReader lr,
			int smoothing,
			MTLMaterial material,
			List<Vector3> vertices,
			List<Vector3> normals,
			List<Vector2> uvs
		)
		{
			// Use stackalloc for performance increase and GC override
			unsafe
			{

				var count = lr.Splits.Length - 1;
				if (count < 3) return; // well, what can we do if it is not a triangle?
				var faceIndeces = stackalloc int[count];

				if (!this.SubMeshes.TryGetValue(material.Name, out var subMesh))
				{

					subMesh = new SubMesh(material, this);
					this.SubMeshes.Add(material.Name, subMesh);

				}

				for (int i = 0; i < count; ++i)
				{

					var combo = lr.ReadString().Split('/');
					var meshVertex = new MeshVertex();

					switch (combo.Length)
					{

						case 1:
							meshVertex.VertexIndex = Utils.StringToInt32(combo[0]);
							meshVertex.SmoothingGroup = smoothing;
							break;

						case 2:
							meshVertex.UVIndex = Utils.StringToInt32(combo[1]);
							goto case 1;

						case 3:
							meshVertex.NormalIndex = Utils.StringToInt32(combo[2]);
							goto case 2;

						default:
							break;

					}

					if (!this._meshVertexToIndex.TryGetValue(meshVertex, out int index))
					{

						index = this._meshVertexToIndex.Count;
						this._meshVertexToIndex.Add(meshVertex, index);

						var vertexIndex = meshVertex.VertexIndex;
						var normalIndex = meshVertex.NormalIndex;
						var uvIndex = meshVertex.UVIndex;

						if (vertexIndex <= 0 || vertexIndex > vertices.Count) this.Vertices.Add(Vector3.Zero);
						else this.Vertices.Add(vertices[vertexIndex - 1]);

						if (uvIndex <= 0 || uvIndex > uvs.Count) this.UVs.Add(Vector2.Zero);
						else this.UVs.Add(uvs[uvIndex - 1]);

						if (0 < normalIndex && normalIndex <= normals.Count) this.Normals.Add(normals[normalIndex - 1]);
						else { this.Normals.Add(Vector3.Zero); subMesh.EnableMissingNormals(); }

						this.SmoothingGroups.Add(smoothing);

					}

					faceIndeces[i] = index;

				}

				var list = subMesh.Triangles;

				switch (count)
				{

					case 3:
						list.Add(new Triangle(faceIndeces[0], faceIndeces[1], faceIndeces[2]));
						break;

					case 4:
						list.Add(new Triangle(faceIndeces[0], faceIndeces[1], faceIndeces[2]));
						list.Add(new Triangle(faceIndeces[2], faceIndeces[3], faceIndeces[0]));
						break;

					default: // 5 and more
						for (int i = count - 2; i > 0; --i)
						{
							
							list.Add(new Triangle(faceIndeces[0], faceIndeces[i], faceIndeces[i + 1]));
						
						}
						break;

				}
			}
		}
	}
}
