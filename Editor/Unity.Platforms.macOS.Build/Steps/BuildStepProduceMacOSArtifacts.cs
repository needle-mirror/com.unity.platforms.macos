using System.Diagnostics;
using System.IO;
using Unity.Build;
using UnityEngine;

namespace Unity.Platforms.MacOS.Build
{
    [BuildStep(description = k_Description, category = "Classic")]
    public class BuildStepProduceMacOSArtifacts : BuildStep
    {
        const string k_Description = "Produce MacOS Artifacts";

        public override string Description => k_Description;

        public override BuildStepResult RunBuildStep(BuildContext context)
        {
            var report = context.GetValue<UnityEditor.Build.Reporting.BuildReport>();
            var artifact = context.GetOrCreateValue<BuildArtifactsMacOS>();
            artifact.OutputTargetFile = new FileInfo(report.summary.outputPath);
            return Success();
        }
    }
}
