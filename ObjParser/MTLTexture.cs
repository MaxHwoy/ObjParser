using System;
using Linear.Vectors;



namespace ObjParser
{
    /// <summary>
    /// Stores data and optional arguments for a texture linked to an <see cref="MTLMaterial"/>.
    /// All comments are taken from http://paulbourke.net/dataformats/mtl/, for more information
    /// please visit their website.
    /// </summary>
    public class MTLTexture
    {
        /// <summary>
        /// The -blendu option turns texture blending in the horizontal direction 
        /// (u direction) on or off. This is a nullable value, if it does not exist 
        /// in the texture arguments, it is <see langword="null"/>.
        /// </summary>
        public bool? BlendU { get; set; } = null;

        /// <summary>
        /// The -blendv option turns texture blending in the vertical direction 
        /// (v direction) on or off. This is a nullable value, if it does not exist 
        /// in the texture arguments, it is <see langword="null"/>.
        /// </summary>
        public bool? BlendV { get; set; } = null;

        /// <summary>
        /// The -bm option specifies a bump multiplier. It can only be used with the 
        /// "bump" statement. This is a nullable value, if it does not exist 
        /// in the texture arguments, it is <see langword="null"/>.
        /// </summary>
        public float? Bm { get; set; } = null;

        /// <summary>
        /// The -boost option increases the sharpness, or clarity, of mip-mapped texture 
        /// files - that is, color(.mpc), scalar(.mps), and bump(.mpb) files. This is a
        /// nullable value, if it does not exist in the texture arguments, it is
        /// <see langword="null"/>.
        /// </summary>
        public float? Boost { get; set; } = null;

        /// <summary>
        /// The -cc option turns on color correction for the texture. It can only be used 
        /// with the color map statements:  map_Ka, map_Kd, and map_Ks. This is a
        /// nullable value, if it does not exist in the texture arguments, it is
        /// <see langword="null"/>.
        /// </summary>
        public bool? CC { get; set; } = null;

        /// <summary>
        /// The -clamp option turns clamping on or off.  When clamping is on, textures are 
        /// restricted to 0-1 in the uvw range.The default is off. This is a nullable value, 
        /// if it does not exist in the texture arguments, it is <see langword="null"/>.
        /// </summary>
        public bool? Clamp { get; set; } = null;

        /// <summary>
        /// The -imfchan option specifies the channel used to create a scalar or bump texture.
        /// Scalar textures are applied to: transparency, specular exponent, decal, 
        /// displacement. This is a nullable value, if it does not exist in the texture 
        /// arguments, it is <see langword="null"/>.
        /// </summary>
        public ImfchanType? Imfchan { get; set; } = null;

        /// <summary>
        /// The -mm option modifies the range over which scalar or color texture values 
        /// may vary. "base" adds a base value to the texture values. A positive value 
        /// makes everything brighter; a negative value makes everything dimmer. This is 
        /// a nullable value, if it does not exist in the texture arguments, it is 
        /// <see langword="null"/>.
        /// </summary>
        public float? MMBase { get; set; } = null;

        /// <summary>
        /// The -mm option modifies the range over which scalar or color texture values 
        /// may vary. "gain" expands the range of the texture values.  Increasing the number 
        /// increases the contrast. This is a nullable value, if it does not exist in the 
        /// texture arguments, it is <see langword="null"/>.
        /// </summary>
        public float? MMGain { get; set; } = null;

        /// <summary>
        /// The -o option offsets the position of the texture map on the surface by shifting 
        /// the position of the map origin. This is a nullable value, if it does not exist 
        /// in the texture arguments, it is <see langword="null"/>.
        /// </summary>
        public Vector3? O { get; set; } = null;

        /// <summary>
        /// The -s option scales the size of the texture pattern on the textured surface by 
        /// expanding or shrinking the pattern. This is a nullable value, if it does not 
        /// exist in the texture arguments, it is <see langword="null"/>.
        /// </summary>
        public Vector3? S { get; set; } = null;

        /// <summary>
        /// The -t option turns on turbulence for textures.  Adding turbulence to a texture 
        /// along a specified direction adds variance to the original image and allows a 
        /// simple image to be repeated over a larger area without noticeable tiling effects.
        /// This is a nullable value, if it does not exist in the texture arguments, it is 
        /// <see langword="null"/>.
        /// </summary>
        public Vector3? T { get; set; } = null;

        /// <summary>
        /// The -texres option specifies the resolution of texture created when an image is 
        /// used. This is a nullable value, if it does not exist in the texture arguments, 
        /// it is <see langword="null"/>.
        /// </summary>
        public int? TexRes { get; set; } = null;

        /// <summary>
        /// Filename of the texture linked relative to this MTL file.
        /// </summary>
        public string Filename { get; set; } = String.Empty;

        /// <inheritdoc/>
		public override string ToString() => String.IsNullOrEmpty(this.Filename) ? "No Texture" : this.Filename;
	}
}
