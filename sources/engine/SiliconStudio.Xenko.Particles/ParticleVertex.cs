﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Graphics;

namespace SiliconStudio.Xenko.Particles
{
    // TODO This is just a copy from the old implementation. It will change when the particle shader is done.
    [StructLayout(LayoutKind.Sequential)]
    public struct ParticleVertex // Identical to VertexPositionColorTextureSwizzle
    {
        public Vector4   Position;
        public Color4    Color;
        public Vector2   TexCoord;
        public float     Swizzle;


        /// <summary>
        /// Defines structure byte size.
        /// </summary>
        public static readonly int Size = 44;

        /// <summary>
        /// The vertex layout of this struct.
        /// </summary>
        public static readonly VertexDeclaration Layout = new VertexDeclaration(
            VertexElement.Position<Vector4>(),
            VertexElement.Color<Color4>(),
            VertexElement.TextureCoordinate<Vector2>(),
            new VertexElement("BATCH_SWIZZLE", PixelFormat.R32_Float)
            );
    }

    /*
    public struct ParticleVertex
    {
        public Vector3   Position;
        public float     Size;
        public Vector2   TexCoord;
        public uint      Color;
    }
    */

}
