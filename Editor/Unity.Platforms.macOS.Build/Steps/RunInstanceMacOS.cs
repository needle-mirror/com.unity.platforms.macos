using System.Diagnostics;
using Unity.Build;

namespace Unity.Platforms.MacOS.Build
{
    public sealed class RunInstanceMacOS : IRunInstance
    {
        Process m_Process;

        public bool IsRunning => !m_Process.HasExited;

        public RunInstanceMacOS(Process process)
        {
            m_Process = process;
        }

        public void Dispose()
        {
            m_Process.Dispose();
        }
    }
}
