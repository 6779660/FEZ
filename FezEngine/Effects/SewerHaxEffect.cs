﻿// Type: FezEngine.Effects.SewerHaxEffect
// Assembly: FezEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11F00D13-0150-47CC-B906-98B362969219
// Assembly location: F:\Program Files (x86)\FEZ\FezEngine.dll

using FezEngine.Effects.Structures;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Graphics;

namespace FezEngine.Effects
{
  public class SewerHaxEffect : BaseEffect
  {
    private readonly SemanticMappedTexture texture;

    public SewerHaxEffect()
      : base("SewerHaxEffect")
    {
      this.texture = new SemanticMappedTexture(this.effect.Parameters, "BaseTexture");
    }

    public override void Prepare(Mesh mesh)
    {
      base.Prepare(mesh);
      this.texture.Set((Texture) mesh.Texture);
    }
  }
}
