﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Data.Local;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    public class AddTasksViewModel : ViewModelBase
    {
        #region Fields
        public ObservableCollection<Models.Task> Tasks;

        #endregion

        #region Builder
        public AddTasksViewModel()
        {
            Tasks = new ObservableCollection<Models.Task>();
        }
        #endregion


        #region Properties
        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set
            {
                _createdAt = value;
                OnPropertyChanged();
            }
        }

        private DateTime _updateAt;
        public DateTime UpdateAt
        {
            get { return _updateAt; }
            set
            {
                _updateAt = value;
                OnPropertyChanged();
            }
        }

        private bool _status;
        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private byte[] _imagen;
        public byte[] Imagen
        {
            get { return _imagen; }
            set
            {
                _imagen = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Use cases

        /// <summary>
        /// Init ViewModel
        /// </summary>
        /// <param name="navigationData"></param>
        /// <returns></returns>
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        /// <summary>
        /// Add new Task persist local
        /// </summary>
        /// <returns></returns>
        private async Task AddTask()
        {
            await TaskDao.Instance.AddTaskAsync(new Models.Task()
            {
                Content = Content,
                CreatedAt = DateTime.Now,
                Imagen = Imagen,
                Status = true,
                UpdateAt = DateTime.Now,
            });
        }
        
        #endregion

        #region Commands

        Command _addTaskCommand;
        public Command AddTaskCommand
        {
            get
            {
                return _addTaskCommand ?? (_addTaskCommand = new Command(async () => await AddTaskAsync(), () => !IsBusy));
            }
        }
        public async Task AddTaskAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await AddTask();
            }
            finally
            {
                IsBusy = false;
            }
        }

        
        #endregion
    }
}
