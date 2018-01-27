using System;
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

namespace FindMinPing
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            txtAddress.Text = "https://free-ss.site/";
            this.txtMail.Text = "ss@rohankdd.com";
            this.panel1.Controls.Clear();
            this.txtList.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(txtList);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1060, 704);
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
            btnResolve.PerformClick();
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtList.Text)) return;
            DataTable dt = new DataTable();
            var lines = txtList.Text.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            dt.Columns.Add("HeartStar");
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
            dt.Columns.Add("Min");
            dt.Columns.Add("Max");
            dt.Columns.Add("Avg");
            this.dgvResult.DataSource = dt;
            this.dgvResult.Dock = DockStyle.Fill;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(dgvResult);
            int displayIndex = 0;
            dgvResult.Columns["PingResult"].DisplayIndex = displayIndex++;
            dgvResult.Columns["Min"].DisplayIndex = displayIndex++;
            dgvResult.Columns["Max"].DisplayIndex = displayIndex++;
            dgvResult.Columns["Avg"].DisplayIndex = displayIndex++;
        }

        private void btnPingSelected_Click(object sender, EventArgs e)
        {
            if (dgvResult.DataSource == null || dgvResult.SelectedRows.Count == 0) return;
            this.Cursor = Cursors.WaitCursor;
            string address = dgvResult.SelectedRows[0].Cells["IP"].Value.ToString();
            IList<string> times = PingUtil.Ping(address);
            AnalyzePingResult(times, out var min, out var max, out var avg);
            MessageBox.Show($"{string.Join(",", times)}\nMin:{min}\nMax:{max}\nAvg:{avg}", "ping result :" + address);
            this.Cursor = Cursors.Default;
        }

        private void AnalyzePingResult(IList<string> results, out int min, out int max, out int avg)
        {
            min = int.MaxValue;
            max = avg = 0;
            int avgCnt = 0;
            foreach (string result in results)
            {
                if (result != "Fail" && result != "TimeOut")
                {
                    var tmp = int.Parse(result);
                    avgCnt++;
                    avg += tmp;
                    if (tmp > max)
                    {
                        max = tmp;
                    }
                    if (tmp < min)
                    {
                        min = tmp;
                    }
                }
            }

            if (avgCnt > 0)
            {
                avg /= avgCnt;
            }
        }

        private ConcurrentQueue<WorkerParam> _workerParams;
        private void btnPingAll_Click(object sender, EventArgs e)
        {
            if (dgvResult.DataSource == null || dgvResult.SelectedRows.Count == 0) return;
            this.btnPingAll.Enabled = false;
            dgvResult.Select();
            _workerParams = new ConcurrentQueue<WorkerParam>();
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                _workerParams.Enqueue( new WorkerParam()
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
            MessageBox.Show("Ping全部完成");
        }

        private void UpdateRow(WorkerResult result)
        {
            AnalyzePingResult(result.Times, out var min, out var max, out var avg);
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
            OpenFileDialog dialog = new OpenFileDialog();


            foreach (DataGridViewRow row in dgvResult.SelectedRows)
            {

            }
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
    }
}
