using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Linear.Vectors;



namespace ObjParser
{
	public class ObjLoader
	{
		private string _name;
		private string _filename;

		public ObjLoader()
		{
		}

		private void LoadMTLLib(string filename, Dictionary<string, MTLMaterial> map)
		{
			string path = null;

			if (this._filename == null) path = filename;
			else path = Path.Combine(Path.GetDirectoryName(this._filename), filename);

			var loader = new MTLLoader();
			var mats = loader.Load(path);

			if (mats != null)
			{

				foreach (var mtl in mats) map[mtl.Name] = mtl;

			}
		}

		private void LoadMTLLib(Stream stream, Dictionary<string, MTLMaterial> map)
		{
			var loader = new MTLLoader();
			var mats = loader.Load(stream);

			if (mats != null)
			{

				foreach (var mtl in mats) map[mtl.Name] = mtl;

			}
		}

		public MeshCollection Load(Stream objStream, Stream mtlStream = null)
		{
			var reader = new LineReader(objStream);

			var vertices = new List<Vector3>();
			var normals = new List<Vector3>();
			var uvs = new List<Vector2>();
			
			var meshNameToMesh = new Dictionary<string, OBJMesh>();
			var matNameToMat = new Dictionary<string, MTLMaterial>();

			OBJMesh defaultMesh = new OBJMesh(String.Empty);
			OBJMesh currentMesh = null;
			int currentSmoothing = -1;
			
			MTLMaterial currentMaterial = new MTLMaterial(String.Empty)
			{
				Kd = new ColorF(0.8f, 0.8f, 0.8f, 1.0f),
				D = 1.0f,
				Illuminance = IlluminanceType.HighlightOn
			};
			matNameToMat[String.Empty] = currentMaterial;

			if (!(mtlStream is null)) this.LoadMTLLib(mtlStream, matNameToMat);

			while (reader.ReadNext())
			{

				if (String.IsNullOrWhiteSpace(reader.Current) || reader.Current.StartsWith("#")) continue;
				if (reader.Splits.Length < 2) continue; // if less then 2 splits then invalid line

				var arg = reader.ReadString().ToLowerInvariant();

				switch (arg)
				{

					case "mtllib": {
						var path = reader.ReadString();
						this.LoadMTLLib(path, matNameToMat);
						continue;
					}

					case "v": {
						vertices.Add(reader.ReadVector3());
						continue;
					}

					case "vn": {
						normals.Add(reader.ReadVector3());
						continue;
					}
					
					case "vt": {
						uvs.Add(reader.ReadVector2());
						continue;
					}

					case "o":
					case "g": { 
						var name = reader.ReadString();
						if (!meshNameToMesh.TryGetValue(name, out currentMesh))
						{

							currentMesh = new OBJMesh(name);
							meshNameToMesh[name] = currentMesh;

						}
						continue;
					}

					case "usemtl": { 
						var mtl = reader.ReadString();
						if (!matNameToMat.TryGetValue(mtl, out currentMaterial))
						{

							currentMaterial = matNameToMat[String.Empty];
						
						}
						continue;
					}

					case "f": {
						if (currentMesh is null) currentMesh = defaultMesh; 
						currentMesh.ReadFace(reader, currentSmoothing, currentMaterial, vertices, normals, uvs);
						continue;
					}

					case "s": {
						currentSmoothing = reader.ReadInt32();
						continue;
					}

					default:
						continue;

				}

			}

			// If at least one submesh exists in default mesh
			if (defaultMesh.SubMeshes.Count > 0) meshNameToMesh[String.Empty] = defaultMesh;

			return new MeshCollection(this._name, this._filename, meshNameToMesh.Values, matNameToMat.Values);
		}

		public MeshCollection Load(string objFilename, string mtlFilename = null)
		{
			if (!File.Exists(objFilename)) throw new FileNotFoundException($"File {objFilename} does not exist");

			this._filename = objFilename;
			this._name = Path.GetFileNameWithoutExtension(objFilename);

			using (var osr = File.Open(objFilename, FileMode.Open, FileAccess.Read))
			{

				if (!File.Exists(mtlFilename))
				{

					return this.Load(osr);
				
				}
				else
				{

					using (var msr = File.Open(mtlFilename, FileMode.Open, FileAccess.Read))
					{

						return this.Load(osr, msr);

					}

				}

			}
		}
	}
}
