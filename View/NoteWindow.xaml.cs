using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontFamily.ItemsSource = fontFamilies;

            List<double> fontSizes = new List<double>() { 8, 9, 10, 11, 12, 13, 16, 20, 28, 32, 48, 60 };
            cmbFontSize.ItemsSource = fontSizes;

            //var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
            //                      where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
            //                      select r).FirstOrDefault();

            return; //I placed this here to continue with the course

            // I tried an alternative which returned "en-AU" for my computer but this also crashed.
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture;

            recognizer = new SpeechRecognitionEngine();

            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendDictation();
            Grammar grammar = new Grammar(builder);

            //When I left the SpeechRecognitionEngine as blank / default, it then crashed on the next line.

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

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            return;

            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                recognizer.RecognizeAsyncStop();
            }

        }

        private void rtbContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters = (new TextRange(rtbContent.Document.ContentStart, rtbContent.Document.ContentEnd)).Text.Length;

            tbStatus.Text = $"Document length: {amountOfCharacters} characters";
        }

        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
            {
                rtbContent.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                rtbContent.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void btnItalic_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
            {
                rtbContent.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                rtbContent.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void btnUnderline_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
            {
                rtbContent.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                TextDecorationCollection textDecorations;
                (rtbContent.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                rtbContent.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void rtbContent_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = rtbContent.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));

            var selectedStyle = rtbContent.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

            var selectedUnderline = rtbContent.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedUnderline.Equals(TextDecorations.Underline));

            cmbFontFamily.SelectedItem = rtbContent.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontSize.Text = (rtbContent.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFontFamily.SelectedItem != null)
            {
                rtbContent.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            rtbContent.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
        }
    }
}
