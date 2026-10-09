using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
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
        AddLog("Готов к ��окальному управлению Windows.");
        SetListeningState(false);
    }

    private void ToggleListening_Click(object sender, RoutedEventArgs e)
    {
        isListening = !isListening;
        SetListeningState(isListening);
        AddLog(isListening ? "Прослушивание активировано." : "Прослушивание остановлено.");
    }

    private void OpenSettings_Click(object sender, RoutedEventArgs e)
    {
        AddLog("Открываю настройки AURA.");
        StatusText.Text = "Настройки";

        try
        {
            Process.Start(new ProcessStartInfo("ms-settings:") { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            AddLog($"Не удалось открыть настройки: {ex.Message}");
        }
    }

    private void RunBrowserCommand_Click(object sender, RoutedEventArgs e)
    {
        ExecuteCommand("открыть браузер");
    }

    private void RunWorkModeCommand_Click(object sender, RoutedEventArgs e)
    {
        ExecuteCommand("рабочий режим");
    }

    private void CreateNoteCommand_Click(object sender, RoutedEventArgs e)
    {
        ExecuteCommand("создать заметку");
    }

    private void RunCustomCommand_Click(object sender, RoutedEventArgs e)
    {
        var command = CommandInput.Text;
        ExecuteCommand(command);
    }

    private void CommandInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            ExecuteCommand(CommandInput.Text);
        }
    }

    private void ExecuteCommand(string rawCommand)
    {
        var result = CommandExecutor.Execute(rawCommand);
        AddLog(result);
        StatusText.Text = result;

        if (result.Contains("рабочий режим", StringComparison.OrdinalIgnoreCase))
        {
            StatusText.Text = "Рабочий режим";
        }
    }

    private void SetListeningState(bool listening)
    {
        ToggleListeningButton.Content = listening ? "Остановить прослушивание" : "Запустить прослушивание";
        StatusText.Text = listening ? "Слушаю" : "Ожидание";
        StatusIndicator.Fill = listening
            ? new SolidColorBrush(Color.FromRgb(34, 197, 94))
            : new SolidColorBrush(Color.FromRgb(148, 163, 184));
    }

    private void AddLog(string message)
    {
        log.Insert(0, $"{DateTime.Now:HH:mm:ss} — {message}");
    }
}
