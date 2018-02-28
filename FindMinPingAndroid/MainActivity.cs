using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using CommonLibrary;

namespace FindMinPingAndroid
{
    [Activity(Icon = "@drawable/icon",Label = "FindMinPingAndroid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button btnCopyAddress = FindViewById<Button>(Resource.Id.btnCopyAddress);
            btnCopyAddress.Click += delegate
            {
                var url = "https://free-ss.site/";
                //for copy
                var clipboard = (ClipboardManager)GetSystemService(ClipboardService);
                var clip = ClipData.NewPlainText("SS", url);
                clipboard.PrimaryClip = clip;
                Toast.MakeText(this, "复制网址成功！", ToastLength.Long).Show();

                // Open link
                var uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };
            btnCopyAddress.PerformClick();

            Button btnPaste = FindViewById<Button>(Resource.Id.btnPaste);
            btnPaste.Click += delegate
            {
                EditText tv = FindViewById<EditText>(Resource.Id.editText1);
                tv.Text = GetClipboardText();
            };

            Button btnPing = FindViewById<Button>(Resource.Id.btnAnalyze);
            btnPing.Click += delegate
            {
                EditText tv = FindViewById<EditText>(Resource.Id.editText1);
                if (string.IsNullOrEmpty(tv.Text)) return;
                Toast.MakeText(this, "开始测试，请耐心等待结果", ToastLength.Long).Show();
                PingByThread(tv.Text);
                tv.Text = "Ping测试运行中，请等待。。。";
            };
        }

        private string GetClipboardText()
        {
            //  paste it
            var clipboard = (ClipboardManager)GetSystemService(ClipboardService);
            var pasteData = "";

            if (!(clipboard.HasPrimaryClip))
            {
                // If it does contain data, decide if you can handle the data.
            }
            else
            {
                try
                {
                    //since the clipboard contains plain text or html
                    var item = clipboard.PrimaryClip.GetItemAt(0);
                    // Gets the clipboard as text.
                    pasteData = item.Text;
                }
                catch (Exception e)
                {
                    pasteData = "数据格式有误";
                }
            }
            return pasteData;
        }

        public void PingByThread(string text)
        {
            new Thread(() =>
            {
                //this.RunOnUiThread(() =>
                {
                    var surpportedMethods = new string[] { "table","rc4","rc4-md5","rc4-md5-6",
                        "aes-128-cfb","aes-192-cfb","aes-256-cfb","aes-128-ctr","aes-192-ctr",
                        "aes-256-ctr","bf-cfb","camellia-128-cfb","camellia-192-cfb","camellia-256-cfb",
                        "salsa20","chacha20","chacha20-ietf"};
                    IList<PingRetItem> list = new List<PingRetItem>();
                    var lines = text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var rowCells = lines[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                        if (!int.TryParse(rowCells[0], out var tmp))
                        {
                            if (i == 0) continue; // start or end
                            else break;
                        }
                        if (!surpportedMethods.Contains(rowCells[4])) continue;
                        IList<string> times = PingUtil.Ping(rowCells[1], 3);
                        PingUtil.AnalyzePingResult(times, out var min, out var max, out var avg);
                        list.Add(new PingRetItem()
                        {
                            Avg = avg,
                            IP = rowCells[1],
                            Port = rowCells[2],
                            Password = rowCells[3],
                            Method = rowCells[4],
                            PingResult = string.Join(",", times)
                        });
                        if (list.Count >= 25) break;
                    }
                    this.RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "Ping测试结束，请查看avg靠前的10个结果列表", ToastLength.Long).Show();
                        EditText tv = FindViewById<EditText>(Resource.Id.editText1);
                        tv.Text = string.Join("\n", list.OrderBy(p => p.Avg).Select(GetSSLinkForServer));
                        //for copy
                        var clipboard = (ClipboardManager)GetSystemService(ClipboardService);
                        var clip = ClipData.NewPlainText("SS", tv.Text);
                        clipboard.PrimaryClip = clip;
                    });
                }; //);

            }).Start();

        }

        private string GetSSLinkForServer(PingRetItem item)
        {
            string parts = item.Method + ":" + item.Password + "@" + item.IP + ":" + item.Port;
            string base64 = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(parts)).Replace("=", "");
            return "ss://" + base64;
        }

        class PingRetItem
        {
            public string IP;
            public string Port;
            public string Password;
            public string Method;
            public int Avg;
            public string PingResult;

            public override string ToString()
            {
                return PingResult + " avg: " + Avg + " ip: " + IP + " port: " + Port + " pwd: " + Password + " " + Method + "\n";
            }
        }
       
    }
}

