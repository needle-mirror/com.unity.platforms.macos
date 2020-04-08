using System.IO;
using Unity.Build;

namespace Unity.Platforms.MacOS.Build
{
    sealed class MacOSArtifact : IBuildArtifact
    {
        public FileInfo OutputTargetFile;
    }
}
