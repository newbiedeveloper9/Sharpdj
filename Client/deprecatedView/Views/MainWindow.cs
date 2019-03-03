using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using SharpDj.Logic.Helpers.Updater;
using Debug = Communication.Shared.Debug;

namespace SharpDj.View.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _canShow;
        private bool _updated;


        public MainWindow()
        {
            Topmost = true;
            InitializeComponent();
            MediaElement.MediaEnded += MediaElement_MediaEnded;
            Task.Factory.StartNew(InitAsync);
        }

        public bool CanShow
        {
            get => _canShow;
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
            get => _updated;
            set
            {
                if (value == _updated) return;
                _updated = value;
                if (value)
                    Show();
            }
        }

        private new void Show()
        {
            if (Updated)
                if (CanShow)
                {
                    var view = new SdjMainView();
                    view.Show();
                    Close();
                }
        }

        private async Task InitAsync()
        {
            var debug = new Debug("Updater");

            debug.Log("Local json");
            if (!File.Exists("config.json"))
            {
                debug.Log("config.json doesn't exists");
                var defaultConfig = @"{
                          ""Version"": ""1.0.0"",
                          ""UpdateToken"": ""0"",
                          ""UpdateUrl"": ""http://sharpdj.cba.pl/updater.txt""
                        }";
                File.WriteAllText("config.json", defaultConfig);
                debug.Log("Created new config.json");
            }

            var localJson = File.ReadAllText("config.json");
            var local = JsonConvert.DeserializeObject<FTPModel>(localJson);

            debug.Log("Ftp Json");
            var ftpJson = GetSourcePage.GetSource(local.UpdateUrl);
            var ftp = JsonConvert.DeserializeObject<FTPModel>(ftpJson);

            if (Directory.Exists("tmp"))
                Directory.Delete("tmp", true);

            debug.Log("Check Update Token");
            if (ftp.UpdateToken != local.UpdateToken)
            {
                debug.Log("localToken: " + local.UpdateToken);
                debug.Log("ftpToken: " + ftp.UpdateToken);
                debug.Log("Updating");
                Directory.CreateDirectory("tmp");
                debug.Log("Download");
                await DownloadAsync(ftp.ZipToUpdate, "tmp/update.zip");
                ZipFile.ExtractToDirectory("tmp/update.zip", "tmp/");
                debug.Log("Extract");
                File.Delete("tmp/update.zip");

                Directory.CreateDirectory("backup/backup " + local.Version);
                debug.Log("Replace");
                foreach (var file in Directory.GetFiles("tmp/"))
                {
                    var fileName = Path.GetFileName(file);

                    if (File.Exists(fileName))
                        File.Replace(file, fileName, @"backup/backup " + local.Version + "/" + fileName);
                    else
                        File.Move(file, fileName);
                }

                Directory.Delete("tmp", true);
                debug.Log("Restarting");

                await Dispatcher.BeginInvoke((Action) delegate
                {
                    Application.Current.Shutdown();
                    Process.Start("SharpDj.exe");
                    Close();
                });
            }

            debug.Log("Closing updater");
            await Dispatcher.BeginInvoke((Action) delegate { Updated = true; });
        }


        private async Task DownloadAsync(string url, string path)
        {
            var wc = new WebClient();
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