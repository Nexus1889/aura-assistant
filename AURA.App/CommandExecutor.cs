using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AURA.App;

public static class CommandExecutor
{
    public static string Execute(string rawCommand)
    {
        if (string.IsNullOrWhiteSpace(rawCommand))
        {
            return "Пустая команда.";
        }

        var command = rawCommand.Trim();

        if (command.Contains("браузер", StringComparison.OrdinalIgnoreCase)
            || command.Contains("открой интернет", StringComparison.OrdinalIgnoreCase)
            || command.Contains("open browser", StringComparison.OrdinalIgnoreCase))
        {
            OpenUrl("https://www.google.com");
            return "Открываю браузер.";
        }

        if (command.Contains("калькулятор", StringComparison.OrdinalIgnoreCase)
            || command.Contains("calculator", StringComparison.OrdinalIgnoreCase))
        {
            StartProcess("calc");
            return "Запускаю калькулятор.";
        }

        if (command.Contains("блокнот", StringComparison.OrdinalIgnoreCase)
            || command.Contains("notepad", StringComparison.OrdinalIgnoreCase))
        {
            StartProcess("notepad");
            return "Открываю блокнот.";
        }

        if (command.Contains("рисовалка", StringComparison.OrdinalIgnoreCase)
            || command.Contains("paint", StringComparison.OrdinalIgnoreCase))
        {
            StartProcess("mspaint");
            return "Открываю Paint.";
        }

        if (command.Contains("загрузки", StringComparison.OrdinalIgnoreCase)
            || command.Contains("downloads", StringComparison.OrdinalIgnoreCase))
        {
            OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads");
            return "Открываю папку Загрузки.";
        }

        if (command.Contains("документы", StringComparison.OrdinalIgnoreCase)
            || command.Contains("documents", StringComparison.OrdinalIgnoreCase))
        {
            OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            return "Открываю Документы.";
        }

        if (command.Contains("рабочий режим", StringComparison.OrdinalIgnoreCase)
            || command.Contains("work mode", StringComparison.OrdinalIgnoreCase))
        {
            return "Запускаю рабочий режим AURA.";
        }

        if (command.Contains("заметка", StringComparison.OrdinalIgnoreCase)
            || command.Contains("note", StringComparison.OrdinalIgnoreCase))
        {
            CreateNote();
            return "Создаю заметку.";
        }

        if (command.Contains("настройки", StringComparison.OrdinalIgnoreCase)
            || command.Contains("settings", StringComparison.OrdinalIgnoreCase))
        {
            StartProcess("ms-settings:");
            return "Открываю настройки Windows.";
        }

        if (command.Contains("терминал", StringComparison.OrdinalIgnoreCase)
            || command.Contains("cmd", StringComparison.OrdinalIgnoreCase)
            || command.Contains("командная строка", StringComparison.OrdinalIgnoreCase))
        {
            StartProcess("cmd");
            return "Открываю командную строку.";
        }

        if (command.Contains("папка", StringComparison.OrdinalIgnoreCase)
            || command.Contains("explorer", StringComparison.OrdinalIgnoreCase))
        {
            StartProcess("explorer");
            return "Открываю проводник.";
        }

        return $"Команда не распознана: \"{command}\".";
    }

    private static void OpenUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (Exception) { }
    }

    private static void StartProcess(string processName)
    {
        try
        {
            if (processName.StartsWith("ms-", StringComparison.OrdinalIgnoreCase))
            {
                Process.Start(new ProcessStartInfo(processName) { UseShellExecute = true });
                return;
            }

            Process.Start(new ProcessStartInfo(processName) { UseShellExecute = true });
        }
        catch (Exception) { }
    }

    private static void OpenFolder(string folder)
    {
        try
        {
            Directory.CreateDirectory(folder);
            Process.Start(new ProcessStartInfo
            {
                FileName = folder,
                UseShellExecute = true
            });
        }
        catch (Exception) { }
    }

    private static void CreateNote()
    {
        var notesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AURA");
        Directory.CreateDirectory(notesFolder);

        var filePath = Path.Combine(notesFolder, $"note-{DateTime.Now:yyyyMMdd-HHmmss}.txt");
        File.WriteAllText(filePath, $"Запись AURA от {DateTime.Now:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}---{Environment.NewLine}");
    }
}
