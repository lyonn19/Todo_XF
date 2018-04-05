using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Services.Navigation;
using Xamarin.Forms;

namespace com.company.todo.ViewModels.Base
{
    public class ViewModelBase : BindableObject
    {
        /// <summary>
        /// PROVIDES ACCESS TO THE NAVIGATION SERVICE
        /// </summary>
        protected readonly INavigationService Navigation;

        public ViewModelBase()
        {
            Navigation = ViewModelLocator.Instance.Resolve<INavigationService>();
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
