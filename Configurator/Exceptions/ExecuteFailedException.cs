using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configurator.Exceptions
{
    public class ExecuteFailedException : Exception
    {
        public ExecuteFailedException(Exception ex)
        {
            MessageBox.Show($"An unknown error occured during the \"Test\" phase, Error: {ex}", "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
