using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DesignIntentDesktop.Expressions;

namespace DesignIntentDesktop.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Command Helpers
        
        /// <summary>
        /// Runs a command if the updating flag is not set.
        /// If the flag is true (the function is already running)
        /// If the flag is false (indicating no running function) the the action is run
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag defining if the command is already running</param>
        /// <param name="action">The action to run if command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            //Check if the flag property is true (meaning the function is already running)
            if (updatingFlag.GetPropertyValue())
                return;

            //Set the property to true to indicate we are running
            updatingFlag.SetPropertyValue(true);

            try
            {
                //Run the added in action
                await action();
            }
            finally
            {
                //set the property back false
                updatingFlag.SetPropertyValue(false);
            }

        }

        #endregion
    }
}
