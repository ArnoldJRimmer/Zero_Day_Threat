#region Using Statements

using JigLibX.Geometry;
using Microsoft.Xna.Framework;

#endregion

namespace JigLibX.Collision
{
    /// <summary>
    /// Class CollDetectCapsulePlane
    /// </summary>
    public class CollDetectCapsulePlane : DetectFunctor
    {
        /// <summary>
        /// DetectFunctor for CapsulePlane collison detection.
        /// </summary>
        public CollDetectCapsulePlane()
            : base("CapsulePlane", (int)PrimitiveType.Capsule, (int)PrimitiveType.Plane)
        {
        }

        /// <summary>
        /// CollDetect
        /// </summary>
        /// <param name="info"></param>
        /// <param name="collTolerance"></param>
        /// <param name="collisionFunctor"></param>
        public override void CollDetect(CollDetectInfo info, float collTolerance, CollisionFunctor collisionFunctor)
        {
            if (info.Skin0.GetPrimitiveOldWorld(info.IndexPrim0).Type == this.Type1)
            {
                CollisionSkin skinSwap = info.Skin0;
                info.Skin0 = info.Skin1;
                info.Skin1 = skinSwap;
                int primSwap = info.IndexPrim0;
                info.IndexPrim0 = info.IndexPrim1;
                info.IndexPrim1 = primSwap;
            }

            Vector3 body0Pos = (info.Skin0.Owner != null) ? info.Skin0.Owner.OldPosition : Vector3.Zero;
            Vector3 body1Pos = (info.Skin1.Owner != null) ? info.Skin1.Owner.OldPosition : Vector3.Zero;

            // todo - proper swept test
            Capsule oldCapsule = (Capsule)info.Skin0.GetPrimitiveOldWorld(info.IndexPrim0);
            Capsule newCapsule = (Capsule)info.Skin0.GetPrimitiveNewWorld(info.IndexPrim0);

            JigLibX.Geometry.Plane oldPlane = (JigLibX.Geometry.Plane)info.Skin1.GetPrimitiveOldWorld(info.IndexPrim1);
            JigLibX.Geometry.Plane newPlane = (JigLibX.Geometry.Plane)info.Skin1.GetPrimitiveNewWorld(info.IndexPrim1);

            Matrix newPlaneInvTransform = newPlane.InverseTransformMatrix;
            Matrix oldPlaneInvTransform = oldPlane.InverseTransformMatrix;

            unsafe
            {
#if USE_STACKALLOC
                SmallCollPointInfo* collPts = stackalloc SmallCollPointInfo[MaxLocalStackSCPI];
#else
                SmallCollPointInfo[] collPtArray = SCPIStackAlloc();
                fixed (SmallCollPointInfo* collPts = collPtArray)
#endif
                {
                    int numCollPts = 0;

                    // the start
                    {
                        Vector3 oldCapsuleStartPos = Vector3.Transform(oldCapsule.Position, oldPlaneInvTransform);
                        Vector3 newCapsuleStartPos = Vector3.Transform(newCapsule.Position, newPlaneInvTransform);

                        float oldDist = Distance.PointPlaneDistance(oldCapsuleStartPos, oldPlane);
                        float newDist = Distance.PointPlaneDistance(newCapsuleStartPos, newPlane);

                        if (MathHelper.Min(newDist, oldDist) < collTolerance + newCapsule.Radius)
                        {
                            float oldDepth = oldCapsule.Radius - oldDist;
                            // calc the world position based on the old position8(s)
                            Vector3 worldPos = oldCapsule.Position - oldCapsule.Radius * oldPlane.Normal;

                            // BEN-OPTIMISATION: Now reuses existing collPts instead of reallocating.
                            collPts[numCollPts].R0 = worldPos - body0Pos;
                            collPts[numCollPts].R1 = worldPos - body1Pos;
                            collPts[numCollPts++].InitialPenetration = oldDepth;
                        }
                    }

                    // the end
                    {
                        Vector3 oldCapsuleEndPos = Vector3.Transform(oldCapsule.GetEnd(), oldPlaneInvTransform);
                        Vector3 newCapsuleEndPos = Vector3.Transform(newCapsule.GetEnd(), newPlaneInvTransform);
                        float oldDist = Distance.PointPlaneDistance(oldCapsuleEndPos, oldPlane);
                        float newDist = Distance.PointPlaneDistance(newCapsuleEndPos, newPlane);

                        if (System.Math.Min(newDist, oldDist) < collTolerance + newCapsule.Radius)
                        {
                            float oldDepth = oldCapsule.Radius - oldDist;
                            // calc the world position based on the old position(s)
                            Vector3 worldPos = oldCapsule.GetEnd() - oldCapsule.Radius * oldPlane.Normal;

                            // BEN-OPTIMISATION: Now reuses existing collPts instead of reallocating.
                            collPts[numCollPts].R0 = worldPos - body0Pos;
                            collPts[numCollPts].R1 = worldPos - body1Pos;
                            collPts[numCollPts++].InitialPenetration = oldDepth;
                        }

                        if (numCollPts > 0)
                        {
                            collisionFunctor.CollisionNotify(ref info, ref oldPlane.normal, collPts, numCollPts);
                        }
                    }
                }
#if !USE_STACKALLOC
                FreeStackAlloc(collPtArray);
#endif
            }
        }
    }
}