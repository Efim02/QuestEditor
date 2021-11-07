using AutoMapper;
using QuestEditor.Vms;
using QuestEditor.Xlsx;
using SimpleInjector;

namespace QuestEditor.Utils
{
    /// <summary>
    ///     Утилиты приложения.
    /// </summary>
    public static class AppUtils
    {
        private static readonly MapperConfiguration _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap(typeof(RawQuest), typeof(RawQuestVM)).ReverseMap();
            cfg.CreateMap(typeof(RawStep), typeof(RawStepVM)).ReverseMap();
        });

        /// <summary>
        ///     Установить настройки для приложения.
        /// </summary>
        /// <remarks></remarks>
        public static void Configure()
        {
            var container = new Container();

            var mapper = _mapperConfiguration.CreateMapper();
            container.RegisterInstance(mapper);

            Injector.RegisterContainer(container);
        }
    }
}