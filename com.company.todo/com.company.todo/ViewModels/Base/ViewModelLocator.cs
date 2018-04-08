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
    /// <summary>
    /// ViewModel Locator Pattern for Register and Resolve objects dependency
    /// </summary>
    public class ViewModelLocator
    {
        private readonly IUnityContainer _container;

        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // services
            _container.RegisterType<INavigationService, NavigationService>();

            // viewmodels
            _container.RegisterType<TodoViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<DoneViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<AddTodoViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<DetailTodoViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<EditTodoViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<UpdateLogViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<MediaViewModel>(new ContainerControlledLifetimeManager());

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
