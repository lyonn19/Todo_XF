using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Data.Local;
using com.company.todo.Models;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    /// <summary>
    /// ViewModel for AddTodoPage AddNew todoItem
    /// </summary>
    public class AddTodoViewModel : ViewModelBase
    {
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

        
        private ImageSource _imagen;
        public ImageSource ImagenSource
        {
            get { return _imagen; }
            set
            {
                _imagen = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add new todoItem in local database
        /// </summary>
        /// <returns></returns>
        private async Task AddTodo()
        {
            try
            {
                await TodoDao.Instance.AddTodoAsync(new TodoItem
                {
                    Content = Content,
                    CreatedAt = DateTime.Now,
                    Imagen = ViewModelLocator.Instance.Resolve<MediaViewModel>().PImage,
                    Status = false,
                    UpdatedAt = DateTime.Now,
                });

                await Application.Current.MainPage.DisplayAlert("Info", "Item added successfully", "OK");
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error saving task, try again", "OK");
            }
            
        }
        
        #endregion

        #region Commands
        Command _addTodoCommand;
        public Command AddTodoCommand
        {
            get
            {
                return _addTodoCommand ?? (_addTodoCommand = new Command(async () => await AddTodoAsync(), () => !IsBusy));
            }
        }
        public async Task AddTodoAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await AddTodo();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
