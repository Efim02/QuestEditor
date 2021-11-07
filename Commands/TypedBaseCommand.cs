using System;
using System.Windows;

namespace QuestEditor.Commands
{
    /// <summary>
    ///     Базовый класс для команд с типизованным параметром.
    /// </summary>
    /// <typeparam name="T">Тип вью-модели.</typeparam>
    public abstract class TypedBaseCommand<T> : BaseCommand
    {
        /// <summary>
        ///     Название окна ошибки.
        /// </summary>
        private const string ErrorWindowTitle = "Ошибка команды";

        /// <summary>
        ///     Указывает, доступна ли возможность вызова команды.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        /// <returns> True - доступна. </returns>
        public sealed override bool CanExecute(object parameter)
        {
            if (parameter is T typedParameter)
                return CanExecute(typedParameter);

            return true;
        }

        /// <summary>
        ///     Выполнение команды.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        public sealed override void Execute(object parameter)
        {
            try
            {
                if (parameter is T typedParameter)
                    Execute(typedParameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ErrorWindowTitle}\n{ex.Message}\n{ex.InnerException}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        ///     Указывает, доступна ли возможность вызова команды с типизированным параметром.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        /// <returns> True - доступна. </returns>
        protected virtual bool CanExecute(T parameter)
        {
            return true;
        }

        /// <summary>
        ///     Выполнение команды с типизированным параметром.
        /// </summary>
        /// <param name="parent"> Параметр для команды. </param>
        protected abstract void Execute(T parent);
    }
}