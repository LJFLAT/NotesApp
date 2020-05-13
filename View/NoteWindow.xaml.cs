using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotesApp.View
{
    /// <summary>
    /// Interaction logic for NoteWindow.xaml
    /// </summary>
    public partial class NoteWindow : Window
    {
        SpeechRecognitionEngine recognizer;

        public NoteWindow()
        {
            InitializeComponent();

            //var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
            //                      where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
            //                      select r).FirstOrDefault();


            // I tried an alternative which returned "en-AU" for my computer but this also crashed.
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture;

                recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            recognizer.SetInputToDefaultAudioDevice();

            //When I left the SpeechRecognitionEngine as blank / default, it then crashed on the next line.

                GrammarBuilder builder = new GrammarBuilder();
                builder.AppendDictation();
                Grammar grammar = new Grammar(builder);

                recognizer.LoadGrammar(grammar);
                recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;

            rtbContent.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        bool isRecognizing = false;

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isRecognizing)
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                isRecognizing = true;
            }
            else
            {
                recognizer.RecognizeAsyncStop();
                isRecognizing = false;
            }

        }

        private void rtbContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters = (new TextRange(rtbContent.Document.ContentStart, rtbContent.Document.ContentEnd)).Text.Length;

            tbStatus.Text = $"Document length: {amountOfCharacters} characters";
        }

        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            rtbContent.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
        }

        private void btnItalic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUnderline_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
