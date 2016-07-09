//This code references Johnny Manson's YouTube video (C# Development Tutorial | Voice Recognition):
//https://www.youtube.com/watch?v=AB9lfHDOe5U

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
//using System.Speech.Synthesis;

namespace VoiceControlApplication2
{
    public partial class frmVoiceControl : Form
    {
        public frmVoiceControl()
        {
            InitializeComponent();
        }

        SpeechRecognitionEngine speechRecEngine = new SpeechRecognitionEngine();

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices voiceCommands = new Choices();
            voiceCommands.Add(new string[] { "Say hello.", "Print my name."});
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
                    MessageBox.Show("Hello Tyler.  How are you?");
                    break;
                case "Print my name.":
                    rtbVoiceLog.Text += "\nTyler";
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
