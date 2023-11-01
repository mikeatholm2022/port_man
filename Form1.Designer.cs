using System.Threading;

namespace port_man
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_dataGridView = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_protocol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_localAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_foreignAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DnsLookup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_processName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Updated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dataGridView
            // 
            this.m_dataGridView.AllowUserToAddRows = false;
            this.m_dataGridView.AllowUserToDeleteRows = false;
            this.m_dataGridView.AllowUserToOrderColumns = true;
            this.m_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.m_protocol,
            this.m_localAddress,
            this.m_foreignAddress,
            this.DnsLookup,
            this.m_state,
            this.m_pid,
            this.m_processName,
            this.Updated});
            this.m_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dataGridView.Location = new System.Drawing.Point(0, 0);
            this.m_dataGridView.Name = "m_dataGridView";
            this.m_dataGridView.ReadOnly = true;
            this.m_dataGridView.Size = new System.Drawing.Size(733, 354);
            this.m_dataGridView.TabIndex = 0;
            // 
            // Key
            // 
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Visible = false;
            // 
            // m_protocol
            // 
            this.m_protocol.HeaderText = "Protocol";
            this.m_protocol.Name = "m_protocol";
            this.m_protocol.ReadOnly = true;
            this.m_protocol.Width = 50;
            // 
            // m_localAddress
            // 
            this.m_localAddress.HeaderText = "Local Address";
            this.m_localAddress.Name = "m_localAddress";
            this.m_localAddress.ReadOnly = true;
            this.m_localAddress.Width = 130;
            // 
            // m_foreignAddress
            // 
            this.m_foreignAddress.HeaderText = "Remote Address";
            this.m_foreignAddress.Name = "m_foreignAddress";
            this.m_foreignAddress.ReadOnly = true;
            this.m_foreignAddress.Width = 130;
            // 
            // DnsLookup
            // 
            this.DnsLookup.HeaderText = "DnsLookup";
            this.DnsLookup.Name = "DnsLookup";
            this.DnsLookup.ReadOnly = true;
            this.DnsLookup.Visible = false;
            this.DnsLookup.Width = 200;
            // 
            // m_state
            // 
            this.m_state.HeaderText = "State";
            this.m_state.Name = "m_state";
            this.m_state.ReadOnly = true;
            this.m_state.Width = 75;
            // 
            // m_pid
            // 
            this.m_pid.HeaderText = "PID";
            this.m_pid.Name = "m_pid";
            this.m_pid.ReadOnly = true;
            this.m_pid.Width = 50;
            // 
            // m_processName
            // 
            this.m_processName.HeaderText = "Process Name";
            this.m_processName.Name = "m_processName";
            this.m_processName.ReadOnly = true;
            this.m_processName.Width = 150;
            // 
            // Updated
            // 
            this.Updated.HeaderText = "Updated";
            this.Updated.Name = "Updated";
            this.Updated.ReadOnly = true;
            this.Updated.Width = 75;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 354);
            this.Controls.Add(this.m_dataGridView);
            this.Name = "MainForm";
            this.Text = "Connection Manager";
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_protocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_localAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_foreignAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn DnsLookup;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_state;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_processName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Updated;
    }
}

