namespace ObjParser
{
    /// <summary>
    /// Channel type for -imfchan command in <see cref="MTLTexture"/>.
    /// </summary>
    public enum ImfchanType
    {
        /// <summary>
        /// Red channel creates a scalar or bump texture.
        /// </summary>
        R = 0,

        /// <summary>
        /// Green channel creates a scalar or bump texture.
        /// </summary>
        G = 1,

        /// <summary>
        /// Blue channel creates a scalar or bump texture.
        /// </summary>
        B = 2,

        /// <summary>
        /// Matte channel creates a scalar or bump texture.
        /// </summary>
        M = 3,

        /// <summary>
        /// Luminance channel creates a scalar or bump texture.
        /// </summary>
        L = 4,

        /// <summary>
        /// Z-Depth channel creates a scalar or bump texture.
        /// </summary>
        Z = 5,
    }
}
