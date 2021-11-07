using System.Windows;
using QuestEditor.Utils;

namespace QuestEditor
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppUtils.Configure();
            base.OnStartup(e);
        }
    }
}