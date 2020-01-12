using System.Diagnostics;
using System.IO;
using Unity.Platforms.Build;

namespace Unity.Platforms.MacOS.Build
{
    public class RunStepMacOS : RunStep
    {
        public override bool CanRun(BuildConfiguration settings, out string reason)
        {
            var artifact = BuildArtifacts.GetBuildArtifact<BuildArtifactMacOS>(settings);
            if (artifact == null)
            {
                reason = $"Could not retrieve build artifact '{nameof(BuildArtifactMacOS)}'.";
                return false;
            }

            if (artifact.OutputTargetFile == null)
            {
                reason = $"{nameof(BuildArtifactMacOS.OutputTargetFile)} is null.";
                return false;
            }

            // On macOS, the output target is a .app directory structure
            if (!Directory.Exists(artifact.OutputTargetFile.FullName))
            {
                reason = $"Output target file '{artifact.OutputTargetFile.FullName}' not found.";
                return false;
            }

            reason = null;
            return true;
        }

        public override RunStepResult Start(BuildConfiguration settings)
        {
            var artifact = BuildArtifacts.GetBuildArtifact<BuildArtifactMacOS>(settings);
            var process = new Process();
            process.StartInfo.FileName = "open";
            process.StartInfo.Arguments = '\"' + artifact.OutputTargetFile.FullName.Trim('\"') + '\"';
            process.StartInfo.WorkingDirectory = artifact.OutputTargetFile.Directory?.FullName ?? string.Empty;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = true;

            if (!process.Start())
            {
                return Failure(settings, $"Failed to start process at '{process.StartInfo.FileName}'.");
            }

            return Success(settings, new RunInstanceMacOS(process));
        }
    }
}
