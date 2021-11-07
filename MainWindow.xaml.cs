using QuestEditor.Vms;

namespace QuestEditor
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new EditorVM();
            InitializeComponent();
        }
    }
}