using System.IO;
using Unity.Build;

namespace Unity.Platforms.MacOS.Build
{
    // TODO: Don't make this class public until platform team reviews the fields inside, there is a concern that FileInfo is not a correct field
    //       Due to fact build can produce multiple files instead of one
    sealed class BuildArtifactsMacOS : IBuildArtifact
    {
        public FileInfo OutputTargetFile;
    }
}
