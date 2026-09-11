# customClock

A small, customizable Windows desktop clock built with WPF.

I made this because I wanted a clock that did a few things the default Windows clock doesn't do, particularly the custom ringtones.

## ✨ Features

- 🕒 **Live digital clock**
  - Displays the current time in `HH:mm:ss` format.
  - Updates once per second.

- 💬 **Time-based messages**
  - The clock displays different messages depending on the time of day.
  - Messages change around midday, evening, and late night.

- ⏰ **Custom alarms**
  - Set alarms with hour, minute, and second precision.
  - Add multiple alarms.
  - Enable or disable individual alarms.
  - Remove alarms from the active alarm list.
  - View scheduled alarms in a dedicated alarm-management page.

- 🔔 **Custom ringtones**
  - Select your own audio files for alarms.
  - Supported formats include:
    - `.mp3`
    - `.wav`
    - `.wma`
    - `.aac`
    - `.flac`

- 🪟 **Custom alarm notification**
  - Alarms use a dedicated popup window instead of the standard Windows message box.
  - The popup displays the alarm time and selected ringtone.
  - Ringtones loop until the alarm is dismissed.
  - The alarm window stays on top of other windows.

- 🎨 **Custom UI**
  - Gradient background.
  - Glass-like translucent panels.
  - Rounded corners and subtle shadows.
  - Pink/purple visual theme.
  - Custom alarm cards and controls.

## 🖼️ Interface

The main clock provides a simple digital display with a notification-bell button for accessing alarms.

The alarm page contains:

- A time selector for creating alarms.
- A ringtone selector.
- A list of active alarms.
- Alarm count indicator.
- Controls for enabling/disabling and deleting alarms.

## 🛠️ Built With

- **C#**
- **WPF (Windows Presentation Foundation)**
- **.NET 10**
- **XAML**

The project targets:

```text
net10.0-windows
```

and uses WPF for the user interface.

### Dependencies

The project currently uses:

- [Emoji.Wpf](https://github.com/samhocevar/Emoji.Wpf) — emoji rendering in the WPF interface.
- [LibVLCSharp](https://github.com/videolan/libvlcsharp) — media functionality.
- [VideoLAN.LibVLC.Windows](https://www.nuget.org/packages/VideoLAN.LibVLC.Windows) — Windows LibVLC runtime.

## 🚀 Getting Started

### Requirements

- Windows
- .NET 10 SDK
- Visual Studio 2022 or another IDE capable of building modern .NET/WPF projects

### Build

Clone the repository:

```bash
git clone https://github.com/ssek69420/customClock.git
cd customClock
```

Build the solution:

```bash
dotnet build
```

Run the application:

```bash
dotnet run --project project/project.csproj
```

Alternatively, open `custom_clock.slnx` in Visual Studio and run the project from there.

## 📁 Project Structure

```text
customClock/
├── project/
│   ├── Assets/
│   ├── AlarmPopup.xaml
│   ├── AlarmPopup.xaml.cs
│   ├── Animation.cs
│   ├── App.xaml
│   ├── App.xaml.cs
│   ├── MainPage.xaml
│   ├── MainPage.xaml.cs
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   ├── OpenAlarms.xaml
│   ├── OpenAlarms.xaml.cs
│   └── project.csproj
├── .gitattributes
├── .gitignore
└── custom_clock.slnx
```

### Main components

| File | Purpose |
| --- | --- |
| `MainPage.xaml` | Main clock interface |
| `MainPage.xaml.cs` | Clock timer and time-based messages |
| `OpenAlarms.xaml` | Alarm-management interface |
| `OpenAlarms.xaml.cs` | Alarm creation, management, and scheduling |
| `AlarmPopup.xaml` | Custom alarm notification UI |
| `AlarmPopup.xaml.cs` | Alarm popup behavior and ringtone playback |
| `MainWindow.xaml` | Main application window |
| `MainWindow.xaml.cs` | Initializes the main page |

## ⏰ How Alarms Work

Alarms are represented by an `Alarm` object containing:

- Hour
- Minute
- Second
- Ringtone path
- Enabled/disabled state

The alarm page checks the current time once per second. When an enabled alarm matches the current time, the alarm is disabled and the custom alarm popup is shown.

The selected ringtone is played and automatically loops until the user dismisses the alarm.

## ⚠️ Current Limitations

This is a small personal project rather than a full replacement for the Windows Clock application.

Currently:

- Alarms are stored in memory and are **not persisted** between application/page sessions.
- Alarm scheduling relies on a one-second `DispatcherTimer`.
- There is currently no recurring/day-of-week alarm system.
- There is currently no snooze functionality.
- There is no installer or packaged release yet.

## 📌 Project Status

**Feature-complete for its current scope.**

The project started as a personal alternative to the default Windows clock and has grown into a simple customizable clock with a dedicated alarm system and custom alarm notifications.

## 📄 License

No license has currently been specified for this repository.

If you plan to distribute or reuse the project, check the repository for the current licensing status.

## 👤 Author

Created by [ssek69420](https://github.com/ssek69420).

Repository: https://github.com/ssek69420/customClock
