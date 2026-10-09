# AURA Assistant — Build Instructions

## Требования

- **Windows 10/11** (x64)
- **.NET 8.0 SDK** — скачать с https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- **Inno Setup 6.x** — скачать с https://jrsoftware.org/isinfo.php

## Сборка

### Вариант 1: На Windows (CMD)

```bash
build-all.cmd
```

### Вариант 2: На Windows (PowerShell)

```powershell
powershell -ExecutionPolicy Bypass -File .\build-all.ps1
```

## Результаты сборки

После успешной сборки получишь:

1. **`publish/AURA Assistant.exe`** — исполняемый файл приложения (standalone)
2. **`installer/output/AURA-Assistant-Setup-x64.exe`** — установщик для Windows

## Установка

Двойной клик на **`AURA-Assistant-Setup-x64.exe`** и следуй инструкциям мастера установки.

После установки AURA будет доступна в:
- Меню **Пуск** → **AURA Assistant**
- Папка установки: `C:\Users\<YourName>\AppData\Local\Programs\AURA Assistant`

## Запуск без установки

Можно запустить приложение напрямую:
```bash
publish\AURA Assistant.exe
```

## Решение проблем

### Ошибка: "iscc.exe не найден"

Установи **Inno Setup** и убедись, что он добавлен в переменную окружения PATH:
1. Скачай с https://jrsoftware.org/isinfo.php
2. Установи с галочкой "Add to PATH"
3. Перезагрузи компьютер

### Ошибка: ".NET SDK не найден"

Скачай и установи **.NET 8.0 SDK** с https://dotnet.microsoft.com/en-us/download/dotnet/8.0

## Лицензия

MIT License — см. LICENSE файл в репозитории.
