using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using Data;
using HarmonyLib;
using Il2CppAssets.Main.Scenes;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Utils;
using Il2CppTMPro;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using HarmonyPatch = HarmonyLib.HarmonyPatch;
using HarmonyPostfix = HarmonyLib.HarmonyPostfix;
using Image = UnityEngine.UI.Image;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[assembly: MelonInfo(typeof(Mod.Mod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Mod
{
    public class Mod : BloonsTD6Mod
    {
        public static Dictionary<int, SpriteReference> BossRounds = new Dictionary<int, SpriteReference>();

        public static int SkullsCount = 3;

        public static Bloon? boss = null;

        // These variables store data on the boss for performing mechanics
        public static float bossShieldCount = 0;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (InGame.instance == null)
                return;

            if (InGame.instance.bridge == null)
                return;

            if (InGame.instance.bridge.GetCurrentRound() + 1 > 120)
                return;

            // Checks if the boss is active
            foreach (BloonToSimulation bloonSimulation in InGame.instance.bridge.GetAllBloons())
            {
                Bloon bloon = bloonSimulation.GetBloon();

                if (bloon.GetBloonBehavior<HealthPercentTrigger>() != null)
                {
                    UI.WriteBoss(bloon);
                    break;
                }
            }
        }

        public static System.Random rdm = new System.Random();

        [HarmonyPatch(typeof(TitleScreen), "Start")]
        public class BossCreation
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                Game.instance.model.maxHealth = 500_000;

                AddBoss(GetBossInfos(int.Parse(Round40Boss.GetValue().ToString())), 1, 40);
                AddBoss(GetBossInfos(int.Parse(Round60Boss.GetValue().ToString())), 2, 60);
                AddBoss(GetBossInfos(int.Parse(Round80Boss.GetValue().ToString())), 3, 80);
                AddBoss(GetBossInfos(int.Parse(Round100Boss.GetValue().ToString())), 4, 100);
                AddBoss(GetBossInfos(int.Parse(Round120Boss.GetValue().ToString())), 5, 120);
            }
        }

        private static string[][] allInfos = new string[][] {
            new string[] { "The Demon Prince", "BossPack-PrinceT", "Prince-Icon" },
            new string[] { "Queen of Jaws", "BossPack-CeverT", "Cever-Icon" },
            new string[] { "Bloontonium Expert", "BossPack-EtdbT", "Etdb-Icon" },
            new string[] { "Flame of Terror", "BossPack-FlameT", "Flame-Icon" },
            new string[] { "Ghost King", "BossPack-WightT", "Wight-Icon" },
            new string[] { "Goemon's Student", "BossPack-NinjaT", "Ninja-Icon" }
        };

        private static string[] GetBossInfos(int num)
        {
            return allInfos[num];
        }

        public static readonly ModSettingInt Round40Boss = new(1)
        {
            description = "0 = The Demon Prince (PRINCE)\n1 = Queen of Jaws (CEVER)\n2 = Bloontonium Expert (ETDB)\n3 = Flame of Terror\n4 = Ghost King (WIGHT)\n5 = Goemon's Student (NINJA)",
            min = 0,
            max = 5,
            requiresRestart = true,
            displayName = "Round 40 Boss"
        };
        public static readonly ModSettingInt Round60Boss = new(0)
        {
            description = Round40Boss.description,
            min = Round40Boss.min,
            max = Round40Boss.max,
            requiresRestart = true,
            displayName = "Round 60 Boss"
        };
        public static readonly ModSettingInt Round80Boss = new(3)
        {
            description = Round40Boss.description,
            min = Round40Boss.min,
            max = Round40Boss.max,
            requiresRestart = true,
            displayName = "Round 80 Boss"
        };
        public static readonly ModSettingInt Round100Boss = new(4)
        {
            description = Round40Boss.description,
            min = Round40Boss.min,
            max = Round40Boss.max,
            requiresRestart = true,
            displayName = "Round 100 Boss"
        };
        public static readonly ModSettingInt Round120Boss = new(2)
        {
            description = Round40Boss.description,
            min = Round40Boss.min,
            max = Round40Boss.max,
            requiresRestart = true,
            displayName = "Round 120 Boss"
        };

        public static void AddBoss(string[] bossInfos, int tier, int round)
        {
            if (BossRounds.ContainsKey(round))
            {
                BossRounds[round] = ModContent.GetSpriteReference<Mod>(bossInfos[2]);
            }
            else
                BossRounds.Add(round, ModContent.GetSpriteReference<Mod>(bossInfos[2]));

            ModHelper.Msg<Mod>($"{bossInfos[0]} added on round {round}");
            Game.instance.model.roundSets[1].rounds[round - 1].AddBloonGroup(bossInfos[1] + tier.ToString());
        }

        // ---------------------------------------

        [HarmonyPatch(typeof(HealthPercentTrigger), "Trigger")]
        public class BossSkull
        {
            [HarmonyPrefix]
            public static bool Prefix(HealthPercentTrigger __instance)
            {
                string modelName = __instance.model._name.Replace("HealthPercentTriggerModel_", "");

                switch (modelName)
                {
                    case "CeverSkull":
                        __instance.bloon.AddMutator(new SpeedUpMutator("", 10f), 9);
                        break;
                    case "EtdbSkull":
                        InGame.instance.SpawnBloons("Purple", 100, 0);
                        break;
                    case "FlameSkull":
                        InGame.instance.bridge.SetCash(InGame.instance.bridge.GetCash() * 0.6);
                        break;
                    case "PrinceSkull":
                        InGame.instance.AddHealth(-50 * (InGame.instance.bridge.GetCurrentRound() + 1) / 40);
                        break;
                    case "WightSkull":
                        Wight.BossActions.Skull(__instance.bloon);
                        break;
                    case "NinjaSkull":
                        Ninja.BossActions.Skull();
                        break;
                    default:
                        break;
                }

                if (!InGame.instance.bridge.IsSandboxMode())
                    UI.ActivateSkull();

                return true;
            }
        }

        [HarmonyPatch(typeof(TimeTrigger), "Trigger")]
        public class BossTimer
        {
            [HarmonyPrefix]
            public static bool Prefix(TimeTrigger __instance)
            {
                string modelName = __instance.model.name.Replace("TimeTriggerModel_", "");

                switch (modelName)
                {
                    case "CeverTimer":
                        Cever.BossActions.Timer();
                        break;
                    case "EtdbTimer":
                        Etdb.BossActions.Timer(__instance.bloon);
                        break;
                    case "FlameTimer":
                        int amount = 60 * (InGame.instance.bridge.GetCurrentRound() + 1) / 40;
                        InGame.instance.SpawnBloons("Ceramic", amount, 10);
                        break;
                    case "PrinceTimer":
                        InGame.instance.AddHealth(-2);
                        break;
                    case "WightTimer":
                        InGame.instance.SpawnBloons("Purple", 10 * (InGame.instance.bridge.GetCurrentRound() + 1) / 20, 1);
                        break;
                    case "NinjaTimer":
                        Ninja.BossActions.Timer(__instance.bloon);
                        break;
                    default:
                        break;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(InGame), "OnDestroy")]
        public class OnDestroy
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                UI.BossText = null;
            }
        }

        [HarmonyPatch(typeof(InGame), "Initialise")]
        public class InGameInitialise
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                Reset();
            }
        }
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            Reset();
        }
        private static void Reset()
        {
            boss = null;

            if (BossRounds.Count == 0)
                return;

            if (InGame.instance.bridge.IsSandboxMode())
                return;

            int BossIndex = 0;
            List<KeyValuePair<int, SpriteReference>> bosses = BossRounds.ToList();

            while (InGame.instance.bridge.GetCurrentRound() + 1 > bosses[BossIndex].Key) BossIndex++;

            int roundsRemaining = bosses[BossIndex].Key - InGame.instance.bridge.GetCurrentRound() - 1;
            UI.WriteWait($"Boss Appears in: {roundsRemaining} Rounds", bosses[BossIndex].Value);
        }

        [HarmonyPatch(typeof(Bloon), "Damage")]
        public class BloonDamage
        {
            [HarmonyPrefix]
            public static bool Prefix(Bloon __instance, float totalAmount, Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Projectile projectile, bool distributeToChildren, bool overrideDistributeBlocker, bool createEffect, Il2CppAssets.Scripts.Simulation.Towers.Tower tower, Il2Cpp.BloonProperties immuneBloonProperties, bool canDestroyProjectile, bool ignoreNonTargetable, bool blockSpawnChildren, bool ignoreInvunerable)
            {
                switch (__instance.bloonModel.name)
                {
                    case "BossPack-WillOTheWisp":
                        if(tower != null)
                        {
                            if (tower.towerModel.baseId == "IceMonkey")
                                return true;
                        }
                        return false;
                    default:
                        break;
                }
                return true; // Continue
            }
        }

        [HarmonyPatch(typeof(Bloon), "OnDestroy")]
        public class BloonDestroy
        {
            [HarmonyPrefix]
            public static bool Prefix(Bloon __instance)
            {
                if (__instance.bloonModel.name == "BossPack-WillOTheWisp")
                    WillOTheWispCountdown();

                return true; // Continue
            }
        }

        // Kill the player if boss leaks
        public override bool PreBloonLeaked(Bloon bloon)
        {
            if (bloon.bloonModel.danger >= 16)
                bloon.bloonModel.leakDamage = (float)(InGame.instance.GetHealth() + 1);

            if (bloon.bloonModel.name == "BossPack-WillOTheWisp")
                WillOTheWispCountdown();

            return base.PreBloonLeaked(bloon);
        }

        private static void WillOTheWispCountdown()
        {
            bossShieldCount--;

            if (bossShieldCount <= 0)
            {
                bossShieldCount = 0;

                List<BloonToSimulation> bloons = InGame.instance.GetAllBloonToSim().ToList();

                for (int i = 0; i < bloons.Count; i++)
                {
                    if (bloons[i].GetBloon().bloonModel.name.Contains("Wight"))
                        bloons[i].GetBloon().IsInvulnerable = false;
                }
            }
        }
    }

    public class UI : MelonMod
    {
        private static UnityEngine.Object? bar = null;
        private static UnityEngine.Object? wait = null;
        private static UnityEngine.Object? skullTemp = null;

        private static void Initialise()
        {
            // Find a better way to get the UIs

            // Get Assets
            if (bar == null)
            {
                AssetBundle bossBundle = ModContent.GetBundle<Mod>("bossbundle");
                // Boss Bar
                bar = bossBundle.LoadAsset("BossBar");
                wait = bossBundle.LoadAsset("BossWait");
                skullTemp = bossBundle.LoadAsset("Skull");
            }

            // Instantiate Boss Bar
            BossUI = GameObject.Find(UnityEngine.Object.Instantiate(bar).name);
            BossUI.transform.parent = GameObject.Find("UIRect").transform;
            BossUI.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<Image>().pixelsPerUnitMultiplier = 0.1f;
            BossUI.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
            BossUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -300);
            BossUI.transform.localScale = new Vector3(4, 4, 4);
            BossUI.SetActive(false);

            // Instantiate Boss Wait
            BossWaitUI = GameObject.Find(UnityEngine.Object.Instantiate(wait).name);
            BossWaitUI.transform.parent = BossUI.transform.parent;
            BossWaitUI.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().font = Fonts.Btd6FontTitle;
            BossWaitUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -200);
            BossWaitUI.transform.localScale = new Vector3(4, 4, 4);
            BossWaitUI.SetActive(false);

            // Slider
            BossSlider = BossUI.transform.GetChild(1).GetComponent<Slider>();

            // Text
            BossUI.transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().font = Fonts.Btd6FontTitle;
            BossText = BossUI.transform.GetChild(1).GetChild(1).GetChild(3).gameObject;

            // Icon
            BossIcon = BossUI.transform.GetChild(0).gameObject;

            // Skulls
            if (SkullPrefab == null)
                UnityEngine.Object.Instantiate(skullTemp);

            SkullPrefab = GameObject.Find("Skull(Clone)");

            SkullContainer = BossUI.transform.GetChild(1).GetChild(1).GetChild(4);
        }

        public static GameObject? BossText = null;
        public static GameObject? BossIcon = null;

        private static GameObject? BossWaitUI = null;
        public static void WriteWait(string text, SpriteReference Icon)
        {
            if (BossText == null)
                Initialise();

            if (BossWaitUI == null || BossUI == null)
                return;

            if (!BossWaitUI.activeSelf)
            {
                BossWaitUI.SetActive(true);
                BossUI.SetActive(false);
            }

            BossWaitUI.transform.GetChild(1).GetComponent<Image>().SetSprite(Icon);
            BossWaitUI.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = text;
        }

        private static GameObject? BossUI = null;
        private static Bloon? LastBoss = null;
        public static void WriteBoss(Bloon boss)
        {
            if (BossText == null)
                Initialise();

            if (BossText == null || BossIcon == null || BossUI == null || BossWaitUI == null)
                return;

            if (!BossUI.activeSelf)
            {
                BossUI.SetActive(true);
                BossWaitUI.SetActive(false);
            }

            if (LastBoss == null)
            {
                LastBoss = boss;
                BossIcon.GetComponent<Image>().SetSprite(boss.bloonModel.icon);
                SpawnSkulls(boss.bloonModel.GetBehavior<HealthPercentTriggerModel>().percentageValues.Count);
            }

            UpdateBossBar(boss.health, boss.bloonModel.maxHealth);
        }

        private static Slider? BossSlider = null;
        public static void UpdateBossBar(float value, float maxValue)
        {
            if (BossSlider == null)
                return;

            if (BossSlider.maxValue != maxValue)
                BossSlider.maxValue = maxValue;

            BossSlider.value = value;
            BossText.GetComponent<TextMeshProUGUI>().text = $"{Math.Floor(value)} / {maxValue}";
        }

        public static GameObject? SkullPrefab = null;
        private static Transform? SkullContainer = null;
        public static void SpawnSkulls(int amount)
        {
            if (SkullContainer == null || SkullPrefab == null || BossText == null)
                return;

            for (int i = 0; i < SkullContainer.childCount; i++) GameObject.Destroy(SkullContainer.GetChild(i).gameObject);

            if (amount >= 6)
                BossText.transform.parent.GetChild(4).GetComponent<HorizontalLayoutGroup>().spacing = -40;
            else if (amount == 5)
                BossText.transform.parent.GetChild(4).GetComponent<HorizontalLayoutGroup>().spacing = -50;
            else
                BossText.transform.parent.GetChild(4).GetComponent<HorizontalLayoutGroup>().spacing = -150 + 30 * (amount - 1);

            for (int i = 0; i < amount; i++)
            {
                GameObject skull = GameObject.Instantiate(SkullPrefab);
                skull.GetComponent<Image>().enabled = true;
                skull.transform.parent = SkullContainer;
                skull.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }

            skullIndex = amount;
        }

        private static int skullIndex = 3;
        public static void ActivateSkull()
        {
            skullIndex--;

            if (SkullContainer == null)
                return;

            if (SkullContainer.transform.childCount < skullIndex || skullIndex < 0)
                return;

            SkullContainer.transform.GetChild(skullIndex).GetComponent<Animator>().enabled = true;
        }
    }
}