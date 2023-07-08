using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
//using Autofac;
using ArtiluxEOL.Framework;

namespace ArtiluxEOL.Framework
{
    public class Command : BindableObject, ICommand
    {
        private Action<object> executeAction;
        private Func<object, Task> executeActionAsync;
        private Func<object, bool> canExecuteAction;
        private readonly object syncObject = new object();

        private bool isExecuting = false;
        public bool IsExecuting
        {
            get { return isExecuting; }
            set
            {
                isExecuting = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler<Exception> Failed;

        private Command()
        {
            if (Command.application == null)
                throw new InvalidOperationException("Command type not initialized. Call Command.Use(IApplication) before first use.");
        }

        #region -=ICommand members=-

        bool ICommand.CanExecute(object parameter) => CanExecute(parameter);
        async void ICommand.Execute(object parameter) => await Execute(parameter);

        #endregion

        public bool CanExecute(object parameter) => canExecuteAction.Invoke(parameter);

        public async Task Execute(object parameter)
        {
            lock (syncObject)
            {
                if (isExecuting) return;
                IsExecuting = true;
            }
            try
            {
                if (executeAction != null)
                    executeAction.Invoke(parameter);
                else if (executeActionAsync != null)
                    await executeActionAsync.Invoke(parameter);
            }
            catch (Exception ex)
            {
                OnExecutionFailure(ex);
            }
            finally
            {
                IsExecuting = false;
            }

        }

        public virtual void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        protected virtual void OnExecutionFailure(Exception ex)
        {
            if (Failed == null)
                ExecutionFailed?.Invoke(this, ex);

            Failed?.Invoke(this, ex);
            using (var scope = application.BeginLifetimeScope())
            {
                scope.Resolve<IExceptionHandler>().Handle(ex);
            }
        }

        #region -=Static members=-
        private static IApplication application;
        /// <summary>
        /// Global command execution failure event. Global event is triggered when local event (Failed) is not set on command instance.
        /// </summary>
        public static event EventHandler<Exception> ExecutionFailed;

        public static void Use(IApplication appInstance)
        {
            application = appInstance;
        }
        /// <summary>
        /// Creates new Command instance.
        /// </summary>
        /// <param name="execute"></param>
        /// <returns></returns>
        public static Command Create(Action<object> execute) => Create(execute, parameter => true);
        /// <summary>
        /// Creates new Command instance.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <returns></returns>
        public static Command Create(Action<object> execute, Func<object, bool> canExecute)
            => new Command { executeAction = execute, canExecuteAction = canExecute ?? new Func<object, bool>(parameter => true) };

        /// <summary>
        /// Creates new Command instance for async execution.
        /// </summary>
        /// <param name="execute"></param>
        /// <returns></returns>
        public static Command CreateAsync(Func<object, Task> execute) => CreateAsync(execute, parameter => true);
        /// <summary>
        /// Creates new Command instance for async execution.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <returns></returns>
        public static Command CreateAsync(Func<object, Task> execute, Func<object, bool> canExecute)
            => new Command { executeActionAsync = execute, canExecuteAction = canExecute ?? new Func<object, bool>(parameter => true) };

        #endregion
    }
}
