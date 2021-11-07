using System;
using System.Windows.Input;

namespace QuestEditor.Commands
{
    /// <summary>
    ///     Класс позволяющий реализовать команды в wpf.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        ///     Может ли, действие выполниться.
        /// </summary>
        /// <param name="parameter">Объект вызвавший метод.</param>
        /// <returns></returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        ///     Происходит при изменениях, влияющих на то, должна выполняться данная команда или нет.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Выполнить действие команды.
        /// </summary>
        /// <param name="parameter">Объект вызвавший метод.</param>
        public abstract void Execute(object parameter);
    }
}