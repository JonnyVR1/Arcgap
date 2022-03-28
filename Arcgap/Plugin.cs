using System.Reflection;
using HarmonyLib;
using IPA;
using IPA.Logging;
using IPA.Utilities;
using JetBrains.Annotations;

namespace Arcgap
{
    [UsedImplicitly]
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		internal static Logger Log { get; private set; }

		internal static Harmony Harmony { get; private set; }

		[Init]
		public Plugin(Logger logger)
		{
			Log = logger;
			Harmony = new Harmony("Kinsi55.BeatSaber.Arcgap");
		}

        [UsedImplicitly]
		[OnEnable]
        public void OnEnable()
        {
            var gameVersion = UnityGame.GameVersion;
            if (gameVersion < new AlmostVersion("1.20.0") || gameVersion >= new AlmostVersion("1.24.0"))
            {
                Log.Warn("Plugin not enabled - This bug should probably fixed in Game Versions after 1.20-1.23");
                return;
            }

            Harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [UsedImplicitly]
		[OnDisable]
		public void OnDisable()
		{
			Harmony.UnpatchSelf();
		}
	}
}
