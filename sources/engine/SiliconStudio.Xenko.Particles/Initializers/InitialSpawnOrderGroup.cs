// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.

using System;
using SiliconStudio.Core;
using SiliconStudio.Xenko.Particles;
using SiliconStudio.Xenko.Particles.Initializers;

namespace SiliconStudio.Xenko.Particles.Initializers
{
    /// <summary>
    /// The <see cref="InitialSpawnOrderGroup"/> is an initializer which assigns all particles an increasing number based on the order of their spawning while keeping all particles spawned in the same frame in a separate spawn group (this is important for ribbons)
    /// </summary>
    [DataContract("InitialSpawnOrderGroup")]
    [Display("Spawn Order (Group)")]
    public class InitialSpawnOrderGroup : ParticleInitializer
    {
        // Will loop every so often, but the loop condition should be unreachable for normal games (~800 hours for spawning rate of 100 particles/second)
        private uint spawnOrder = 0;

        /// <inheritdoc />
        public override void ResetSimulation()
        {
            spawnOrder = 0;
        }

        /// <summary>
        /// Default constructor which also registers the fields required by this updater
        /// </summary>
        public InitialSpawnOrderGroup()
        {
            spawnOrder = 0;

            RequiredFields.Add(ParticleFields.Order);
        }

        /// <inheritdoc />
        public unsafe override void Initialize(ParticlePool pool, int startIdx, int endIdx, int maxCapacity)
        {
            if (!pool.FieldExists(ParticleFields.Order))
                return;

            var orderField = pool.GetField(ParticleFields.Order);
            var childOrderField = pool.GetField(ParticleFields.ChildOrder);

            var i = startIdx;
            while (i != endIdx)
            {
                var particle = pool.FromIndex(i);

                (*((uint*)particle[orderField])) = spawnOrder++; // Will loop every so often, but the loop condition should be unreachable for normal games

                if (childOrderField.IsValid())
                    (*((uint*)particle[childOrderField])) = 0;

                i = (i + 1) % maxCapacity;
            }

            // Increase the group by one
            spawnOrder = (spawnOrder >> SpawnOrderConst.GroupBitOffset);
            spawnOrder++;
            spawnOrder = (spawnOrder << SpawnOrderConst.GroupBitOffset);
        }
    }
}
