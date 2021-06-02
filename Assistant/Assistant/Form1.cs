using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Speech.Recognition;
using System.IO;
using System.Net.NetworkInformation;

namespace Assistant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Assistant s = new Assistant();
            pictureBox3.Visible = false;
            s.getQ(richTextBox1.Text);
            richTextBox1.Text = "";
            richTextBox1.Text = s.GetR();

            


            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoiceByHints(VoiceGender.Female);
            synth.SpeakAsync(richTextBox1.Text);

        }
        
        

        private  void pictureBox1_Click_1(object sender, EventArgs e)
        {
            
        }
    }

    public class Assistant
    {
        String q;
        String r;
        public void getQ(String q2)
        {
            q = q2.ToLower();
        }
        public String GetR()
        {
            if (q.Equals("hello"))
                r = "Hi there. I'm your virtual assistant. How can I help you with?";
            if (q.Equals("my ip"))
            {
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                r = "Your IP Address is :" + myIP;
            }
            if (q.Equals("my host"))
            {
                string hostName = Dns.GetHostName();
                r = "Your host name is " + hostName;
            }
            if (q.Equals("shutdown"))
            {
                Process.Start("shutdown", "/s /t 0");
                r = "Goodbye sir";
            }
            if (q.Equals("hibernate"))
            {
                Application.SetSuspendState(PowerState.Hibernate, true, true);
                r = "Hibernate. Got it.";
            }
            if (q.Equals("sleep"))
            {
                Application.SetSuspendState(PowerState.Suspend, true, true);
                r = "Sleep. Got it.";
            }
            if (q == "open google")
            {
                r = "Sure. Opening Google Engine";
                Process.Start("chrome.exe", "http://www.google.com");

            }
            if (q == "play mozart")
            {
                r = "Sure. Playing Mozart on Youtube";
                Process.Start("microsoft-edge:https://www.youtube.com/watch?v=Rb0UmrCXxVA&t=357s");

            }
            if (q.Contains("search"))
            {
                String aux;
                aux = q.Replace("search", " ");
                r = "Sure, searching" + aux + " on Google";
                System.Diagnostics.Process.Start("http://www.google.com.au/search?q=" + aux);
            }
            if (q.Contains("ipconfig") || q.Contains("tree") || q.Contains("cmd"))
            {
                System.Diagnostics.Process.Start("CMD.exe", q);
                r = "Sure. Opening Terminal";
            }
            if (q.Equals("my pc"))
            {
                r = "Sure. Opening your pc";
                Process.Start("::{20d04fe0-3aea-1069-a2d8-08002b30309d}");
            }
            if (q.Equals("i wanna code"))
            {

                Process.Start("chrome.exe", "https://stackoverflow.com");
                Process.Start("chrome.exe", "https://github.com/");
                Process.Start("chrome.exe", "https://java.com");
                r = "Sure. Opening your favourite 3 tabs from Google.";
            }
            if (q.Equals("tell me the time") || q.Equals("tell me the date"))
            {
                DateTime now = DateTime.Now;
                r = now.ToString("F");

            }
            if (q.Contains("strada"))
            {
                r = "Sure. Opening your location.";

                Process.Start("chrome.exe", "https://www.google.ro/maps/place/" + q);
            }
            if (q.Equals("cpu usage"))
            {
                var cpuUsage = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                Thread.Sleep(1000);
                var firstCall = cpuUsage.NextValue();

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(1000);
                    r = "Your cpu usage is " + cpuUsage.NextValue() + "%";
                }

            }
            if (q.Equals("internet speed"))
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Reset();
                stopwatch.Start();

                WebClient webClient = new WebClient();
                byte[] bytes = webClient.DownloadData("http://www.google.com");

                stopwatch.Stop();

                double seconds = stopwatch.Elapsed.TotalSeconds;

                double speed = bytes.Count() / seconds;
                speed = speed;

                r = "Your speed is " + speed + " bytes per second.";
            }
            if (q.Equals("battery status"))
            {

                PowerLineStatus status = SystemInformation.PowerStatus.PowerLineStatus;
                if (status == PowerLineStatus.Offline)
                    r = "Running on Battery";
                else
                    r = "Running on Power";

            }
            if (q.Equals("wifi status"))
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                if (reply.Status == IPStatus.Success)
                {
                    r = "You are online";
                }
                else r = "You are offline";
            }
            
                return r;
        
        }
    }

}
