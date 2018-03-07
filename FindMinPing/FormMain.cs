using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;

namespace FindMinPing
{
    public partial class FormMain : Form
    {
        private string m_SiteUrl = "https://free-ss.site/";
        public FormMain()
        {
            InitializeComponent();
            txtAddress.Text = m_SiteUrl;
            this.txtMail.Text = "ss@rohankdd.com";
            ShowControlInPanel(this.txtList);
        }

        private void ShowControlInPanel(Control control)
        {
            this.panel1.Controls.Clear();
            control.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(control);
            control.Select();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1060, 704);
            try
            {
                Clipboard.SetText(txtAddress.Text);
            }
            catch (Exception) { }
        }

        private void dgvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            try
            {
                Clipboard.SetText(dgvResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show("Copy to ClipBoard Error!");
            }
        }

        private void txtList_DoubleClick(object sender, EventArgs e)
        {
            txtList.Text = Clipboard.GetText();
        }

        private void txtList_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtList.Text))
            {
                btnResolve.PerformClick();
            }
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtList.Text)) return;
            DataTable dt = new DataTable();
            var lines = txtList.Text.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            dt.Columns.Add("HeartStar", typeof(int));
            dt.Columns.Add("IP");
            dt.Columns.Add("Port");
            dt.Columns.Add("Password");
            dt.Columns.Add("Method");
            dt.Columns.Add("Time");
            dt.Columns.Add("Location");
            int tmp;
            for (int i = 1; i < lines.Length; i++)
            {
                var rowCells = lines[i].Split(new string[] {"\t"}, StringSplitOptions.RemoveEmptyEntries);
                if(!int.TryParse(rowCells[0], out tmp))
                {
                    break;
                }
                DataRow row = dt.NewRow();
                int colIndex = 0;
                foreach (var cell in rowCells)
                {
                    row[colIndex++] = cell;
                }
                dt.Rows.Add(row);
            }

            dt.Columns.Add("PingResult");
            dt.Columns.Add("Min", typeof(int));
            dt.Columns.Add("Max",typeof(int));
            dt.Columns.Add("Avg", typeof(int));
            ShowControlInPanel(this.dgvResult);
            DataGridBind(dt);
        }

        private void btnPingSelected_Click(object sender, EventArgs e)
        {
            if (dgvResult.DataSource == null || dgvResult.SelectedRows.Count == 0) return;
            PingSelectedRows(dgvResult.SelectedRows);
        }

        private ConcurrentQueue<WorkerParam> _workerParams;
        private void btnPingAll_Click(object sender, EventArgs e)
        {
            if (dgvResult.DataSource == null || dgvResult.RowCount == 0) return;
            PingSelectedRows(dgvResult.Rows);
        }

        private void PingSelectedRows(IList rows)
        {
            this.btnPingAll.Enabled = false;
            this.btnPingSelected.Enabled = false;
            dgvResult.Select();
            _workerParams = new ConcurrentQueue<WorkerParam>();
            foreach (DataGridViewRow row in rows)
            {
                _workerParams.Enqueue(new WorkerParam()
                {
                    Address = row.Cells["IP"].Value.ToString(),
                    RowIndex = row.Index
                });
            }

            for (int i = 0; i < 10; i++)
            {
                if (_workerParams.IsEmpty) break;
                _workerParams.TryDequeue(out var param);
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += WorkerOnDoWork;
                worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
                worker.RunWorkerAsync(param);
            }
        }

        private void EnableButton()
        {
            if (btnPingAll.Enabled) return;
            btnPingAll.Enabled = true;
            btnPingSelected.Enabled = true;
            MessageBox.Show("Ping全部完成");
        }

        private void UpdateRow(WorkerResult result)
        {
            PingUtil.AnalyzePingResult(result.Times, out var min, out var max, out var avg);
            var row = dgvResult.Rows[result.RowIndex];
            row.Cells["PingResult"].Value = string.Join(";", result.Times);
            row.Cells["Min"].Value = min;
            row.Cells["Max"].Value = max;
            row.Cells["Avg"].Value = avg;
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            WorkerResult result = runWorkerCompletedEventArgs.Result as WorkerResult;
            if (dgvResult.InvokeRequired)
            {
                dgvResult.Invoke(new DelegateUpdateRow(UpdateRow), result);
            }
            else
            {
                UpdateRow(result);
            }

            _workerParams.TryDequeue(out var param);
            if (param == null)
            {
                if (btnResolve.InvokeRequired)
                {
                    btnResolve.Invoke(new DelegateEnableButton(EnableButton));
                }
                else
                {
                    EnableButton();
                }
                return;
            }
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.RunWorkerAsync(param);
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            WorkerParam param = doWorkEventArgs.Argument as WorkerParam;
            doWorkEventArgs.Result = new WorkerResult()
            {
                Times = PingUtil.Ping(param.Address),
                RowIndex = param.RowIndex
            };
        }

        private void btnExportJson_Click(object sender, EventArgs e)
        {
            //if (dgvResult.SelectedRows.Count == 0) return;
            OpenFileDialog dialog = new OpenFileDialog()
            {
                InitialDirectory = @"Z:\SSR",
                Filter = "Json文件|*.json",
                RestoreDirectory = true,
                FilterIndex = 1,
                FileName = "gui-config.json"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                JsonManipulator jManip = new JsonManipulator(dialog.FileName);
                foreach (DataGridViewRow row in dgvResult.Rows)
                {
                    if (!row.Selected || !row.Visible) continue;
                    var remark = row.Cells["Location"].Value.ToString();
                    SS_GUI_Config config = new SS_GUI_Config()
                    {
                        remarks = remark,
                        remarks_base64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(remark)),
                        server = row.Cells["IP"].Value.ToString(),
                        server_port = row.Cells["Port"].Value.ToString(),
                        password = row.Cells["Password"].Value.ToString(),
                        method = row.Cells["Method"].Value.ToString()
                    };
                    jManip.AddConfig(config);
                }
                jManip.WriteJsonToFile();
                MessageBox.Show("已成功保存到文件：" + dialog.FileName, "保存成功");
            }
        }

        private void btnHideTimeout_Click(object sender, EventArgs e)
        {
            bool visible = true;
            if (btnHideTimeout.Text == "隐藏TimeOut")
            {
                visible = false;
                btnHideTimeout.Text = "显示所有";
            }
            else
            {
                btnHideTimeout.Text = "隐藏TimeOut";
            }
            CurrencyManager myCM = (CurrencyManager)BindingContext[dgvResult.DataSource];
            myCM.SuspendBinding();//挂起数据绑定
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                if (row.Cells["PingResult"].Value.ToString().Contains("TimeOut"))
                {
                    row.Visible = visible;
                }
            }
            myCM.ResumeBinding();//恢复数据绑定
        }

        #region Inner Typedefs
        private delegate void DelegateEnableButton();//定义委托

        private delegate void DelegateUpdateRow(WorkerResult result);//定义委托

        class WorkerParam
        {
            public string Address;
            public int RowIndex;
        }

        class WorkerResult
        {
            public IList<string> Times;
            public int RowIndex;
        }

        #endregion

        //The SortCompare event does not occur when the DataSource property is set or when the VirtualMode property value is true...........
        private void dgvResult_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            HashSet<string> colNames = new HashSet<string>();
            colNames.Add("Min");
            colNames.Add("Max");
            colNames.Add("HeartStar");
            colNames.Add("Agv");
            if (colNames.Contains(e.Column.Name))
            {
                e.SortResult = Convert.ToInt16(e.CellValue1) - Convert.ToInt16(e.CellValue2);
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }

        private void txt_DoubleClick(object sender, EventArgs e)
        {
            //调用系统默认的浏览器   
            System.Diagnostics.Process.Start(m_SiteUrl);
            return;
            TextBox obj = sender as TextBox;
            try
            {
                Clipboard.SetText(obj.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("剪切板操作失败");
            }
        }

        private void btnShowTextBox_Click(object sender, EventArgs e)
        {
            ShowControlInPanel(this.txtList);
        }

        private void DataGridBind(DataTable dt)
        {
            this.dgvResult.DataSource = dt;

            int displayIndex = 0;
            dgvResult.Columns["PingResult"].DisplayIndex = displayIndex++;
            dgvResult.Columns["Min"].DisplayIndex = displayIndex++;
            dgvResult.Columns["Max"].DisplayIndex = displayIndex++;
            dgvResult.Columns["Avg"].DisplayIndex = displayIndex++;

            //dgvResult.Columns["HeartStar"].SortMode = DataGridViewColumnSortMode.Programmatic;
            //dgvResult.Columns["Min"].SortMode = DataGridViewColumnSortMode.Programmatic;
            //dgvResult.Columns["Max"].SortMode = DataGridViewColumnSortMode.Programmatic;
            //dgvResult.Columns["Avg"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        #region Code Commented
        private void dgvResult_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
            if (e.ColumnIndex <= 0) return;
            //取得点击列的索引
            int nColumnIndex = e.ColumnIndex;
            if (dgvResult.Columns[nColumnIndex].SortMode != DataGridViewColumnSortMode.Programmatic)
            {
                return;
            }
            switch (dgvResult.Columns[nColumnIndex].HeaderCell.SortGlyphDirection)
            {
                case SortOrder.None:
                case SortOrder.Ascending:
                    //在这里加入排序的逻辑
                    DataView view = new DataView(this.dgvResult.DataSource as DataTable);
                    view.Sort = dgvResult.Columns[nColumnIndex].Name + " ASC";

                    //设置列标题的状体 
                    dgvResult.Columns[nColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    break;
                default:
                    dgvResult.Columns[nColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    break;
            }
        }
        #endregion
    }
}
