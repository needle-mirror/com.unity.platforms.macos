using System.Diagnostics;
using Unity.Build;

namespace Unity.Platforms.MacOS.Build
{
    sealed class MacOSRunInstance : IRunInstance
    {
        Process m_Process;

        public bool IsRunning => !m_Process.HasExited;

        public MacOSRunInstance(Process process)
        {
            m_Process = process;
        }

        public void Dispose()
        {
            m_Process.Dispose();
        }
    }
}
