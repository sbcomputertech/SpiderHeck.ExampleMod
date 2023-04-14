using BepInEx;
using HarmonyLib;

namespace ExampleMod
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string ModName = "ExampleMod";
        public const string ModAuthor  = "Your Name";
        public const string ModGUID = "com.example.mod";
        public const string ModVersion = "1.0.2";
        
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
    * I recommend using typeof/nameof as they provide IDE auto-completion, and also throw compiler errors if you mistype a name
    * However, in cases like private methods you would have to use the string parameters
    */
    public class VersionNumberTextMeshPatch
    {
        // Postfix is called after executing target method's code.
        public static void Postfix(ref VersionNumberTextMesh __instance)
        {
            // We're adding new line to version text.
            __instance.textMesh.text += $"\n<color=red>The example mod loads!</color>";
        }
    }
}
