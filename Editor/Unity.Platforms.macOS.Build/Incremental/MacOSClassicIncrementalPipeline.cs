#if ENABLE_EXPERIMENTAL_INCREMENTAL_PIPELINE
using Bee.NativeProgramSupport.Building;
using NiceIO;
using System;
using System.Diagnostics;
using System.Linq;
using Unity.Build;
using Unity.Build.Classic.Private;
using Unity.Build.Classic.Private.IncrementalClassicPipeline;
using Unity.Build.Common;
using Unity.BuildSystem.NativeProgramSupport;
using Unity.BuildTools;
using UnityEditor;

namespace Unity.Platforms.MacOS.Build
{
    class MacOSClassicIncrementalPipeline : ClassicIncrementalPipelineBase
    {
        public override Platform Platform => new MacOSXPlatform();
        protected override BuildTarget BuildTarget => BuildTarget.StandaloneOSX;

        public override BuildStepCollection BuildSteps { get; } = new[]
        {
            typeof(SetupCopiesFromSlimPlayerBuild),
            typeof(GraphCopyDefaultResources),
            typeof(GraphSetupCodeGenerationStep),
            typeof(GraphSetupIl2Cpp),
            typeof(GraphSetupNativePlugins),
            typeof(GraphSetupPlayerFiles),
            typeof(SetupAdditionallyProvidedFiles)
        };

        protected override void PrepareContext(BuildContext context)
        {
            base.PrepareContext(context);

            var classicContext = context.GetValue<IncrementalClassicSharedData>();
            var appBundleResourcesDirectory =
                new NPath(context.GetOutputBuildDirectory()).Combine(
                    context.GetComponentOrDefault<GeneralSettings>().ProductName + ".app", "Contents", "Resources");
            var appBundlePluginsDirectory =
                new NPath(context.GetOutputBuildDirectory()).Combine(
                    context.GetComponentOrDefault<GeneralSettings>().ProductName + ".app", "Contents", "Plugins");
            classicContext.DataDeployDirectory = appBundleResourcesDirectory.Combine("Data").MakeAbsolute();

            var classicData = context.GetValue<ClassicSharedData>();
            classicData.StreamingAssetsDirectory = classicContext.DataDeployDirectory.Combine("StreamingAssets").ToString();

            // TODO: Add support for IL2CPP builds too
            classicContext.VariationDirectory = classicContext.PlayerPackageDirectory.Combine("Variations", classicData.DevelopmentPlayer ? "macosx64_development_mono" : "macosx64_nondevelopment_mono").MakeAbsolute();
            classicContext.UnityEngineAssembliesDirectory = classicContext.VariationDirectory.Combine("Data", "Managed");
            classicContext.IL2CPPDataDirectory = classicContext.DataDeployDirectory.Combine("il2cpp_data");
            classicContext.LibraryDeployDirectory = context.GetOutputBuildDirectory();

            var hostToolChain = TypeCache.GetTypesDerivedFrom<ToolChainForHostProvider>().Select(Activator.CreateInstance).Cast<ToolChainForHostProvider>().Select(p => p.Provide()).First(t => t != null);

            classicContext.Architectures.Add(
                Architecture.x64,
                new ClassicBuildArchitectureData()
                {
                    DynamicLibraryDeployDirectory = appBundlePluginsDirectory,
                    BurstTarget = "x64_SSE4",
                    ToolChain = hostToolChain,
                    NativeProgramFormat = hostToolChain.DynamicLibraryFormat
                }
            );
        }

        protected override RunResult OnRun(RunContext context)
        {
            var artifact = context.GetLastBuildArtifact<MacOSArtifact>();
            var process = new Process();

            process.StartInfo.FileName = "open";
            process.StartInfo.Arguments = artifact.OutputTargetFile.FullName.InQuotes();
            process.StartInfo.WorkingDirectory = artifact.OutputTargetFile.Directory?.FullName ?? string.Empty;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = true;

            if (!process.Start())
            {
                return context.Failure($"Failed to start process at '{process.StartInfo.FileName}'.");
            }

            return context.Success(new MacOSRunInstance(process));
        }
    }
}
#endif
