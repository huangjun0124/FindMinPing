namespace FindMinPing
{
    partial class FormMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.txtList = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnResolve = new System.Windows.Forms.Button();
            this.btnPingAll = new System.Windows.Forms.Button();
            this.btnPingSelected = new System.Windows.Forms.Button();
            this.btnExportJson = new System.Windows.Forms.Button();
            this.btnHideTimeout = new System.Windows.Forms.Button();
            this.btnShowTextBox = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel1.Controls.Add(this.txtAddress, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMail, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1109, 595);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(3, 3);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(394, 25);
            this.txtAddress.TabIndex = 0;
            this.txtAddress.DoubleClick += new System.EventHandler(this.txt_DoubleClick);
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(403, 3);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(210, 25);
            this.txtMail.TabIndex = 1;
            this.txtMail.DoubleClick += new System.EventHandler(this.txt_DoubleClick);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.dgvResult);
            this.panel1.Controls.Add(this.txtList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(969, 558);
            this.panel1.TabIndex = 4;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResult.Location = new System.Drawing.Point(556, 8);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowTemplate.Height = 27;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(462, 433);
            this.dgvResult.TabIndex = 4;
            this.dgvResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellDoubleClick);
            this.dgvResult.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvResult_ColumnHeaderMouseClick);
            this.dgvResult.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvResult_SortCompare);
            // 
            // txtList
            // 
            this.txtList.Location = new System.Drawing.Point(8, 8);
            this.txtList.Name = "txtList";
            this.txtList.Size = new System.Drawing.Size(527, 445);
            this.txtList.TabIndex = 3;
            this.txtList.Text = "";
            this.txtList.TextChanged += new System.EventHandler(this.txtList_TextChanged);
            this.txtList.DoubleClick += new System.EventHandler(this.txtList_DoubleClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnShowTextBox);
            this.flowLayoutPanel1.Controls.Add(this.btnResolve);
            this.flowLayoutPanel1.Controls.Add(this.btnPingAll);
            this.flowLayoutPanel1.Controls.Add(this.btnPingSelected);
            this.flowLayoutPanel1.Controls.Add(this.btnExportJson);
            this.flowLayoutPanel1.Controls.Add(this.btnHideTimeout);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(978, 34);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(128, 558);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btnResolve
            // 
            this.btnResolve.AutoSize = true;
            this.btnResolve.Location = new System.Drawing.Point(3, 46);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(126, 37);
            this.btnResolve.TabIndex = 3;
            this.btnResolve.Text = "分析原文本";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // btnPingAll
            // 
            this.btnPingAll.AutoSize = true;
            this.btnPingAll.Location = new System.Drawing.Point(3, 89);
            this.btnPingAll.Name = "btnPingAll";
            this.btnPingAll.Size = new System.Drawing.Size(126, 37);
            this.btnPingAll.TabIndex = 3;
            this.btnPingAll.Text = "Ping所有的";
            this.btnPingAll.UseVisualStyleBackColor = true;
            this.btnPingAll.Click += new System.EventHandler(this.btnPingAll_Click);
            // 
            // btnPingSelected
            // 
            this.btnPingSelected.AutoSize = true;
            this.btnPingSelected.Location = new System.Drawing.Point(3, 132);
            this.btnPingSelected.Name = "btnPingSelected";
            this.btnPingSelected.Size = new System.Drawing.Size(126, 37);
            this.btnPingSelected.TabIndex = 3;
            this.btnPingSelected.Text = "Ping选中的";
            this.btnPingSelected.UseVisualStyleBackColor = true;
            this.btnPingSelected.Click += new System.EventHandler(this.btnPingSelected_Click);
            // 
            // btnExportJson
            // 
            this.btnExportJson.AutoSize = true;
            this.btnExportJson.Location = new System.Drawing.Point(3, 175);
            this.btnExportJson.Name = "btnExportJson";
            this.btnExportJson.Size = new System.Drawing.Size(126, 37);
            this.btnExportJson.TabIndex = 3;
            this.btnExportJson.Text = "选中行生成json";
            this.btnExportJson.UseVisualStyleBackColor = true;
            this.btnExportJson.Click += new System.EventHandler(this.btnExportJson_Click);
            // 
            // btnHideTimeout
            // 
            this.btnHideTimeout.AutoSize = true;
            this.btnHideTimeout.Location = new System.Drawing.Point(3, 218);
            this.btnHideTimeout.Name = "btnHideTimeout";
            this.btnHideTimeout.Size = new System.Drawing.Size(126, 37);
            this.btnHideTimeout.TabIndex = 3;
            this.btnHideTimeout.Text = "隐藏TimeOut";
            this.btnHideTimeout.UseVisualStyleBackColor = true;
            this.btnHideTimeout.Click += new System.EventHandler(this.btnHideTimeout_Click);
            // 
            // btnShowTextBox
            // 
            this.btnShowTextBox.AutoSize = true;
            this.btnShowTextBox.Location = new System.Drawing.Point(3, 3);
            this.btnShowTextBox.Name = "btnShowTextBox";
            this.btnShowTextBox.Size = new System.Drawing.Size(126, 37);
            this.btnShowTextBox.TabIndex = 3;
            this.btnShowTextBox.Text = "显示文本框";
            this.btnShowTextBox.UseVisualStyleBackColor = true;
            this.btnShowTextBox.Click += new System.EventHandler(this.btnShowTextBox_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 595);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "找到ping值最小的IP";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.RichTextBox txtList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.Button btnPingSelected;
        private System.Windows.Forms.Button btnPingAll;
        private System.Windows.Forms.Button btnExportJson;
        private System.Windows.Forms.Button btnHideTimeout;
        private System.Windows.Forms.Button btnShowTextBox;
    }
}

