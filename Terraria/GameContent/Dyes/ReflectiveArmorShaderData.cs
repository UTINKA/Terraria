// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Dyes.ReflectiveArmorShaderData
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
        Vector2 vector2 = Vector2.op_Addition(position, Vector2.op_Multiply(new Vector2(width, height), 0.1f));
        float num2 = width * 0.8f;
        float num3 = height * 0.8f;
        Vector3 subLight1 = Lighting.GetSubLight(Vector2.op_Addition(vector2, new Vector2(num2 * 0.5f, 0.0f)));
        Vector3 subLight2 = Lighting.GetSubLight(Vector2.op_Addition(vector2, new Vector2(0.0f, num3 * 0.5f)));
        Vector3 subLight3 = Lighting.GetSubLight(Vector2.op_Addition(vector2, new Vector2(num2, num3 * 0.5f)));
        Vector3 subLight4 = Lighting.GetSubLight(Vector2.op_Addition(vector2, new Vector2(num2 * 0.5f, num3)));
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
          Vector2& local = @spinningpoint;
          // ISSUE: explicit reference operation
          double num8 = (^local).X * -1.0;
          // ISSUE: explicit reference operation
          (^local).X = (__Null) num8;
        }
        spinningpoint = spinningpoint.RotatedBy(-(double) num1, (Vector2) null);
        Vector3 vector3;
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).\u002Ector(spinningpoint, (float) (1.0 - (spinningpoint.X * spinningpoint.X + spinningpoint.Y * spinningpoint.Y)));
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local1 = @vector3;
        // ISSUE: explicit reference operation
        double num9 = (^local1).X * 2.0;
        // ISSUE: explicit reference operation
        (^local1).X = (__Null) num9;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local2 = @vector3;
        // ISSUE: explicit reference operation
        double num10 = (^local2).Y - 0.150000005960464;
        // ISSUE: explicit reference operation
        (^local2).Y = (__Null) num10;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local3 = @vector3;
        // ISSUE: explicit reference operation
        double num11 = (^local3).Y * 2.0;
        // ISSUE: explicit reference operation
        (^local3).Y = (__Null) num11;
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).Normalize();
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local4 = @vector3;
        // ISSUE: explicit reference operation
        double num12 = (^local4).Z * 0.600000023841858;
        // ISSUE: explicit reference operation
        (^local4).Z = (__Null) num12;
        this.Shader.get_Parameters().get_Item("uLightSource").SetValue(vector3);
      }
      base.Apply(entity, drawData);
    }
  }
}
