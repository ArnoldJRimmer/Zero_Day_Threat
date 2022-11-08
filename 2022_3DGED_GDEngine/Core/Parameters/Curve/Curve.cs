using Microsoft.Xna.Framework;
using System;

namespace GD.Engine.Parameters
{
    /// <summary>
    /// Allows the developer to define a curve based on a scalar value changing over time (e.g. speed vs time, gravity vs time) and then evaluate the scalar quantity for any valid time on that curve
    /// </summary>
    /// <see cref="Curve3D"/>
    public class Curve1D
    {
        #region Fields

        private Curve curve;
        private CurveLoopType curveLookType;
        private bool bSet;

        #endregion Fields

        #region Properties

        public CurveLoopType CurveLookType
        {
            get
            {
                return curveLookType;
            }
        }

        public Curve Curve
        {
            get
            {
                return curve;
            }
        }

        #endregion Properties

        #region Constructors & Core

        //See CurveLoopType - https://msdn.microsoft.com/en-us/library/microsoft.xna.framework.curvelooptype.aspx
        public Curve1D(CurveLoopType curveLookType)
        {
            this.curveLookType = curveLookType;

            curve = new Curve();
            curve.PreLoop = curveLookType;
            curve.PostLoop = curveLookType;
        }

        public void Add(float value, int timeInMS)
        {
            curve.Keys.Add(new CurveKey(timeInMS, value));
            bSet = false;
        }

        private void Set()
        {
            SetTangents(curve);
            bSet = true;
        }

        public void Clear()
        {
            curve.Keys.Clear();
        }

        public float Evaluate(double timeInMS, int decimalPrecision)
        {
            if (!bSet)
                Set();

            return (float)Math.Round(curve.Evaluate((int)timeInMS), decimalPrecision);
        }

        private void SetTangents(Curve curve)
        {
            CurveKey prev;
            CurveKey current;
            CurveKey next;
            int prevIndex;
            int nextIndex;
            for (int i = 0; i < curve.Keys.Count; i++)
            {
                prevIndex = i - 1;
                if (prevIndex < 0)
                {
                    prevIndex = i;
                }

                nextIndex = i + 1;
                if (nextIndex == curve.Keys.Count)
                {
                    nextIndex = i;
                }

                prev = curve.Keys[prevIndex];
                next = curve.Keys[nextIndex];
                current = curve.Keys[i];
                SetCurveKeyTangent(ref prev, ref current, ref next);
                curve.Keys[i] = current;
            }
        }

        private static void SetCurveKeyTangent(ref CurveKey prev, ref CurveKey cur, ref CurveKey next)
        {
            float dt = next.Position - prev.Position;
            float dv = next.Value - prev.Value;
            if (Math.Abs(dv) < float.Epsilon)
            {
                cur.TangentIn = 0;
                cur.TangentOut = 0;
            }
            else
            {
                // The in and out tangents should be equal to the
                // slope between the adjacent keys.
                cur.TangentIn = dv * (cur.Position - prev.Position) / dt;
                cur.TangentOut = dv * (next.Position - cur.Position) / dt;
            }
        }

        #endregion Constructors & Core

        //Add Equals, Clone, ToString, GetHashCode...
    }

    /// <summary>
    /// Allows the developer to define two curves with two independent values changing over time (e.g. (x,y) vs time, (height, width) vs time) and then evaluate the (x,y) quantity for any valid time on that curve
    /// </summary>
    public class Curve2D
    {
        #region Fields

        private Curve1D xCurve, yCurve;
        private CurveLoopType curveLookType;

        #endregion Fields

        #region Properties

        public CurveLoopType CurveLookType
        {
            get
            {
                return curveLookType;
            }
        }

        #endregion Properties

        #region Constructors & Core

        public Curve2D(CurveLoopType curveLoopType)
        {
            curveLookType = curveLoopType;

            xCurve = new Curve1D(curveLoopType);
            yCurve = new Curve1D(curveLoopType);
        }

        public void Add(Vector2 value, int timeInMS)
        {
            xCurve.Add(value.X, timeInMS);
            yCurve.Add(value.Y, timeInMS);
        }

        public void Clear()
        {
            xCurve.Clear();
            yCurve.Clear();
        }

        public Vector2 Evaluate(double timeInMS, int decimalPrecision)
        {
            return new Vector2(xCurve.Evaluate(timeInMS, decimalPrecision), yCurve.Evaluate(timeInMS, decimalPrecision));
        }

        #endregion Constructors & Core

        //Add Equals, Clone, ToString, GetHashCode...
    }

    /// <summary>
    /// Allows the developer to define three curves with three independent values changing over time (e.g. (x,y, z) vs time, (height, width, depth) vs time) and then evaluate the (x,y,z) quantity for any valid time on that curve
    /// </summary>
    /// <see cref="Transform3DCurve"/>
    public class Curve3D
    {
        #region Fields

        private Curve1D xCurve, yCurve, zCurve;
        private CurveLoopType curveLookType;

        #endregion Fields

        #region Properties

        public CurveLoopType CurveLookType
        {
            get
            {
                return curveLookType;
            }
        }

        #endregion Properties

        #region Constructors & Core

        public Curve3D(CurveLoopType curveLoopType)
        {
            curveLookType = curveLoopType;

            xCurve = new Curve1D(curveLoopType);
            yCurve = new Curve1D(curveLoopType);
            zCurve = new Curve1D(curveLoopType);
        }

        public void Add(Vector3 value, int timeInMS)
        {
            xCurve.Add(value.X, timeInMS);
            yCurve.Add(value.Y, timeInMS);
            zCurve.Add(value.Z, timeInMS);
        }

        public void Clear()
        {
            xCurve.Clear();
            yCurve.Clear();
            zCurve.Clear();
        }

        public Vector3 Evaluate(double timeInMS, int decimalPrecision)
        {
            return new Vector3(xCurve.Evaluate(timeInMS, decimalPrecision),
                yCurve.Evaluate(timeInMS, decimalPrecision),
                 zCurve.Evaluate(timeInMS, decimalPrecision));
        }

        #endregion Constructors & Core

        //Add Equals, Clone, ToString, GetHashCode...
    }
}