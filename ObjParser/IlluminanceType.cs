namespace ObjParser
{
    /// <summary>
    /// Illumination type specifiers for <see cref="MTLMaterial"/>.
    /// </summary>
    public enum IlluminanceType : int
    {
        /// <summary>
        /// This is a constant color illumination model.  The color is the specified Kd 
        /// for the material.
        /// </summary>
        AmbientOff = 0,

        /// <summary>
        /// This is a diffuse illumination model using Lambertian shading. The color includes 
        /// an ambient constant term and a diffuse shading term for each light source.
        /// </summary>
        AmbientOn = 1,

        /// <summary>
        /// This is a diffuse and specular illumination model using Lambertian shading and 
        /// Blinn's interpretation of Phong's specular illumination model (BLIN77). The color 
        /// includes an ambient constant term, and a diffuse and specular shading term for 
        /// each light source.
        /// </summary>
        HighlightOn = 2,

        /// <summary>
        /// This is a diffuse and specular illumination model with reflection using Lambertian 
        /// shading, Blinn's interpretation of Phong's specular illumination model (BLIN77), and 
        /// a reflection term similar to that in Whitted's illumination model (WHIT80). The color 
        /// includes an ambient constant term and a diffuse and specular shading term for each 
        /// light source.
        /// </summary>
        ReflectionOnRayTraceOn = 3,

        /// <summary>
        /// The diffuse and specular illumination model used to simulate glass is the same 
        /// as illumination model 3.  When using a very low dissolve (approximately 0.1), 
        /// specular highlights from lights or reflections become imperceptible.
        /// </summary>
        TransparencyGlassOnRayTraceOn = 4,

        /// <summary>
        /// This is a diffuse and specular shading models similar to illumination model 3, 
        /// except that reflection due to Fresnel effects is introduced into the equation.
        /// Fresnel reflection results from light striking a diffuse surface at a grazing 
        /// or glancing angle.  When light reflects at a grazing angle, the Ks value 
        /// approaches 1.0 for all color samples.
        /// </summary>
        FresnelOnRayTraceOn = 5,

        /// <summary>
        /// This is a diffuse and specular illumination model similar to that used by Whitted 
        /// (WHIT80) that allows rays to refract through a surface. The amount of refraction 
        /// is based on optical density(Ni).  The intensity of light that refracts is equal 
        /// to 1.0 minus the value of Ks, and the resulting light is filtered by 
        /// Tf(transmission filter) as it passes through the object.
        /// </summary>
        RefractionOnFresnelOffRayTraceOn = 6,

        /// <summary>
        /// This illumination model is similar to illumination model 6, except that reflection 
        /// and transmission due to Fresnel effects has been introduced to the equation. At 
        /// grazing angles, more light is reflected and less light is refracted through the object.
        /// </summary>
        RefractionOnFresnelOnRayTraceOn = 7,

        /// <summary>
        /// This illumination model is similar to illumination model 3 without ray tracing.
        /// </summary>
        ReflectionOnRayTraceOff = 8,

        /// <summary>
        /// This illumination model is similar to illumination model 4 without ray tracing.
        /// </summary>
        TransparencyGlassOnRayTraceOff = 9,

        /// <summary>
        /// This illumination model is used to cast shadows onto an invisible surface. This 
        /// is most useful when compositing computer-generated imagery onto live action, 
        /// since it allows shadows from rendered objects to be composited directly on top 
        /// of video-grabbed images.
        /// </summary>
        CastShadowOnInvisibleSurfaces = 10,
    }
}
