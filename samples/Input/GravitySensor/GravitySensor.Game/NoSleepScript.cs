// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System.Threading.Tasks;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Physics;

namespace GravitySensor
{
    /// <summary>
    /// This script will make sure that all the physics elements of this entity will never be set to sleep status
    /// The physics engine will sometimes set colliders to sleep state to reduce processor usage when there is no motion happening
    /// Those colliders will wake up if an external (an other collider hitting us) collision happens, but in this case we need to prevent this behavior totally,
    /// as there will be no external collision once the motion is 0.
    /// </summary>
    public class NoSleepScript : AsyncScript
    {
        public override Task Execute()
        {
            foreach (var physicsElement in Entity.GetAll<RigidbodyComponent>())
            {
                physicsElement.CanSleep = false;
            }

            return Task.FromResult(0);
        }
    }
}
