namespace HuaweiARInternal
{
    using System.Text;

    /// Misc helper methods for running shell commands.
    public static class ShellHelper
    {
        public static void RunCommand(string fileName, string arguments, out string output, out string error)
        {
            using (var process = new System.Diagnostics.Process())
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo(fileName, arguments);
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardError = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;
                process.StartInfo = startInfo;

                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();
                process.OutputDataReceived += (sender, ef) => outputBuilder.AppendLine(ef.Data);
                process.ErrorDataReceived += (sender, ef) => errorBuilder.AppendLine(ef.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();

                // Trims the output strings to make comparison easier.
                output = outputBuilder.ToString().Trim();
                error = errorBuilder.ToString().Trim();
            }
        }
    }
}
