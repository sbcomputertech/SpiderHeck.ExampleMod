using BepInEx;
using HarmonyLib;

namespace ExampleMod
{
    [BepInPlugin(ModName, ModGUID, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string ModName = "ExampleMod";
        public const string ModAuthor  = "John";
        public const string ModGUID = "com.john.examplemod";
        public const string ModVersion = "1.0.1";
        internal Harmony Harmony;
        internal void Awake()
        {
            // Creating new harmony instance
            Harmony = new Harmony(ModGUID);

            // Applying patches
            Harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
        }
    }
    /* Harmony patches modify the game code at runtime
     * Official website: https://harmony.pardeike.net/
     * Introduction: https://harmony.pardeike.net/articles/intro.html
     * API Documentation: https://harmony.pardeike.net/api/index.html
     */

    // Here's the example of harmony patch
    [HarmonyPatch(typeof(VersionNumberTextMesh), nameof(VersionNumberTextMesh.Start))]
    /* We're patching the method "Start" of class VersionNumberTextMesh
    * The first argument can typeof(class) or class name (string). Warning: it's case-sensitive
    * The second argument is our method. It can be a nameof(class.method) or method name (string). Also case-sensitive
    * So, for example, patch can look like this:
    * [HarmonyPatch("VersionNumberTextMesh", "Start")]
    * Or like this:
    * [HarmonyPatch(typeof(VersionNumberTextMesh), nameof(VersionNumberTextMesh.Start))
    */
    public class VersionNumberTextMeshPatch
    {
        // Postfix is called after executing target method's code.
        public static void Postfix(VersionNumberTextMesh __instance)
        {
            // We're adding new line to version text.
            __instance.textMesh.text += $"\n<color=red>{Main.ModName} v{Main.ModVersion} by {Main.ModAuthor}</color>";
        }
    }
}
