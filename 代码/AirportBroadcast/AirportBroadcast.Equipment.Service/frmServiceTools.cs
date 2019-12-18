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
        /// <summary>
        /// 服务名称
        /// </summary>
        private string serverName = "AirportBroadcast.Equipment.Service";
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstall_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Install.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            MessageBox.Show("服务已注册。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnInstall_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Uninstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            MessageBox.Show("服务已注销。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serverName);
            serviceController.Start();
            MessageBox.Show("服务已启动。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serverName);
            if (serviceController.CanStop)
            {
                serviceController.Stop();
            }
            MessageBox.Show("服务已停止。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 暂停/恢复服务---好像没有用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSP_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serverName);
            if (serviceController.CanPauseAndContinue)
            {
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    serviceController.Pause();
                    MessageBox.Show("服务已暂停。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (serviceController.Status == ServiceControllerStatus.Paused)
                {
                    serviceController.Continue();
                    MessageBox.Show("服务已恢复。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// 检查服务状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serverName);
            string Status = serviceController.Status.ToString();
            MessageBox.Show(String.Format("服务状态为：{0}", Status), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
