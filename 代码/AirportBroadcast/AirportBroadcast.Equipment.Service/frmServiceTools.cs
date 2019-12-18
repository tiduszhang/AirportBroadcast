using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportBroadcast.Equipment.Service
{
    public partial class frmServiceTools : Form
    {
        public frmServiceTools()
        {
            InitializeComponent();
        }

        private string serverName = "AirportBroadcast.Equipment.Service";

        private void btnInstall_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Install.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void btnUnInstall_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Uninstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serverName);
            serviceController.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serverName);
            if (serviceController.CanStop)
            {
                serviceController.Stop();
            }
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serverName);
            if (serviceController.CanPauseAndContinue)
            {
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    serviceController.Pause();
                }
                else if (serviceController.Status == ServiceControllerStatus.Paused)
                {
                    serviceController.Continue();
                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serverName);
            string Status = serviceController.Status.ToString();
            MessageBox.Show(String.Format("服务状态为：{0}", Status), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
