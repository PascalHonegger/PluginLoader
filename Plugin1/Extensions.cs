using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Plugin1
{
	public static class Extensions
	{
		public static BitmapSource ToBitmapSource(this Bitmap bitmap)
		{
			if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));

			return Imaging.CreateBitmapSourceFromHBitmap(
				bitmap.GetHbitmap(),
				IntPtr.Zero,
				Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions());
		}
	}
}
