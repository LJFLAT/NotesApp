﻿using NotesApp.Model;
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

namespace NotesApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for NotesUC.xaml
    /// </summary>
    public partial class NotesUC : UserControl
    {


        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Note.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(NotesUC), new PropertyMetadata(null, SetValue));

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotesUC note = d as NotesUC;
            if (note != null)
            {
                note.titleTextBlock.Text = (e.NewValue as Note).Title;
                note.editedTextBlock.Text = (e.NewValue as Note).UpdatedTime.ToShortDateString();
                note.contentTextBlock.Text = (e.NewValue as Note).Title;
            }
        }

        public NotesUC()
        {
            InitializeComponent();
        }
    }
}
