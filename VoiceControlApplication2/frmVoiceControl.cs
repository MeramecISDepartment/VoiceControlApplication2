//This code references Johnny Manson's YouTube video (C# Development Tutorial | Speech Synthesis):
//https://www.youtube.com/watch?v=gkTiKMSLOHY

//These are namespaces and not classes (methods?)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace VoiceControlApplication2
{
    public partial class frmVoiceControl : Form
    {
        public frmVoiceControl()
        {
            InitializeComponent();
        }

        SpeechRecognitionEngine speechRecEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices voiceCommands = new Choices();
            voiceCommands.Add(new string[] { "Say hello.", "Print my name.", "Speak selected text.", "Open Google Calendar.", "Open Microsoft Word.", "Open my email.", "Say hi.", "Sure.", "Exit."});
            GrammarBuilder gramBuilder = new GrammarBuilder();
            gramBuilder.Append(voiceCommands);
            Grammar grammer = new Grammar(gramBuilder);

            //speechRecEngine.LoadGrammar
            //speechRecEngine.AsyncGrammar(grammer);
            speechRecEngine.LoadGrammarAsync(grammer);
            speechRecEngine.SetInputToDefaultAudioDevice();
            speechRecEngine.SpeechRecognized += speechRecEngine_SpeechRecognized;
        }

        void speechRecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                //case "Say hello.":

                //    //MessageBox.Show("Hello Tyler.  How are you?");
                //    //builder.StartStyle(new PromptStyle(PromptEmphasis));
                //    //builder.StartStyle(new PromptStyle(PromptRate));
                //    //builder.StartStyle(new PromptStyle(PromptVolume));
                //    //builder.StartSentence();

                //    //synthesizer.SpeakAsync("Hello Tyler.  How are you?");

                //    bool prompted = false;

                //    while (prompted == false)
                //    {
                //        PromptBuilder builder = new PromptBuilder();

                //        builder.StartSentence();
                //        builder.AppendText("Hello Tyler.");
                //        builder.EndSentence();
                //        //builder.AppendBreak(PromptBreak.Small);
                //        //builder.AppendBreak(PromptBreak.ExtraSmall);
                //        //builder.AppendBreak(new TimeSpan(1)); //TimeSpan is a struct
                //        builder.AppendBreak(new TimeSpan(0, 0, 0, 0, 50));


                //        builder.StartSentence();
                //        builder.AppendText("How are you?", PromptEmphasis.Strong);
                //        builder.EndSentence();

                //        synthesizer.SpeakAsync(builder);
                //        //synthesizer.SpeakAsync("Hello Tyler.", "How are you?");
                //        prompted = true;
                //    }
                //    break;
                case "Print my name.":
                    rtbVoiceLog.Text += "\nTyler";
                    break;
                case "Speak selected text.":
                    synthesizer.SpeakAsync(rtbVoiceLog.SelectedText);
                    break;
                case "Open Google Calendar.":
                    //RebugLuckyy's video (C# Tutorial 2: How to open a web page with C# application), here: 
                    //https://www.youtube.com/watch?v=9Rhjp9Hcins was referenced.
                    //System.Diagnostics.Process.Start("https://accounts.google.com/ServiceLogin?service=cl&passive=1209600&osid=1&continue=https://calendar.google.com/calendar/render&followup=https://calendar.google.com/calendar/render&scc=1");﻿

                    //Hallgrim's answer to Skuta was also referenced for this, at http://stackoverflow.com/questions/223268/how-do-i-open-alternative-webbrowser-mozilla-or-firefox-and-show-the-specific .
                    synthesizer.SpeakAsync("Google Calendar is opening for you.  Please sign in.");
                    string url = "https://accounts.google.com/ServiceLogin?service=cl&passive=1209600&osid=1&continue=https://calendar.google.com/calendar/render&followup=https://calendar.google.com/calendar/render&scc=1";
                    System.Diagnostics.Process.Start(url);
                    break;
                case "Open Microsoft Word.":
                    //I referenced Igal Tabachnik's response to rudigrobler on StackOverflow for this, here: http://stackoverflow.com/questions/240171/launching-a-application-exe-from-c
                    //It turned out to be the same command used for opening a web page.
                    synthesizer.SpeakAsync("Microsoft Word is opening.  You may start typing.");
                    System.Diagnostics.Process.Start("C:/ProgramData/Microsoft/Windows/Start Menu/Programs/Microsoft Office/Microsoft Office Word 2007");
                    break;
                case "Open my email.":
                    synthesizer.SpeakAsync("Your email provider is opening.  Please sign in.");
                    System.Diagnostics.Process.Start("https://login.live.com/login.srf?wa=wsignin1.0&ct=1409066173&rver=6.1.6206.0&sa=1&ntprob=-1&wp=MBI_SSL_SHARED&wreply=https:%2F%2Fmail.live.com%2F%3Fowa%3D1%26owasuffix%3Dowa%252f&id=64855&snsc=1&cbcxt=mail");
                    break;
                case "Say hi.":
                    rtbVoiceLog.Text = "Hi Tyler's dad!";
                    synthesizer.SpeakAsync("Hi Tyler's dad.  Would you like to see a demo of what he has been working on?");
                    break;
                case "Sure.":
                    synthesizer.SpeakAsync("Alright Mr. Barton, here we go.  What would you like to open?");
                    rtbVoiceLog.Text = "\nOpen Google Calendar.\nOpen Microsoft Word.\nOpen my email.\nExit.";
                    synthesizer.SpeakAsync("Here are your options.");
                    break;
                case "Exit.":
                    synthesizer.SpeakAsync("The application is closing.  Thank you and have a nice day.");
                    rtbVoiceLog.Text = "Exiting...";
                    Environment.Exit(1);
                    break;
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            speechRecEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
            btnEnable.Enabled = false;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            speechRecEngine.RecognizeAsyncStop();
            btnEnable.Enabled = true;
            btnDisable.Enabled = false;
        }

        private void rtbVoiceLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
