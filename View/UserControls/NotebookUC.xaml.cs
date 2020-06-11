using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NotesApp.Model;

namespace NotesApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for Notebook.xaml
    /// </summary>
    public partial class NotebookUC : UserControl
    {


        public Notebook DisplayNotebook
        {
            get { return (Notebook)GetValue(DisplayNotebookProperty); }
            set { SetValue(DisplayNotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayNotebook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayNotebookProperty =
            DependencyProperty.Register("DisplayNotebook", typeof(Notebook), typeof(NotebookUC), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotebookUC notebookUC = d as NotebookUC;

            if (notebookUC != null)
            {
                notebookUC.notebookNameTextBlock.Text = (e.NewValue as Notebook).Name;
            }
        }

        public NotebookUC()
        {
            InitializeComponent();
        }
    }
}
