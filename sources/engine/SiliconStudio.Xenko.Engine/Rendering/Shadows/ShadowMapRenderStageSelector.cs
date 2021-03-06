﻿// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System.ComponentModel;
using SiliconStudio.Xenko.Engine;

namespace SiliconStudio.Xenko.Rendering.Shadows
{
    public class ShadowMapRenderStageSelector : RenderStageSelector
    {
        public RenderStage ShadowMapRenderStage { get; set; }
        public string EffectName { get; set; }

        [DefaultValue(RenderGroupMask.All)]
        public RenderGroupMask RenderGroup { get; set; } = RenderGroupMask.All;

        public override void Process(RenderObject renderObject)
        {
            if (ShadowMapRenderStage != null && ((RenderGroupMask)(1U << (int)renderObject.RenderGroup) & RenderGroup) != 0)
            {
                var renderMesh = (RenderMesh)renderObject;

                // Only handle non-transparent meshes
                //if (!renderMesh.MaterialPass.HasTransparency)
                {
                    if (renderMesh.IsShadowCaster)
                        renderMesh.ActiveRenderStages[ShadowMapRenderStage.Index] = new ActiveRenderStage(EffectName);
                }
            }
        }
    }
}
