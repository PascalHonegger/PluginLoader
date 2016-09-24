using System.Windows;
using System.Windows.Media.Imaging;
using Plugin1.Properties;
using PluginContract;

namespace Plugin1
{
	public class Plugin : IPlugin
	{
		public Plugin()
		{
			Image = Resources.Logo.ToBitmapSource();
		}

		public string Name => "Best Plugin in the World!";
		public BitmapSource Image { get; }
		public void Show()
		{
			MessageBox.Show("This could be a usefull plugin!");
		}
	}
}