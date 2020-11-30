using System;
using System.Linq;
using System.Collections.Generic;



namespace ObjParser
{
	public sealed class MeshCollection
	{
		public string Name { get; }
		public string Filename { get; }
		public OBJMesh[] Meshes { get; }
		public MTLMaterial[] Materials { get; }

		public MeshCollection(string name, string file, IEnumerable<OBJMesh> meshes, IEnumerable<MTLMaterial> mtls)
		{
			this.Name = name ?? String.Empty;
			this.Filename = file ?? String.Empty;
			this.Meshes = meshes.ToArray();
			this.Materials = mtls.ToArray();
		}
	}
}
