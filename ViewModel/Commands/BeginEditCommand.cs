﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class BeginEditCommand : ICommand
    {
        public NoteVM Vm { get; set; }
        public event EventHandler CanExecuteChanged;

        public BeginEditCommand(NoteVM vm)
        {
            Vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Vm.StartEditing();
        }
    }
}
