using System;
using Xamarin.Forms;

namespace yooin
{
	public class BaseRelativeLayout : ContentView
	{
		RelativeLayout layout;
		ActivityIndicator aiLoading;
		ScrollView scrolllayout;

		public BaseRelativeLayout()
		{
			this.layout = new RelativeLayout();
			this.aiLoading = new ActivityIndicator();
			this.scrolllayout = new ScrollView();
			this.scrolllayout.Content = this.layout;
			//this.scrolllayout.VerticalOptions = LayoutOptions.End;
			this.Content = this.scrolllayout;
		}
		/// <summary>
		/// Sets a View base percents 
		/// </summary>
		/// <returns>Nothing</returns>
		/// <param name="objView">View to Place</param>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void setCustomView(View objView, double x, double y, double width, double height)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToParent((parent) => { return parent.Width * x; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * y; }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * height; }));
		}
		/// <summary>
		/// Sets a View (square) with the same width / height
		/// </summary>
		/// <returns>The square view.</returns>
		/// <param name="objView">Object view.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		public void setSquareView(View objView, double x, double y, double width)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToParent((parent) => { return parent.Width * x; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * y; }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }));
		}
		/// <summary>
		/// Sets View (width/height)in middle of the screen
		/// </summary>
		/// <returns>The custom center view.</returns>
		/// <param name="objView">Object view.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void setCustomCenterView(View objView, double width, double height)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToParent((parent) => { return (parent.Width * .5) - ((parent.Width * width) / 2); }),
				Constraint.RelativeToParent((parent) => { return (parent.Height * .5) - ((parent.Height * height) / 2); }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * height; }));
		}
		/// <summary>
		/// Sets View in middle of width
		/// </summary>
		/// <returns>The custom horizontal center view.</returns>
		/// <param name="objView">Object view.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void setCustomHorizontalCenterView(View objView, double y, double width, double height)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToParent((parent) => { return (parent.Width * .5) - ((parent.Width * width) / 2); }),
				Constraint.RelativeToParent((parent) => { return (parent.Height * y); }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * height; }));
		}
		public void setCustomHorizontalCenterViewSquare(View objView, double y, double width)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToParent((parent) => { return (parent.Width * .5) - ((parent.Width * width) / 2); }),
				Constraint.RelativeToParent((parent) => { return (parent.Height * y); }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }));
		}
		/// <summary>
		/// Sets View in middle of height
		/// </summary>
		/// <returns>The custom horizontal center view.</returns>
		/// <param name="objView">Object view.</param>
		/// <param name="X">The X coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void setCustomVertialCenterView(View objView, double x, double width, double height)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToParent((parent) => { return (parent.Width * x); }),
				Constraint.RelativeToParent((parent) => { return (parent.Height * .5) - ((parent.Height * height) / 2); }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * height; }));
		}

		/// <summary>
		/// Sets the custom view relative.
		/// </summary>
		/// <returns>The custom view relative.</returns>
		/// <param name="targetObject">Target object.</param>
		/// <param name="objView">Object to place.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void setCustomViewRelative(View targetObject, View objView, double x, double y, double width, double height)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.X + view.Width + (view.Width * x); }),
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.Y + view.Height + (view.Height * y); }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * height; }));
		}
		public void setCustomViewRelativeNoHeight(View targetObject, View objView, double x, double y, double width)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.X + view.Width + (view.Width * x); }),
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.Y + view.Height + (view.Height * y); }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }));
		}
		public void setCustomViewRelativeNoHeightNoWidth(View targetObject, View objView, double x, double y, double width, double height)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.X + view.Width + (view.Width * x); }),
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.Y + view.Height + (view.Height * y); }));
		}
		public void setCustomConstraint(View objView, Constraint objConstraintX, Constraint objConstraintY, Constraint objConstraintWidth, Constraint objConstraintHeight)
		{
			this.layout.Children.Add(
				objView,
				objConstraintX,
				objConstraintY,
				objConstraintWidth,
				objConstraintHeight
			);
		}
		public void setCustomViewRelativeTopRightCenter(View targetObject, View objView, double width, double height)
		{
			this.layout.Children.Add(
				objView,
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.X + view.Width - ((view.Width * width) / 2); }),
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.Y - ((view.Height * height) / 2); }),
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.Width * width; }),
				Constraint.RelativeToView(targetObject, (parent, view) => { return view.Height * height; }));
		}
		public void setLoadingScreenSize(double width, double height)
		{
			this.layout.Children.Add(
				aiLoading,
				Constraint.RelativeToParent((parent) => { return (parent.Width * .5) - ((parent.Width * width) / 2); }),
				Constraint.RelativeToParent((parent) => { return (parent.Height * .5) - ((parent.Height * height) / 2); }),
				Constraint.RelativeToParent((parent) => { return parent.Width * width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * height; }));
		}
		public void waintingScreen(bool bEnble)
		{
			disableView(bEnble);
			runLoadingScreen(bEnble);
		}
		private void disableView(bool bEnable)
		{
			foreach (var item in layout.Children)
			{
				item.IsEnabled = !bEnable;
			}
		}
		private void runLoadingScreen(bool bEnble)
		{

			int iIndex = layout.Children.IndexOf(aiLoading);
			layout.Children[iIndex].IsEnabled = bEnble;
			aiLoading.IsRunning = bEnble;
		}


	}
}
