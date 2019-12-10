using System.IO;
using UnityEditor;
using Unity.Build;
using Unity.Platforms.Build;
using BuildPipeline = Unity.Build.BuildPipeline;
using Unity.Platforms.Desktop.Build;

namespace Unity.Platforms.MacOS.Build
{
    public static class MenuItemMacOS
    {
        const string kBuildSettingsClassic = "Assets/Create/Build/BuildSettings MacOS Classic";
        const string kBuildPipelineClassicAssetPath = "Packages/com.unity.platforms.macos/Editor/Unity.Platforms.macOS.Build/Assets/MacOS Classic.buildpipeline";

        [MenuItem(kBuildSettingsClassic, true)]
        static bool CreateNewBuildSettingsAssetValidationClassic()
        {
            return Directory.Exists(AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(kBuildSettingsClassic)]
        static void CreateNewBuildSettingsAssetClassic()
        {
            MenuItemDesktopBuildSettings.CreateNewBuildSettingsAssetClassic(kBuildPipelineClassicAssetPath);
        }
    }
}
