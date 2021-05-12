using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using AngleSharp.Dom.Events;
using Autofac;
using Caliburn.Micro;
using Serilog;
using SharpDj.ViewModels;
using SharpDj.ViewModels.SubViews;
using SharpDj.Input;
using SharpDj.Interfaces;
using SharpDj.Logic;
using SharpDj.Logic.ActionToServer;
using SharpDj.Views;
using IContainer = Autofac.IContainer;

namespace SharpDj
{

    public class AppBootstrapper : BootstrapperBase
    {
        private static IContainer _container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = BuildContainer();
            CreateGestureTriggers();
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>()
                .As<IEventAggregator>()
                .SingleInstance()
                .AutoActivate();

            builder.RegisterType<Config>()
                .AsSelf()
                .SingleInstance()
                .AutoActivate();

            var appAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(appAssemblies)
                .InNamespace(typeof(IAction).Namespace ??
                             throw new InvalidOperationException("Problem with finding namespace for Server Handlers"))
                .PublicOnly()
                .Where(x => x.Name.StartsWith("Client") && x.Name.EndsWith("Action"))
                .InstancePerLifetimeScope()
                .AsSelf(); 

            builder.RegisterType<ClientSender>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<ClientConnection>()
                .AsSelf()
                .SingleInstance();
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (_container.IsRegistered(service))
                    return _container.Resolve(service);
            }
            else
            {
                if (_container.IsRegisteredWithKey(key, service))
                    return _container.ResolveKeyed(key, service);
            }

            var msgFormat = "Could not locate any instances of contract {0}.";
            var msg = string.Format(msgFormat, key ?? service.Name);
            throw new Exception(msg);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(service);
            return _container.Resolve(type) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            //  register view models
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                //  must be a type that ends with ViewModel
                .Where(type => type.Name.EndsWith("ViewModel"))
                //  must implement INotifyPropertyChanged (deriving from PropertyChangedBase will statisfy this)
                .Where(type => type.GetInterface(nameof(INotifyPropertyChanged)) != null)
                //  registered as self
                .AsSelf()
                //  always create a new one
                .InstancePerDependency();

            //  register views
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                //  must be a type that ends with View
                .Where(type => type.Name.EndsWith("View"))
                //  registered as self
                .AsSelf()
                //  always create a new one
                .InstancePerDependency();

            //  register the single window manager for this container
            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            //  register the single event aggregator for this container
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

            ConfigureContainer(builder);
            return builder.Build();
        }

        private void CreateGestureTriggers()
        {
            var defaultCreateTrigger = Parser.CreateTrigger;

            Parser.CreateTrigger = (target, triggerText) =>
            {
                if (triggerText == null)
                {
                    return defaultCreateTrigger(target, null);
                }

                var triggerDetail = triggerText
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                var splits = triggerDetail.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                switch (splits[0])
                {
                    case "Key":
                        var key = (Key)Enum.Parse(typeof(Key), splits[1], true);
                        return new KeyTrigger { Key = key };

                    case "Gesture":
                        var mkg = (MultiKeyGesture)(new MultiKeyGestureConverter()).ConvertFrom(splits[1]);
                        return new KeyTrigger { Modifiers = mkg.KeySequences[0].Modifiers, Key = mkg.KeySequences[0].Keys[0] };
                }

                return defaultCreateTrigger(target, triggerText);
            };
        }
    }
}