using System.Windows;

namespace AURA.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var theme = new ResourceDictionary { Source = new Uri("/AURA.App;component/Styles/Theme.xaml", UriKind.RelativeOrAbsolute) };
    }
}
