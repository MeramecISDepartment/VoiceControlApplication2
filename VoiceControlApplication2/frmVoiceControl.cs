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
            voiceCommands.Add(new string[] { "Say hello.", "Print my name.", "Speak selected text."});
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
                case "Say hello.":

                    //MessageBox.Show("Hello Tyler.  How are you?");
                    //builder.StartStyle(new PromptStyle(PromptEmphasis));
                    //builder.StartStyle(new PromptStyle(PromptRate));
                    //builder.StartStyle(new PromptStyle(PromptVolume));
                    //builder.StartSentence();

                    //synthesizer.SpeakAsync("Hello Tyler.  How are you?");

                    bool prompted = false;

                    while (prompted == false)
                    {
                        PromptBuilder builder = new PromptBuilder();

                        builder.StartSentence();
                        builder.AppendText("Hello Tyler.");
                        builder.EndSentence();
                        //builder.AppendBreak(PromptBreak.Small);
                        //builder.AppendBreak(PromptBreak.ExtraSmall);
                        //builder.AppendBreak(new TimeSpan(1)); //TimeSpan is a struct
                        builder.AppendBreak(new TimeSpan(0, 0, 0, 0, 50));


                        builder.StartSentence();
                        builder.AppendText("How are you?", PromptEmphasis.Strong);
                        builder.EndSentence();

                        synthesizer.SpeakAsync(builder);
                        //synthesizer.SpeakAsync("Hello Tyler.", "How are you?");
                        prompted = true;
                    }
                    break;
                case "Print my name.":
                    rtbVoiceLog.Text += "\nTyler";
                    break;
                case "Speak selected text.":
                    synthesizer.SpeakAsync(rtbVoiceLog.SelectedText);
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
