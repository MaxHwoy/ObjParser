using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



namespace ObjParser
{
    public class MTLLoader
    {
        private void ParseMTLReflection(MTLMaterial material, LineReader lr)
		{
            var type = lr.ReadString().ToLowerInvariant();

            switch (type)
			{

                case "sphere":
                    material.ReflSphere = this.ParseMTLTexture(lr);
                    break;

                case "cube_top":
                    material.ReflCubeTop = this.ParseMTLTexture(lr);
                    break;

                case "cube_bottom":
                    material.ReflCubeBottom = this.ParseMTLTexture(lr);
                    break;

                case "cube_front":
                    material.ReflCubeFront = this.ParseMTLTexture(lr);
                    break;

                case "cube_back":
                    material.ReflCubeBack = this.ParseMTLTexture(lr);
                    break;

                case "cube_left":
                    material.ReflCubeLeft = this.ParseMTLTexture(lr);
                    break;

                case "cube_right":
                    material.ReflCubeRight = this.ParseMTLTexture(lr);
                    break;

                default:
                    break;

			}
		}

        private MTLTexture ParseMTLTexture(LineReader lr)
		{
            var mtltex = new MTLTexture();
            const string on = "on";

            while (!lr.IsOutOfBounds)
			{

                var value = lr.ReadString();
                var arg = value.ToLowerInvariant();

                switch (arg)
				{

                    case "-blendu":
                        mtltex.BlendU = lr.ReadString() == on;
                        break;

                    case "-blendv":
                        mtltex.BlendV = lr.ReadString() == on;
                        break;

                    case "-bm":
                        mtltex.Bm = lr.ReadSingle();
                        break;

                    case "-boost":
                        mtltex.Boost = lr.ReadSingle();
                        break;

                    case "-clamp":
                        mtltex.Clamp = lr.ReadString() == on;
                        break;

                    case "-imfchan":
                        var imfch = lr.ReadString().ToUpperInvariant();
                        if (!Enum.TryParse(imfch, out ImfchanType imfchan)) mtltex.Imfchan = imfchan;
                        break;

                    case "-mm":
                        mtltex.MMBase = lr.ReadSingle();
                        mtltex.MMGain = lr.ReadSingle();
                        break;

                    case "-o":
                        mtltex.O = lr.ReadVector3();
                        break;

                    case "-s":
                        mtltex.S = lr.ReadVector3();
                        break;

                    case "-t":
                        mtltex.T = lr.ReadVector3();
                        break;

                    case "-texres":
                        mtltex.TexRes = lr.ReadInt32();
                        break;

                    default:
                        mtltex.Filename = value;
                        break;

				}

			}

            return mtltex;
		}

        private ColorF ReadColorF(LineReader lr)
		{
            var r = lr.IsOutOfBounds ? 0.0f : lr.ReadSingle();
            var g = lr.IsOutOfBounds ? r : lr.ReadSingle();
            var b = lr.IsOutOfBounds ? r : lr.ReadSingle();
            return new ColorF(r, g, b, 1.0f);
		}

        private IEnumerable<MTLMaterial> LoadPrivate(Stream stream)
        {
            var reader = new LineReader(stream);

            MTLMaterial currentMTL = null;

            while (reader.ReadNext())
			{

                if (String.IsNullOrWhiteSpace(reader.Current) || reader.Current.StartsWith("#")) continue;
                if (reader.Splits.Length < 2) continue; // if less then 2 splits then invalid line

                var argument = reader.ReadString().ToLowerInvariant();

                if (argument == "newmtl")
				{

                    var name = reader.ReadString();
                    currentMTL = new MTLMaterial(name);
                    yield return currentMTL;
                    continue;

				}

                if (currentMTL is null) continue; // if no material yet set

                switch (argument)
				{

                    case "kd": // diffuse color
                        currentMTL.Kd = this.ReadColorF(reader);
                        continue;

                    case "ks": // specular color
                        currentMTL.Ks = this.ReadColorF(reader);
                        continue;

                    case "ka": // ambient color
                        currentMTL.Ka = this.ReadColorF(reader);
                        continue;

                    case "tf": // transmission filter
                        currentMTL.Tf = this.ReadColorF(reader);
                        continue;

                    case "d":
                        var inter = reader.Splits[1];
                        if (inter.ToLowerInvariant() != "-halo") { currentMTL.D = reader.ReadSingle(); continue; }
                        reader.ReadString();
                        currentMTL.Halo = true;
                        currentMTL.D = reader.ReadSingle();
                        continue;

                    case "tr":
                        currentMTL.D = 1.0f - reader.ReadSingle();
                        continue;

                    case "ns":
                        currentMTL.Ns = reader.ReadSingle();
                        continue;

                    case "sharpness":
                        currentMTL.Sharpness = reader.ReadSingle();
                        continue;

                    case "ni":
                        currentMTL.Ni = reader.ReadSingle();
                        continue;

                    case "illum":
                        currentMTL.Illuminance = (IlluminanceType)reader.ReadInt32();
                        continue;

                    case "map_ka":
                        currentMTL.MapKa = this.ParseMTLTexture(reader);
                        continue;

                    case "map_kd":
                        currentMTL.MapKd = this.ParseMTLTexture(reader);
                        continue;

                    case "map_ks":
                        currentMTL.MapKs = this.ParseMTLTexture(reader);
                        continue;

                    case "map_ns":
                        currentMTL.MapNs = this.ParseMTLTexture(reader);
                        continue;

                    case "map_d":
                        currentMTL.MapD = this.ParseMTLTexture(reader);
                        continue;

                    case "decal":
                        currentMTL.Decal = this.ParseMTLTexture(reader);
                        continue;

                    case "map_aat":
                        currentMTL.MapAat = reader.ReadString().ToLowerInvariant() == "on";
                        continue;

                    case "disp":
                        currentMTL.Disp = this.ParseMTLTexture(reader);
                        continue;

                    case "bump":
                    case "map_bump":
                        currentMTL.MapBump = this.ParseMTLTexture(reader);
                        continue;

                    case "refl":
                        this.ParseMTLReflection(currentMTL, reader);
                        continue;

                    default:
                        continue;

				}

			}
        }

        public MTLMaterial[] Load(Stream stream)
		{
            return this.LoadPrivate(stream).ToArray();
		}

        public MTLMaterial[] Load(string file)
        {
            if (!File.Exists(file)) return null;

            using (var fs = File.Open(file, FileMode.Open, FileAccess.Read))
            {

                return this.Load(fs);

            }
        }
    }
}
