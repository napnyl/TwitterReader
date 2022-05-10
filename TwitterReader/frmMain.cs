using System;
using System.Linq;
using System.Windows.Forms;
using TweetSharp;

namespace TwitterReader
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        frmReadTweets frmTweet;
        public int maxTime = 48; //Intervalo máximo de lectura de tweets, segundos.
        public int minTime = 5; //Intervalo mínimo de lectura de tweets, segundos.
        public int maxInterval, minInterval;

        //Credenciales
        //Link: https://developer.twitter.com/en/docs/twitter-api/getting-started/getting-access-to-the-twitter-api
        string apiKey ="";
        string apiKeySecret = "";
        string accessToken = "";
        string accessTokenSecret = "";

        public frmMain()
        {
            InitializeComponent();
            maxInterval = maxTime * 1000;
            minInterval = minTime * 1000;
            tmMain.Interval = minInterval;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        public void InitializeTimer(bool state)
        {
            tmMain.Enabled = state;
        }

        private TwitterService ConnectTwitter()
        {
            //Realiza conexión con twitter.
            TwitterService service = new TwitterService(apiKey, apiKeySecret);
            service.AuthenticateWith(accessToken, accessTokenSecret);
            return service;
        }

        private void btnConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmTweet == null)
            {
                frmTweet = new frmReadTweets(this);
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
            }

            if (!frmTweet.Visible)
            {
                frmTweet = new frmReadTweets(this);
                frmTweet.TopLevel = false;
                pnlMain.Controls.Add(frmTweet);
                frmTweet.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmTweet.Dock = DockStyle.Fill;
                frmTweet.Show();

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;

                //Realiza conexión con twitter.
                var service = ConnectTwitter();
                var tweets = service.ListTweetsMentioningMe(new ListTweetsMentioningMeOptions());
                int newLines = tweets.Count();
                string[] stringLines = new string[newLines];

                int count = 0;
                foreach (var tweet in tweets)
                {
                    stringLines[count++] = tweet.User.ScreenName == null ? string.Empty + ":" + tweet.Text : tweet.User.ScreenName + ":" + tweet.Text + " - " + tweet.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");
                }

                TwitterRateLimitStatus rate = service.Response.RateLimitStatus;

                //Se da la vuelta al array inicial.
                Array.Reverse(stringLines);

                //Leer tweets iniciales.
                frmTweet.SetTextForLabel(stringLines);

                //Se inicializa timer para lectura de nuevos tweets.
                InitializeTimer(true);
            }
        }

        private void btnDisconnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Detiene timer de lectura de nuevos tweets.
            InitializeTimer(false);

            //Limpia mensajes de label.
            frmTweet.ClearTextForLabel();

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;

            //Esconde ventana.
            if (frmTweet.Visible)
                frmTweet.Hide();
        }

        public void SetTimerInterval(int time)
        {
            tmMain.Interval = time;
        }



        private void tmMain_Tick(object sender, EventArgs e)
        {
            if (frmTweet != null && frmTweet.Visible)
            {
                //Realiza conexión con twitter.
                var service = ConnectTwitter();
                var tweets = service.ListTweetsMentioningMe(new ListTweetsMentioningMeOptions());
                if (tweets != null)
                {
                    int newLines = tweets.Count();
                    string[] stringLines = new string[newLines];

                    int count = 0;
                    foreach (var tweet in tweets)
                    {
                        stringLines[count++] = tweet.User.ScreenName == null ? string.Empty + ":" + tweet.Text : tweet.User.ScreenName + ":" + tweet.Text + " - " + tweet.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss");
                    }

                    frmTweet.SetTextForLabel(stringLines);
                    frmTweet.scrollRead.VerticalScroll.Value = frmTweet.scrollRead.VerticalScroll.Maximum;
                }
                TwitterRateLimitStatus rate = service.Response.RateLimitStatus;
                DateTime resetTime = rate.ResetTime;
                int remainingHits = rate.RemainingHits;
                int minutes = (resetTime - DateTime.Now).Minutes;
                int seconds = (resetTime - DateTime.Now).Seconds;
                int totalMiliseconds = ((minutes < 0 ? 0 : minutes * 60) + (seconds < 0 ? 0 : seconds) + minTime) * 1000;
                if (remainingHits == 0)
                    tmMain.Interval = totalMiliseconds;
            }
        }
    }
}
