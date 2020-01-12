using System.IO;
using Unity.Platforms.Build.Classic;
using Unity.Platforms.Build.Common;
using Unity.Platforms.Build.Editor;
using UnityEditor;
using BuildPipeline = Unity.Platforms.Build.BuildPipeline;

namespace Unity.Platforms.MacOS.Build
{
    public static class MenuItemMacOS
    {
        const string k_CreateBuildConfigurationAssetClassic = BuildConfigurationMenuItem.k_BuildConfigurationMenu + "macOS Classic Build Configuration";
        const string k_BuildPipelineClassicAssetPath = "Packages/com.unity.platforms.macos/Editor/Unity.Platforms.macOS.Build/Assets/MacOS Classic.buildpipeline";

        [MenuItem(k_CreateBuildConfigurationAssetClassic, true)]
        static bool CreateBuildConfigurationAssetClassicValidation()
        {
            return Directory.Exists(AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(k_CreateBuildConfigurationAssetClassic)]
        static void CreateBuildConfigurationAsset()
        {
            var pipeline = AssetDatabase.LoadAssetAtPath<BuildPipeline>(k_BuildPipelineClassicAssetPath);
            Selection.activeObject = BuildConfigurationMenuItem.CreateAssetInActiveDirectory(
                "macOSClassic", new GeneralSettings(), new SceneList(), new ClassicBuildProfile { Pipeline = pipeline });
        }
    }
}
