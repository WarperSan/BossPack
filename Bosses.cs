/// <summary>
/// Bare minimum version
/// </summary>
public class SimpleBoss : ModBoss
{
    public override Dictionary<int, BossRoundInfo> RoundsInfo => new Dictionary<int, BossRoundInfo>()
    {
        [/* Round */] = new BossRoundInfo()
        {
            /* Stats */
        }
    };

    public override string Icon => /* Icon */;

    public override void ModifyBaseBloonModel(BloonModel bloonModel)
    {
        /* Modify the stats */
    }
}

/// <summary>
/// Version for people who wants all functionalities but don't want to manage UI
/// </summary>
public class CompleteBoss : ModBoss
{
    public override string DisplayName => /* Display name */;
    public override string Description => /* Boss description */;
    public override string ExtraCredits => /* Extra credits */;
    public override string Icon => /* Icon */;

    public override Dictionary<int, BossRoundInfo> RoundsInfo => new Dictionary<int, BossRoundInfo>()
    {
        [/* Round */] = new BossRoundInfo()
        {
            /* Stats */
        }
    };

    public override void ModifyBaseBloonModel(BloonModel bloonModel)
    {
        /* Set base stats */
    }

    public override BloonModel ModifyForRound(BloonModel bloon, int round)
    {
        /* Modify according to the round */

        return base.ModifyForRound(bloon, round);
    }

    public override string SkullDescription => /* Skull description */;
    public override bool PreventFallThrough => /* Prevent fall through */;
    public override void SkullEffect(Bloon boss)
    {
        /* Skull Effect */
        base.SkullEffect(boss);
    }

    public override string TimerDescription => /* Timer description */;
    public override void TimerTick(Bloon boss)
    {
        /* Do timer tick */
    }
}

/// <summary>
/// Version for people who wants to manage the everything on their own
/// </summary>
public class FullBoss : ModBoss
{
    public override string DisplayName => /* Display name */;
    public override string Description => /* Boss description */;
    public override string ExtraCredits => /* Extra credits */;
    public override string Icon => /* Icon */;

    public override Dictionary<int, BossRoundInfo> RoundsInfo => new Dictionary<int, BossRoundInfo>()
    {
        [/* Round */] = new BossRoundInfo()
        {
            /* Stats */
        }
    };

    public override void ModifyBaseBloonModel(BloonModel bloonModel)
    {
        /* Set base stats */
    }

    public override ModHelperPanel AddWaitPanel(ModHelperPanel waitingHolderPanel)
    {
        /* Custom wait panel */
        return base.AddWaitPanel(waitingHolderPanel);
    }

    public override BloonModel ModifyForRound(BloonModel bloon, int round)
    {
        /* Modify according to the round */

        return base.ModifyForRound(bloon, round);
    }

    public override ModHelperPanel AddBossPanel(ModHelperPanel holderPanel)
    {
        /* Custom boss panel */
        return base.AddBossPanel(holderPanel);
    }

    public override string SkullDescription => /* Skull description */;
    public override bool PreventFallThrough => /* Prevent fall through */;
    public override void SkullEffect(Bloon boss)
    {
        /* Skull Effect */
        base.SkullEffect(boss);
    }

    public override string TimerDescription => /* Timer description */;
    public override void TimerTick(Bloon boss)
    {
        /* Do timer tick */
    }
}

/// <summary>
/// Old System
/// </summary>
public class Template : ModBloon
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
        bloonModel.icon = GetSpriteReference<TemplateMod>("ClicksBtn"); // Boss's Icon
        bloonModel.AddBehavior(new HealthPercentTriggerModel("TemplateSkull", false,
            new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStructArray<float>(new float[] { 0.5f }), // Skulls (BossPack only support evenly spaced skulls)
            new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(new string[] { "CustomSkull" }), true));
        bloonModel.AddBehavior(new TimeTriggerModel("TemplateTimer", 6, // Timer
            false, new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(new string[] { "CustomTimer" })));
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

/// <summary>
/// New System
/// </summary>
public class TemplateNew : ModBoss
{
    public override string Icon => "ClicksBtn";
    public override IEnumerable<string> DamageStates => new string[] { };

    public override Dictionary<int, BossRoundInfo> RoundsInfo => new Dictionary<int, BossRoundInfo>()
    {
        [1] = new BossRoundInfo()
        {
            percentageValues = new float[] { 0.5f },
            interval = 6,
            preventFallThrough = true,
        }
    };

    public override void ModifyBaseBloonModel(BloonModel bloonModel)
    {
        bloonModel.maxHealth = 100_000; // HP
        bloonModel.speed = 20; // Speed
    }

    public override void SkullEffect(Bloon boss) { }
    public override void TimerTick(Bloon boss) { }
}
