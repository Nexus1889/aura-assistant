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
        AddLog("Готов к локальному управлению Windows.");
        SetListeningState(false);
        CommandInput.Text = "открыть браузер";
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
            AddLog("Панель настроек Windows открыта.");
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
        ExecuteCommand(CommandInput.Text);
    }

    private void CommandInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            ExecuteCommand(CommandInput.Text);
        }
    }

    private void ExecuteCommand(string? rawCommand)
    {
        var command = rawCommand ?? string.Empty;
        var result = CommandExecutor.Execute(command);

        AddLog(result);

        if (result.Contains("рабочий режим", StringComparison.OrdinalIgnoreCase))
        {
            StatusText.Text = "Рабочий режим";
            return;
        }

        if (result.Contains("открываю", StringComparison.OrdinalIgnoreCase)
            || result.Contains("запускаю", StringComparison.OrdinalIgnoreCase)
            || result.Contains("создаю", StringComparison.OrdinalIgnoreCase))
        {
            StatusText.Text = "Выполняю";
            return;
        }

        if (result.Contains("не распознана", StringComparison.OrdinalIgnoreCase))
        {
            StatusText.Text = "Команда неизвестна";
            return;
        }

        StatusText.Text = "Готово";
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
