using System.Diagnostics;
using System.IO;

namespace Unity.Platforms.MacOS
{
    public abstract class MacOSBuildTarget : BuildTarget
    {
        public override bool HideInBuildTargetPopup => UnityEngine.Application.platform != UnityEngine.RuntimePlatform.OSXEditor;

        public override string GetExecutableExtension()
        {
            return string.Empty;
        }

        public override string GetUnityPlatformName()
        {
            return nameof(UnityEditor.BuildTarget.StandaloneOSX);
        }

        public override bool Run(FileInfo buildTarget)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.Arguments = $"\"{buildTarget.FullName.Trim('\"')}\"";
            startInfo.FileName = Path.GetFullPath(Path.Combine(UnityEditor.EditorApplication.applicationContentsPath, "MonoBleedingEdge", "bin", "mono"));
            startInfo.WorkingDirectory = buildTarget.Directory.FullName;
            startInfo.CreateNoWindow = true;
            var process = Process.Start(startInfo);
            return process != null;
        }
    }

    class DotNetMacOSBuildTarget : MacOSBuildTarget
    {
#if UNITY_EDITOR_OSX
        protected override bool IsDefaultBuildTarget => true;
#endif

        public override string GetDisplayName()
        {
            return "MacOS .NET";
        }

        public override string GetBeeTargetName()
        {
            return "macos-dotnet";
        }
    }

    class IL2CPPMacOSBuildTarget : MacOSBuildTarget
    {
        public override string GetDisplayName()
        {
            return "MacOS IL2CPP";
        }

        public override string GetBeeTargetName()
        {
            return "macos-il2cpp";
        }
    }
}
