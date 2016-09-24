using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.Commands;
using PluginContract;
using PluginLoader;
using PluginViewer.Properties;

namespace PluginViewer
{
	public sealed class MainWindowViewModel : INotifyPropertyChanged
	{
		private IPlugin _selectedPlugin;

		public MainWindowViewModel()
		{
			Plugins = new ObservableCollection<IPlugin>(PluginLoader<IPlugin>.LoadPlugins(Environment.ExpandEnvironmentVariables(Settings.Default.PluginsFolder)));

			OpenSelectedPlugin = new DelegateCommand(() => SelectedPlugin.Show(), () => SelectedPlugin != null);
		}

		public ObservableCollection<IPlugin> Plugins { get; }

		public IPlugin SelectedPlugin
		{
			get { return _selectedPlugin; }
			set
			{
				if (Equals(_selectedPlugin, value)) return;
				_selectedPlugin = value;
				OnPropertyChanged();
				OpenSelectedPlugin.RaiseCanExecuteChanged();
			}
		}

		public DelegateCommand OpenSelectedPlugin { get; }

		/// <summary>Occurs when a property value changes.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
