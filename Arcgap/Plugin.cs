using System.Reflection;
using HarmonyLib;
using IPA;
using IPA.Logging;
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
