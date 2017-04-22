using System;
using Xamarin.Forms;

namespace yooin
{
	public static class ViewFactory
	{
		public static Image createImageView(ImageSource objSource, Aspect aspec)
		{
			return new Image
			{
				Aspect = aspec,
				Source = objSource,
			};
		}
		public static Button createButton(string sText)
		{
			return new Button
			{
				Text = sText,

			};
		}
		public static BoxView createBoxView(Color objColor, double dOpacity)
		{

			return new BoxView
			{
				Color = objColor,
				Opacity = dOpacity
			};
		}
		public static Label createLabel(string sText)
		{

			return new Label
			{
				Text = sText

			};
		}
		public static Entry createEntry(string sPlaceHolder, bool bPasswordMask)
		{

			return new Entry
			{
				Placeholder = sPlaceHolder,
				IsPassword = bPasswordMask
			};
		}
	}
}
