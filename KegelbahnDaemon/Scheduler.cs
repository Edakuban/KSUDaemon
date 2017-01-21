using KegelbahnSpielberichtUploadDaemon.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace KegelbahnSpielberichtUploadDaemon
{
    public partial class Scheduler : ServiceBase
    {
        private Timer Timer = null;
        private Config Config = null;

        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("Started");
            Config = new Config(AppDomain.CurrentDomain.BaseDirectory + "\\Config.xml");
            Logger.Log("Config loaded");

            //copy imediatly
            Logger.Log("Progress");
            CopyMagic.DoMagic(Config);

            //start timer
            Timer = new Timer(Config.Interval);
            Timer.Elapsed += new ElapsedEventHandler(TimerTick);
            Timer.Enabled = true;
        }

        protected override void OnStop()
        {
            Logger.Log("Stopped");
            Timer.Enabled = false;
        }

        private void TimerTick(object sender, ElapsedEventArgs args)
        {
            Logger.Log("Progress");
            CopyMagic.DoMagic(Config);
        }
    }
}
