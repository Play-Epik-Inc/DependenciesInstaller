using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configurator
{
    public class BuildFailedException : Exception
    {
        public BuildFailedException(Label buildLogs, Exception ex)
        {
            MessageBox.Show($"An error occured during the build and now is canceled, Error message: {ex}", "Build Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            buildLogs.ForeColor = System.Drawing.Color.Red;
            buildLogs.AutoEllipsis = true;
            buildLogs.Text = $"{Program.GetCurrentDate()}: BUILD FAILED with this Error message: {ex}";
        }
    }
}
