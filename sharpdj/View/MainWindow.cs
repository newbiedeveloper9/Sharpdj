using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using SharpDj.Models.Helpers.Updater;
using Debug = SharpDj.Models.Helpers.Debug;

namespace SharpDj.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Debug _debug;
        private bool _updated = false;
        private bool _canShow;

        public bool CanShow
        {
            get { return _canShow; }
            set
            {
                if (value == _canShow) return;
                _canShow = value;
                if (value)
                    Show();

            }
        }

        public bool Updated
        {
            get { return _updated; }
            set
            {
                if (value == _updated) return;
                _updated = value;
                if (value)
                    Show();
            }
        }

        private void Show()
        {
            if (Updated)
                if (CanShow)
                {
                    var view = new SdjMainView();
                    view.Show();
                    Close();
                }
        }


        public MainWindow()
        {
            _debug = new Debug("Updater");

            Topmost = true;
            InitializeComponent();
            MediaElement.MediaEnded += MediaElement_MediaEnded;
            Task.Factory.StartNew(InitAsync);
        }

        private async Task InitAsync()
        {
            _debug.Log("Local json");
            if (!File.Exists("config.json"))
            {
                _debug.Log("config.json doesn't exists");
                var defaultConfig = @"{
                          ""Version"": ""1.0.0"",
                          ""UpdateToken"": ""0"",
                          ""UpdateUrl"": ""http://sharpdj.cba.pl/updater.txt""
                        }";
                File.WriteAllText("config.json", defaultConfig);
                _debug.Log("Created new config.json");
            }

            var localJson = File.ReadAllText("config.json");
            var local = JsonConvert.DeserializeObject<FTPChecker>(localJson);

            _debug.Log("Ftp Json");
            var ftpJson = GetSourcePage.GetSource(local.UpdateUrl);
            var ftp = JsonConvert.DeserializeObject<FTPChecker>(ftpJson);

            if (Directory.Exists("tmp"))
                Directory.Delete("tmp", true);

            _debug.Log("Check Update Token");
            if (ftp.UpdateToken != local.UpdateToken)
            {
                _debug.Log("localToken: " + local.UpdateToken);
                _debug.Log("ftpToken: " + ftp.UpdateToken);
                _debug.Log("Updating");
                Directory.CreateDirectory("tmp");
                _debug.Log("Download");
                await DownloadAsync(ftp.ZipToUpdate, "tmp/update.zip");
                ZipFile.ExtractToDirectory("tmp/update.zip", "tmp/");
                _debug.Log("Extract");
                File.Delete("tmp/update.zip");

                Directory.CreateDirectory("backup/backup " + local.Version);
                _debug.Log("Replace");
                foreach (var file in Directory.GetFiles("tmp/"))
                {
                    var fileName = Path.GetFileName(file);

                    if (File.Exists(fileName))
                        File.Replace(file, fileName, @"backup/backup " + local.Version + "/" + fileName);
                    else
                        File.Move(file, fileName);
                }
                Directory.Delete("tmp", true);
                _debug.Log("Restarting");

                await Dispatcher.BeginInvoke((Action)delegate ()
                 {
                     App.Current.Shutdown();
                     Process.Start("SharpDj.exe");
                     Close();
                 });
            }
            _debug.Log("Closing updater");
            await Dispatcher.BeginInvoke((Action)delegate ()
             {
                 Updated = true;
             });
        }


        public async Task DownloadAsync(string url, string path)
        {
            WebClient wc = new WebClient();
            var download = wc.DownloadFileTaskAsync(url, path);
            await download;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (!CanShow)
            {
                MediaElement.Position = TimeSpan.Zero;
                MediaElement.Play();
                CanShow = true;
            }
        }
    }
}
