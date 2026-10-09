using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace AURA.App;

public partial class MainWindow : Window
{
    private bool isListening;
    private readonly ObservableCollection<string> log = new();

    public MainWindow()
    {
        InitializeComponent();
        CommandLogList.ItemsSource = log;
        AddLog("Приложение запущено.");
        AddLog("Готов к локальному управлению Windows.");
    }

    private void ToggleListening_Click(object sender, RoutedEventArgs e)
    {
        isListening = !isListening;

        ToggleListeningButton.Content = isListening ? "Остановить прослушивание" : "Запустить прослушивание";
        StatusText.Text = isListening ? "Слушаю" : "Ожидание";
        StatusIndicator.Fill = isListening
            ? new SolidColorBrush(Color.FromRgb(34, 197, 94))
            : new SolidColorBrush(Color.FromRgb(148, 163, 184));

        AddLog(isListening ? "Прослушивание активировано." : "Прослушивание остановлено.");
    }

    private void OpenSettings_Click(object sender, RoutedEventArgs e)
    {
        AddLog("Открываю настройки AURA.");
        StatusText.Text = "Настройки";
    }

    private void RunBrowserCommand_Click(object sender, RoutedEventArgs e)
    {
        AddLog("Выполняю команду: открыть браузер.");
    }

    private void RunWorkModeCommand_Click(object sender, RoutedEventArgs e)
    {
        AddLog("Запускаю рабочий режим AURA.");
    }

    private void CreateNoteCommand_Click(object sender, RoutedEventArgs e)
    {
        AddLog("Создаю заметку и сохраняю в рабочую папку.");
    }

    private void AddLog(string message)
    {
        log.Insert(0, $"{DateTime.Now:HH:mm:ss} — {message}");
    }
}
