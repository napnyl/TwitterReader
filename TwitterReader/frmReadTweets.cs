using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TwitterReader
{
    public partial class frmReadTweets : Form
    {
        frmMain mainForm;
        DateTime beginTime;

        public frmReadTweets(frmMain frmMain)
        {
            InitializeComponent();
            mainForm = frmMain;

            this.lblReadTweets.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblReadTweets.Text = string.Empty;
        }

        public void SetTextForLabel(string[] allText)
        {
            for (int i = 0; i < allText.Count(); i++)
            {
                if (lblReadTweets.Text.Contains(allText[i]))
                {
                    allText = allText.Where(val => val != allText[i]).ToArray();
                    i--;
                }
            }

            if (allText.Count() > 0 && beginTime > (DateTime.Now.AddSeconds(-mainForm.maxInterval)))
            {
                DateTime currentTime = DateTime.Now;
                int minutes = (currentTime - beginTime).Minutes;
                int seconds = (currentTime - beginTime).Seconds;
                int totalMiliseconds = ((minutes < 0 ? 0 : minutes * 60) + (seconds < 0 ? 0 : seconds)) * 1000;

                if (mainForm.tmMain.Interval != mainForm.maxInterval)
                {
                    if (totalMiliseconds < mainForm.maxInterval)
                        mainForm.SetTimerInterval(totalMiliseconds);
                    else
                        mainForm.SetTimerInterval(mainForm.maxInterval);
                }
                else
                    mainForm.SetTimerInterval(mainForm.minInterval);
            }

            foreach (var line in allText)
            {
                if (allText.Count() > 1 && Array.IndexOf(allText, line) < (allText.Count() - 1))
                {
                    this.lblReadTweets.Text += line;
                    this.lblReadTweets.Text += Environment.NewLine;
                }
                else if (allText.Count() == 1)
                {
                    this.lblReadTweets.Text += Environment.NewLine;
                    this.lblReadTweets.Text += line;
                }
                else
                    this.lblReadTweets.Text += line;

                beginTime = DateTime.Now;
            }
        }

        public void ClearTextForLabel()
        {
            lblReadTweets.Text = string.Empty;
        }
    }
}
