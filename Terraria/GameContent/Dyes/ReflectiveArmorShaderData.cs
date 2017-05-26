// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Dyes.ReflectiveArmorShaderData
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
  public class ReflectiveArmorShaderData : ArmorShaderData
  {
    public ReflectiveArmorShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public override void Apply(Entity entity, DrawData? drawData)
    {
      if (entity == null)
      {
        this.Shader.get_Parameters().get_Item("uLightSource").SetValue(Vector3.get_Zero());
      }
      else
      {
        float num1 = 0.0f;
        if (drawData.HasValue)
          num1 = drawData.Value.rotation;
        Vector2 position = entity.position;
        float width = (float) entity.width;
        float height = (float) entity.height;
        Vector2 vector2_1 = Vector2.op_Multiply(new Vector2(width, height), 0.1f);
        Vector2 vector2_2 = Vector2.op_Addition(position, vector2_1);
        float num2 = width * 0.8f;
        float num3 = height * 0.8f;
        Vector2 vector2_3 = new Vector2(num2 * 0.5f, 0.0f);
        Vector3 subLight1 = Lighting.GetSubLight(Vector2.op_Addition(vector2_2, vector2_3));
        Vector2 vector2_4 = new Vector2(0.0f, num3 * 0.5f);
        Vector3 subLight2 = Lighting.GetSubLight(Vector2.op_Addition(vector2_2, vector2_4));
        Vector2 vector2_5 = new Vector2(num2, num3 * 0.5f);
        Vector3 subLight3 = Lighting.GetSubLight(Vector2.op_Addition(vector2_2, vector2_5));
        Vector2 vector2_6 = new Vector2(num2 * 0.5f, num3);
        Vector3 subLight4 = Lighting.GetSubLight(Vector2.op_Addition(vector2_2, vector2_6));
        float num4 = (float) (subLight1.X + subLight1.Y + subLight1.Z);
        float num5 = (float) (subLight2.X + subLight2.Y + subLight2.Z);
        float num6 = (float) (subLight3.X + subLight3.Y + subLight3.Z);
        float num7 = (float) (subLight4.X + subLight4.Y + subLight4.Z);
        Vector2 spinningpoint;
        // ISSUE: explicit reference operation
        ((Vector2) @spinningpoint).\u002Ector(num6 - num5, num7 - num4);
        // ISSUE: explicit reference operation
        if ((double) ((Vector2) @spinningpoint).Length() > 1.0)
        {
          float num8 = 1f;
          spinningpoint = Vector2.op_Division(spinningpoint, num8);
        }
        if (entity.direction == -1)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local = @spinningpoint.X;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num8 = (double) ^(float&) local * -1.0;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local = (float) num8;
        }
        spinningpoint = spinningpoint.RotatedBy(-(double) num1, (Vector2) null);
        Vector3 vector3;
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).\u002Ector(spinningpoint, (float) (1.0 - (spinningpoint.X * spinningpoint.X + spinningpoint.Y * spinningpoint.Y)));
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local1 = @vector3.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num9 = (double) ^(float&) local1 * 2.0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local1 = (float) num9;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local2 = @vector3.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num10 = (double) ^(float&) local2 - 0.150000005960464;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local2 = (float) num10;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local3 = @vector3.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num11 = (double) ^(float&) local3 * 2.0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local3 = (float) num11;
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).Normalize();
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local4 = @vector3.Z;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num12 = (double) ^(float&) local4 * 0.600000023841858;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local4 = (float) num12;
        this.Shader.get_Parameters().get_Item("uLightSource").SetValue(vector3);
      }
      base.Apply(entity, drawData);
    }
  }
}
