﻿// Type: Microsoft.Xna.Framework.Graphics.SkinnedEffect
// Assembly: MonoGame.Framework, Version=3.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 69677294-4E99-4B9C-B72B-CC2D8AA03B63
// Assembly location: F:\Program Files (x86)\FEZ\MonoGame.Framework.dll

using Microsoft.Xna.Framework;
using System;

namespace Microsoft.Xna.Framework.Graphics
{
  public class SkinnedEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog
  {
    private static readonly byte[] Bytecode = Effect.LoadEffectResource("Microsoft.Xna.Framework.Graphics.Effect.Resources.SkinnedEffect.ogl.mgfxo");
    private int _shaderIndex = -1;
    private Matrix world = Matrix.Identity;
    private Matrix view = Matrix.Identity;
    private Matrix projection = Matrix.Identity;
    private Vector3 diffuseColor = Vector3.One;
    private Vector3 emissiveColor = Vector3.Zero;
    private Vector3 ambientLightColor = Vector3.Zero;
    private float alpha = 1f;
    private float fogEnd = 1f;
    private int weightsPerVertex = 4;
    private EffectDirtyFlags dirtyFlags = EffectDirtyFlags.All;
    public const int MaxBones = 72;
    private EffectParameter textureParam;
    private EffectParameter diffuseColorParam;
    private EffectParameter emissiveColorParam;
    private EffectParameter specularColorParam;
    private EffectParameter specularPowerParam;
    private EffectParameter eyePositionParam;
    private EffectParameter fogColorParam;
    private EffectParameter fogVectorParam;
    private EffectParameter worldParam;
    private EffectParameter worldInverseTransposeParam;
    private EffectParameter worldViewProjParam;
    private EffectParameter bonesParam;
    private bool preferPerPixelLighting;
    private bool oneLight;
    private bool fogEnabled;
    private Matrix worldView;
    private DirectionalLight light0;
    private DirectionalLight light1;
    private DirectionalLight light2;
    private float fogStart;

    public Matrix World
    {
      get
      {
        return this.world;
      }
      set
      {
        this.world = value;
        this.dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.World | EffectDirtyFlags.Fog;
      }
    }

    public Matrix View
    {
      get
      {
        return this.view;
      }
      set
      {
        this.view = value;
        this.dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.EyePosition | EffectDirtyFlags.Fog;
      }
    }

    public Matrix Projection
    {
      get
      {
        return this.projection;
      }
      set
      {
        this.projection = value;
        this.dirtyFlags |= EffectDirtyFlags.WorldViewProj;
      }
    }

    public Vector3 DiffuseColor
    {
      get
      {
        return this.diffuseColor;
      }
      set
      {
        this.diffuseColor = value;
        this.dirtyFlags |= EffectDirtyFlags.MaterialColor;
      }
    }

    public Vector3 EmissiveColor
    {
      get
      {
        return this.emissiveColor;
      }
      set
      {
        this.emissiveColor = value;
        this.dirtyFlags |= EffectDirtyFlags.MaterialColor;
      }
    }

    public Vector3 SpecularColor
    {
      get
      {
        return this.specularColorParam.GetValueVector3();
      }
      set
      {
        this.specularColorParam.SetValue(value);
      }
    }

    public float SpecularPower
    {
      get
      {
        return this.specularPowerParam.GetValueSingle();
      }
      set
      {
        this.specularPowerParam.SetValue(value);
      }
    }

    public float Alpha
    {
      get
      {
        return this.alpha;
      }
      set
      {
        this.alpha = value;
        this.dirtyFlags |= EffectDirtyFlags.MaterialColor;
      }
    }

    public bool PreferPerPixelLighting
    {
      get
      {
        return this.preferPerPixelLighting;
      }
      set
      {
        if (this.preferPerPixelLighting == value)
          return;
        this.preferPerPixelLighting = value;
        this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
      }
    }

    public Vector3 AmbientLightColor
    {
      get
      {
        return this.ambientLightColor;
      }
      set
      {
        this.ambientLightColor = value;
        this.dirtyFlags |= EffectDirtyFlags.MaterialColor;
      }
    }

    public DirectionalLight DirectionalLight0
    {
      get
      {
        return this.light0;
      }
    }

    public DirectionalLight DirectionalLight1
    {
      get
      {
        return this.light1;
      }
    }

    public DirectionalLight DirectionalLight2
    {
      get
      {
        return this.light2;
      }
    }

    public bool FogEnabled
    {
      get
      {
        return this.fogEnabled;
      }
      set
      {
        if (this.fogEnabled == value)
          return;
        this.fogEnabled = value;
        this.dirtyFlags |= EffectDirtyFlags.FogEnable | EffectDirtyFlags.ShaderIndex;
      }
    }

    public float FogStart
    {
      get
      {
        return this.fogStart;
      }
      set
      {
        this.fogStart = value;
        this.dirtyFlags |= EffectDirtyFlags.Fog;
      }
    }

    public float FogEnd
    {
      get
      {
        return this.fogEnd;
      }
      set
      {
        this.fogEnd = value;
        this.dirtyFlags |= EffectDirtyFlags.Fog;
      }
    }

    public Vector3 FogColor
    {
      get
      {
        return this.fogColorParam.GetValueVector3();
      }
      set
      {
        this.fogColorParam.SetValue(value);
      }
    }

    public Texture2D Texture
    {
      get
      {
        return this.textureParam.GetValueTexture2D();
      }
      set
      {
        this.textureParam.SetValue((Texture) value);
      }
    }

    public int WeightsPerVertex
    {
      get
      {
        return this.weightsPerVertex;
      }
      set
      {
        if (value != 1 && value != 2 && value != 4)
          throw new ArgumentOutOfRangeException("value");
        this.weightsPerVertex = value;
        this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
      }
    }

    bool IEffectLights.LightingEnabled
    {
      get
      {
        return true;
      }
      set
      {
        if (!value)
          throw new NotSupportedException("SkinnedEffect does not support setting LightingEnabled to false.");
      }
    }

    static SkinnedEffect()
    {
    }

    public SkinnedEffect(GraphicsDevice device)
      : base(device, SkinnedEffect.Bytecode)
    {
      this.CacheEffectParameters((SkinnedEffect) null);
      this.DirectionalLight0.Enabled = true;
      this.SpecularColor = Vector3.One;
      this.SpecularPower = 16f;
      Matrix[] boneTransforms = new Matrix[72];
      for (int index = 0; index < 72; ++index)
        boneTransforms[index] = Matrix.Identity;
      this.SetBoneTransforms(boneTransforms);
    }

    protected SkinnedEffect(SkinnedEffect cloneSource)
      : base((Effect) cloneSource)
    {
      this.CacheEffectParameters(cloneSource);
      this.preferPerPixelLighting = cloneSource.preferPerPixelLighting;
      this.fogEnabled = cloneSource.fogEnabled;
      this.world = cloneSource.world;
      this.view = cloneSource.view;
      this.projection = cloneSource.projection;
      this.diffuseColor = cloneSource.diffuseColor;
      this.emissiveColor = cloneSource.emissiveColor;
      this.ambientLightColor = cloneSource.ambientLightColor;
      this.alpha = cloneSource.alpha;
      this.fogStart = cloneSource.fogStart;
      this.fogEnd = cloneSource.fogEnd;
      this.weightsPerVertex = cloneSource.weightsPerVertex;
    }

    public void SetBoneTransforms(Matrix[] boneTransforms)
    {
      if (boneTransforms == null || boneTransforms.Length == 0)
        throw new ArgumentNullException("boneTransforms");
      if (boneTransforms.Length > 72)
        throw new ArgumentException();
      this.bonesParam.SetValue(boneTransforms);
    }

    public Matrix[] GetBoneTransforms(int count)
    {
      if (count <= 0 || count > 72)
        throw new ArgumentOutOfRangeException("count");
      Matrix[] valueMatrixArray = this.bonesParam.GetValueMatrixArray(count);
      for (int index = 0; index < valueMatrixArray.Length; ++index)
        valueMatrixArray[index].M44 = 1f;
      return valueMatrixArray;
    }

    public override Effect Clone()
    {
      return (Effect) new SkinnedEffect(this);
    }

    public void EnableDefaultLighting()
    {
      this.AmbientLightColor = EffectHelpers.EnableDefaultLighting(this.light0, this.light1, this.light2);
    }

    private void CacheEffectParameters(SkinnedEffect cloneSource)
    {
      this.textureParam = this.Parameters["Texture"];
      this.diffuseColorParam = this.Parameters["DiffuseColor"];
      this.emissiveColorParam = this.Parameters["EmissiveColor"];
      this.specularColorParam = this.Parameters["SpecularColor"];
      this.specularPowerParam = this.Parameters["SpecularPower"];
      this.eyePositionParam = this.Parameters["EyePosition"];
      this.fogColorParam = this.Parameters["FogColor"];
      this.fogVectorParam = this.Parameters["FogVector"];
      this.worldParam = this.Parameters["World"];
      this.worldInverseTransposeParam = this.Parameters["WorldInverseTranspose"];
      this.worldViewProjParam = this.Parameters["WorldViewProj"];
      this.bonesParam = this.Parameters["Bones"];
      this.light0 = new DirectionalLight(this.Parameters["DirLight0Direction"], this.Parameters["DirLight0DiffuseColor"], this.Parameters["DirLight0SpecularColor"], cloneSource != null ? cloneSource.light0 : (DirectionalLight) null);
      this.light1 = new DirectionalLight(this.Parameters["DirLight1Direction"], this.Parameters["DirLight1DiffuseColor"], this.Parameters["DirLight1SpecularColor"], cloneSource != null ? cloneSource.light1 : (DirectionalLight) null);
      this.light2 = new DirectionalLight(this.Parameters["DirLight2Direction"], this.Parameters["DirLight2DiffuseColor"], this.Parameters["DirLight2SpecularColor"], cloneSource != null ? cloneSource.light2 : (DirectionalLight) null);
    }

    protected internal override bool OnApply()
    {
      this.dirtyFlags = EffectHelpers.SetWorldViewProjAndFog(this.dirtyFlags, ref this.world, ref this.view, ref this.projection, ref this.worldView, this.fogEnabled, this.fogStart, this.fogEnd, this.worldViewProjParam, this.fogVectorParam);
      this.dirtyFlags = EffectHelpers.SetLightingMatrices(this.dirtyFlags, ref this.world, ref this.view, this.worldParam, this.worldInverseTransposeParam, this.eyePositionParam);
      if ((this.dirtyFlags & EffectDirtyFlags.MaterialColor) != ~EffectDirtyFlags.All)
      {
        EffectHelpers.SetMaterialColor(true, this.alpha, ref this.diffuseColor, ref this.emissiveColor, ref this.ambientLightColor, this.diffuseColorParam, this.emissiveColorParam);
        this.dirtyFlags &= ~EffectDirtyFlags.MaterialColor;
      }
      bool flag = !this.light1.Enabled && !this.light2.Enabled;
      if (this.oneLight != flag)
      {
        this.oneLight = flag;
        this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
      }
      if ((this.dirtyFlags & EffectDirtyFlags.ShaderIndex) != ~EffectDirtyFlags.All)
      {
        int num = 0;
        if (!this.fogEnabled)
          ++num;
        if (this.weightsPerVertex == 2)
          num += 2;
        else if (this.weightsPerVertex == 4)
          num += 4;
        if (this.preferPerPixelLighting)
          num += 12;
        else if (this.oneLight)
          num += 6;
        this.dirtyFlags &= ~EffectDirtyFlags.ShaderIndex;
        if (this._shaderIndex != num)
        {
          this._shaderIndex = num;
          this.CurrentTechnique = this.Techniques[this._shaderIndex];
          return true;
        }
      }
      return false;
    }
  }
}
