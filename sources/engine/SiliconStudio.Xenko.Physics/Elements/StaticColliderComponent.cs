// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.

using SiliconStudio.Core;

namespace SiliconStudio.Xenko.Physics
{
    [DataContract("StaticColliderComponent")]
    [Display("Static Collider")]
    public sealed class StaticColliderComponent : PhysicsTriggerComponentBase
    {
        protected override void OnAttach()
        {
            NativeCollisionObject = new BulletSharp.CollisionObject
            {
                CollisionShape = ColliderShape.InternalShape,
                ContactProcessingThreshold = !Simulation.CanCcd ? 1e18f : 1e30f,
                UserObject = this
            };

            NativeCollisionObject.CollisionFlags |= BulletSharp.CollisionFlags.NoContactResponse;

            if (ColliderShape.NeedsCustomCollisionCallback)
            {
                NativeCollisionObject.CollisionFlags |= BulletSharp.CollisionFlags.CustomMaterialCallback;
            }

            //this will set all the properties in the native side object
            base.OnAttach();

            UpdatePhysicsTransformation(); //this will set position and rotation of the collider

            Simulation.AddCollider(this, (CollisionFilterGroupFlags)CollisionGroup, CanCollideWith);
        }

        protected override void OnDetach()
        {
            if (NativeCollisionObject == null) return;

            Simulation.RemoveCollider(this);

            base.OnDetach();
        }
    }
}
