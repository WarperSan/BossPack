using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Unity.Display;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateBoss
{
    public class TemplateT1 : ModBloon
    {
        public override string BaseBloon => BloonType.Bad;
        public override IEnumerable<string> DamageStates => new string[] { };

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            // Don't modify
            bloonModel.RemoveAllChildren();
            bloonModel.danger = 16;
            bloonModel.overlayClass = BloonOverlayClass.Dreadbloon;
            bloonModel.bloonProperties = BloonProperties.None;
            bloonModel.isBoss = true;
            bloonModel.tags = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(new string[] { "Bad", "Moabs", "Boss" });
            // ----

            bloonModel.maxHealth = 100_000; // HP
            bloonModel.speed = 20; // Speed
            bloonModel.icon = GetSpriteReference<Mod.Mod>("Template-Icon"); // Boss's Icon
            bloonModel.AddBehavior(new HealthPercentTriggerModel("TemplateSkull", false,
                new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStructArray<float>(new float[] { 0.5f }), // Skulls (BossPack only support evenly spaced skulls)
                new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(new string[] { "CustomSkull" }), true));
            bloonModel.AddBehavior(new TimeTriggerModel("TemplateTimer", 6, // Timer
                false, new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(new string[] { "CustomTimer" })));
        }
    }

    // 2D boss
    public class TemplateT1Display : ModBloonDisplay<TemplateT1>
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override string Name => base.Name;
        public TemplateT1Display() { }

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.gameObject.transform.GetChild(0).localScale = new Vector3(3, 3);
            Set2DTexture(node, "Template");
        }
    }

    public class BossActions : MonoBehaviour
    {
        public static void Timer()
        {
            // Timer Effect
        }

        public static void Skull()
        {
            // Skull Effect
        }
    }
}
