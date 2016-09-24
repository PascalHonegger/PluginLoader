using System.Windows.Media.Imaging;

namespace PluginContract
{
	public interface IPlugin
	{
		string Name { get; }
		BitmapSource Image { get; }
		void Show();
	}
}