using System.IO;
using Unity.Build;

namespace Unity.Platforms.MacOS.Build
{
    [BuildStep(Name = "Produce macOS Artifacts", Description = "Producing macOS Artifacts", Category = "macOS Platform")]
    public class BuildStepProduceMacOSArtifacts : BuildStep
    {
        public override BuildStepResult RunBuildStep(BuildContext context)
        {
            var report = context.GetValue<UnityEditor.Build.Reporting.BuildReport>();
            var artifact = context.GetOrCreateValue<BuildArtifactMacOS>();
            artifact.OutputTargetFile = new FileInfo(report.summary.outputPath);
            return Success();
        }
    }
}
