using System;
using Xamarin.Forms;

namespace yooin
{
	public class CustomEntry : Entry
	{
		public bool bIsPassword
		{
			get;
			set;
		}
		public string sTextEntry
		{
			get;
			set;
		}
		public ViewFactory.KeyboardType kbt
		{
			get;
			set;
		}
		public event EventHandler ValidationRequested;
		public void Validate()
		{
			if (ValidationRequested != null)
			{
				ValidationRequested(this, EventArgs.Empty);
			}
		}
		public event EventHandler getText;
		public void getTextData()
		{
			if (getText != null)
			{
				getText(this, EventArgs.Empty);
			}
		}
	}
}
