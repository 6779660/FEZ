﻿// Type: Microsoft.Xna.Framework.CurveKey
// Assembly: MonoGame.Framework.Windows, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2107839-320D-467B-B82A-28CB452CC584
// Assembly location: F:\Program Files (x86)\FEZ\MonoGame.Framework.Windows.dll

using System;

namespace Microsoft.Xna.Framework
{
  public class CurveKey : IEquatable<CurveKey>, IComparable<CurveKey>
  {
    private CurveContinuity continuity;
    private float position;
    private float tangentIn;
    private float tangentOut;
    private float value;

    public CurveContinuity Continuity
    {
      get
      {
        return this.continuity;
      }
      set
      {
        this.continuity = value;
      }
    }

    public float Position
    {
      get
      {
        return this.position;
      }
    }

    public float TangentIn
    {
      get
      {
        return this.tangentIn;
      }
      set
      {
        this.tangentIn = value;
      }
    }

    public float TangentOut
    {
      get
      {
        return this.tangentOut;
      }
      set
      {
        this.tangentOut = value;
      }
    }

    public float Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public CurveKey(float position, float value)
      : this(position, value, 0.0f, 0.0f, CurveContinuity.Smooth)
    {
    }

    public CurveKey(float position, float value, float tangentIn, float tangentOut)
      : this(position, value, tangentIn, tangentOut, CurveContinuity.Smooth)
    {
    }

    public CurveKey(float position, float value, float tangentIn, float tangentOut, CurveContinuity continuity)
    {
      this.position = position;
      this.value = value;
      this.tangentIn = tangentIn;
      this.tangentOut = tangentOut;
      this.continuity = continuity;
    }

    public static bool operator !=(CurveKey a, CurveKey b)
    {
      return !(a == b);
    }

    public static bool operator ==(CurveKey a, CurveKey b)
    {
      if (object.Equals((object) a, (object) null))
        return object.Equals((object) b, (object) null);
      if (object.Equals((object) b, (object) null))
        return object.Equals((object) a, (object) null);
      else
        return (double) a.position == (double) b.position && (double) a.value == (double) b.value && ((double) a.tangentIn == (double) b.tangentIn && (double) a.tangentOut == (double) b.tangentOut) && a.continuity == b.continuity;
    }

    public CurveKey Clone()
    {
      return new CurveKey(this.position, this.value, this.tangentIn, this.tangentOut, this.continuity);
    }

    public int CompareTo(CurveKey other)
    {
      return this.position.CompareTo(other.position);
    }

    public bool Equals(CurveKey other)
    {
      return this == other;
    }

    public override bool Equals(object obj)
    {
      return obj is CurveKey && (CurveKey) obj == this;
    }

    public override int GetHashCode()
    {
      return this.position.GetHashCode() ^ this.value.GetHashCode() ^ this.tangentIn.GetHashCode() ^ this.tangentOut.GetHashCode() ^ this.continuity.GetHashCode();
    }
  }
}
