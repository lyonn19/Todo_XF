using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Services.Navigation;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace com.company.todo.ViewModels.Base
{
    public class ViewModelLocator
    {
        private static AddTasksViewModel _tasksViewModel;
        public static AddTasksViewModel TasksViewModel
            => _tasksViewModel ?? (_tasksViewModel = new AddTasksViewModel());

        private readonly IUnityContainer _container;

        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // services
            _container.RegisterType<INavigationService, NavigationService>();
            // remote services

            // repository

            // viewmodels
            _container.RegisterType<AddTasksViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<TasksViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<GalleryViewModel>(new ContainerControlledLifetimeManager());

            var unityServiceLocator = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
