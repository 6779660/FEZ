﻿// Type: OpenTK.Graphics.OpenGL.ClearBufferMask
// Assembly: OpenTK, Version=1.1.0.0, Culture=neutral, PublicKeyToken=bad199fe84eb3df4
// MVID: FE2CFFE8-B942-430E-8C15-E783DB6F0AD6
// Assembly location: F:\Program Files (x86)\FEZ\OpenTK.dll

using System;

namespace OpenTK.Graphics.OpenGL
{
  [Flags]
  public enum ClearBufferMask
  {
    DepthBufferBit = 256,
    AccumBufferBit = 512,
    StencilBufferBit = 1024,
    ColorBufferBit = 16384,
    CoverageBufferBitNv = 32768,
  }
}
