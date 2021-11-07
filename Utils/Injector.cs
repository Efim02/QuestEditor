using SimpleInjector;

namespace QuestEditor.Utils
{
    /// <summary>
    ///     Инжектор зависимостей.
    /// </summary>
    public static class Injector
    {
        /// <summary>
        ///     Контейнер.
        /// </summary>
        private static Container _container;

        /// <summary>
        ///     Возвращает Instance из Container-а.
        /// </summary>
        /// <typeparam name="TService">Тип запрошенной переменной.</typeparam>
        /// <returns>Переменная.</returns>
        public static TService Get<TService>()
            where TService : class
        {
            return _container.GetInstance<TService>();
        }

        /// <summary>
        ///     Данный метод установит в переменную Container, основной Container приложения.
        /// </summary>
        /// <returns>True - значит контейнер был установлен.</returns>
        public static bool RegisterContainer(Container container)
        {
            if (_container != null)
                return false;

            _container = container;
            return true;
        }
    }
}