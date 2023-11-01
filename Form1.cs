using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;

namespace port_man
{
    public partial class MainForm : Form
    {
        private Timer m_timer;
        NetstatHelper m_nsh;

        public MainForm()
        {
            InitializeComponent();

            // Initialize the timer
            m_timer = new Timer();
            m_timer.Interval = 5000; // Set the interval to 5 second (5000 milliseconds)
            m_timer.Enabled = true;  // Start the timer
            m_timer.Tick += OnTimerUpdate; // Attach the event handler

            m_dataGridView.Rows.Clear();

            m_nsh = new NetstatHelper();
        }

        private void OnTimerUpdate(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in m_dataGridView.Rows)
                row.Cells["Updated"].Value = "";

            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("HH:mm:ss");
            
            List<EstablishedConnectionData> data = m_nsh.RetrieveTcpInformation();
            foreach (var details in data)
            {
                details.PrintData();
                string key = details.GetKey();
                bool found = false;

                foreach (DataGridViewRow row in m_dataGridView.Rows)
                {
                    string row_key = row.Cells["Key"].Value.ToString();
                    if (row_key != null && row_key == key)
                    {
                        found = true;
                        row.Cells["Updated"].Value = formattedDateTime;
                        break;
                    }
                }

                if (!found)
                    m_dataGridView.Rows.Add(key, "TCP", details.GetLocalAddress(), details.GetRemoteAddress(), details.m_dns, "Established", details.m_pid.ToString(), details.m_process_name, formattedDateTime);
            }

            for (int i = m_dataGridView.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = m_dataGridView.Rows[i];
                if (row.Cells["Updated"].Value.ToString() == "")
                    m_dataGridView.Rows.Remove(row);
            }

            Console.WriteLine("Timer triggered!");
        }

    }
}
