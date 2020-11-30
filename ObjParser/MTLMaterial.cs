using System;



namespace ObjParser
{
    /// <summary>
    /// Stores data of an MTL material. All comments are taken from http://paulbourke.net/dataformats/mtl/,
    /// for more information please visit their website.
    /// </summary>
    public class MTLMaterial
    {
        /// <summary>
        /// Name of the material.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Ambient reflectivity of this material. This is a nullable value, if it does not 
        /// exist in the material, it is <see langword="null"/>.
        /// </summary>
        public ColorF? Ka { get; set; } = null;

        /// <summary>
        /// Diffuse reflectivity of this material. This is a nullable value, if it does not 
        /// exist in the material, it is <see langword="null"/>.
        /// </summary>
        public ColorF? Kd { get; set; } = null;

        /// <summary>
        /// Specular reflectivity of this material. This is a nullable value, if it does not 
        /// exist in the material, it is <see langword="null"/>.
        /// </summary>
        public ColorF? Ks { get; set; } = null;

        /// <summary>
        /// Transmission filter setup. Any light passing through the object is filtered 
        /// by the transmission filter, which only allows the specifiec colors to pass 
        /// through. This is a nullable value, if it does not exist in the material, it is
        /// <see langword="null"/>.
        /// </summary>
        public ColorF? Tf { get; set; } = null;

        /// <summary>
        /// Specifies the dissolve for this material. This is a nullable value, if it does not 
        /// exist in the material, it is <see langword="null"/>.
        /// </summary>
        public float? D { get; set; } = null;

        /// <summary>
        /// Specifies that a dissolve is dependent on the surface orientation relative to the viewer.
        /// This is a nullable value, if it does not exist in the material, it is <see langword="null"/>.
        /// </summary>
        public bool? Halo { get; set; } = null;

        /// <summary>
        /// Specifies the specular exponent for the current material. This defines the focus 
        /// of the specular highlight. This is a nullable value, if it does not exist in the 
        /// material, it is <see langword="null"/>.
        /// </summary>
        public float? Ns { get; set; } = null;

        /// <summary>
        /// Specifies the optical density for the surface.  This is also known as index of 
        /// refraction. Default This is a nullable value, if it does not exist in the 
        /// material, it is <see langword="null"/>.
        /// </summary>
        public float? Ni { get; set; } = null;

        /// <summary>
        /// Specifies the sharpness of the reflections from the local reflection map. 
        /// This is a nullable value, if it does not exist in the material, it is 
        /// <see langword="null"/>.
        /// </summary>
        public float? Sharpness { get; set; } = null;

        /// <summary>
        /// Specifies the illumination model to use in the material. Illumination models are 
        /// mathematical equations that represent various material lighting and shading effects.
        /// This is a nullable value, if it does not exist in the material, it is 
        /// <see langword="null"/>.
        /// </summary>
        public IlluminanceType? Illuminance { get; set; } = null;

        /// <summary>
        /// Turns on anti-aliasing of textures in this material without anti-aliasing all 
        /// textures in the scene. This is a nullable value, if it does not exist in the 
        /// material, it is <see langword="null"/>.
        /// </summary>
        public bool? MapAat { get; set; } = null;

        /// <summary>
        /// Specifies that a color texture file or a color procedural texture file is 
        /// applied to the ambient reflectivity of the material. During rendering, the 
        /// MapKa value is multiplied by the Ka value. If material has no MapKa set,
        /// this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture MapKa { get; set; }

        /// <summary>
        /// Specifies that a color texture file or color procedural texture file is 
        /// linked to the diffuse reflectivity of the material. During rendering, 
        /// the MapKd value is multiplied by the Kd value. If material has no MapKd
        /// set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture MapKd { get; set; }

        /// <summary>
        /// Specifies that a color texture file or color procedural texture file is 
        /// linked to the specular reflectivity of the material. During rendering, 
        /// the MapKs value is multiplied by the Ks value. If material has no MapKs
        /// set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture MapKs { get; set; }

        /// <summary>
        /// Specifies that a scalar texture file or scalar procedural texture file is linked 
        /// to the specular exponent of the material. During rendering, the MapNs value is 
        /// multiplied by the Ns value. If material has no MapNs set, this returns 
        /// <see langword="null"/>.
        /// </summary>
        public MTLTexture MapNs { get; set; }

        /// <summary>
        /// Specifies that a scalar texture file or scalar procedural texture file is 
        /// linked to the dissolve of the material. During rendering, the MapD value is 
        /// multiplied by the D value. If material has no MapD set, this returns 
        /// <see langword="null"/>.
        /// </summary>
        public MTLTexture MapD { get; set; }

        /// <summary>
        /// Specifies that a scalar texture file or a scalar procedural texture file 
        /// is used to selectively replace the material color with the texture color.
        /// If material has no Decal set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture Decal { get; set; }

        /// <summary>
        /// Specifies that a scalar texture is used to deform the surface of an object, 
        /// creating surface roughness. If material has no Disp set, this returns 
        /// <see langword="null"/>.
        /// </summary>
        public MTLTexture Disp { get; set; }

        /// <summary>
        /// Specifies that a bump texture file or a bump procedural texture file is linked 
        /// to the material. If material has no Bump set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture MapBump { get; set; }

        /// <summary>
        /// Specifies an infinitely large sphere that casts reflections onto the material from all
        /// sides. If material has no "refl -type sphere" set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture ReflSphere { get; set; }

        /// <summary>
        /// Specifies an infinitely large sphere that casts reflections onto the material from top
        /// side. If material has no "refl -type cube_top" set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture ReflCubeTop { get; set; }

        /// <summary>
        /// Specifies an infinitely large sphere that casts reflections onto the material from bottom
        /// side. If material has no "refl -type cube_bottom" set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture ReflCubeBottom { get; set; }

        /// <summary>
        /// Specifies an infinitely large sphere that casts reflections onto the material from front
        /// side. If material has no "refl -type cube_front" set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture ReflCubeFront { get; set; }

        /// <summary>
        /// Specifies an infinitely large sphere that casts reflections onto the material from back
        /// side. If material has no "refl -type cube_back" set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture ReflCubeBack { get; set; }

        /// <summary>
        /// Specifies an infinitely large sphere that casts reflections onto the material from left
        /// side. If material has no "refl -type cube_left" set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture ReflCubeLeft { get; set; }

        /// <summary>
        /// Specifies an infinitely large sphere that casts reflections onto the material from right
        /// side. If material has no "refl -type cube_right" set, this returns <see langword="null"/>.
        /// </summary>
        public MTLTexture ReflCubeRight { get; set; }

        /// <summary>
        /// Initializes
        /// </summary>
        /// <param name="name"></param>
        public MTLMaterial(string name) => this.Name = name ?? String.Empty;

        /// <inheritdoc/>
		public override string ToString() => String.IsNullOrEmpty(this.Name) ? "No Name" : this.Name;
	}
}
