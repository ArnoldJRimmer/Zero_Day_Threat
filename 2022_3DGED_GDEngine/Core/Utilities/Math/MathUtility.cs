using Microsoft.Xna.Framework;
using System;

namespace GD.Engine.Utilities
{
    /// <summary>
    /// Encapsulates the (time-domain) co-efficiencts applied to a basic trigonometric function (e.g. sine, cos, tan)
    /// </summary>
    public class TrigonometricParameters
    {
        #region Fields

        private float maxAmplitude, angularFrequency, phaseAngle;

        #endregion Fields

        #region Properties

        public float MaxAmplitude
        {
            get
            {
                return maxAmplitude;
            }
            set
            {
                maxAmplitude = (value > 0) ? value : 1;
            }
        }

        public float AngularFrequency
        {
            get
            {
                return angularFrequency;
            }
            set
            {
                angularFrequency = (value > 0) ? value : 1;
            }
        }

        public float PhaseAngle
        {
            get
            {
                return phaseAngle;
            }
            set
            {
                phaseAngle = value;
            }
        }

        #endregion Properties

        public TrigonometricParameters(float maxAmplitude, float angularFrequency, float phaseAngle)
        {
            MaxAmplitude = maxAmplitude;
            AngularFrequency = angularFrequency;
            PhaseAngle = phaseAngle;
        }

        public TrigonometricParameters(float maxAmplitude, float angularFrequency)
            : this(maxAmplitude, angularFrequency, 0)
        {
        }

        public override bool Equals(object obj)
        {
            TrigonometricParameters other = obj as TrigonometricParameters;
            return maxAmplitude == other.MaxAmplitude
                && angularFrequency == other.AngularFrequency
                    && phaseAngle == other.PhaseAngle;
        }

        public override int GetHashCode()
        {
            int hash = 1;
            hash = hash * 31 + maxAmplitude.GetHashCode();
            hash = hash * 17 + angularFrequency.GetHashCode();
            hash = hash * 11 + phaseAngle.GetHashCode();
            return hash;
        }

        public object Clone()
        {
            //deep because all variables are either C# types (e.g. primitives, structs, or enums) or  XNA types
            return MemberwiseClone();
        }
    }

    public class MathUtility
    {
        public static int RandomExcludeNumber(int excludedValue, int max)
        {
            Random random = new Random();
            int randomValue = 0;
            do
            {
                randomValue = random.Next(max);
            } while (randomValue == excludedValue);

            return randomValue;
        }

        public static int RandomExcludeRange(int lo, int hi, int max)
        {
            Random random = new Random();
            int randomValue = 0;
            do
            {
                randomValue = random.Next(max);
            } while ((randomValue >= lo) && (randomValue <= hi));

            return randomValue;
        }

        //lerps along a sine wave with properties defined by TrigonometricParameters (i.e. max amplitude, phase, speed) - see UISineLerpController
        public static float SineLerpByElapsedTime(TrigonometricParameters trigonometricParameters, float totalElapsedTime)
        {
            //range - max amplitude -> + max amplitude
            float lerpFactor = (float)(trigonometricParameters.MaxAmplitude
                * Math.Sin(trigonometricParameters.AngularFrequency
                * MathHelper.ToRadians(totalElapsedTime) + trigonometricParameters.PhaseAngle));
            //range 0 -> 2* max amplitude
            lerpFactor += trigonometricParameters.MaxAmplitude;
            //range 0 -> max amplitude
            lerpFactor /= 2.0f;

            return lerpFactor;
        }
    }
}