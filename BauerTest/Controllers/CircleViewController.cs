
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BauerTest
{
	public partial class CircleViewController : UIViewController
	{

		//CircleView circleView;	
		private const int  CIRCLE_VIEW_CONTROLLER_TAG = 1;

		public CircleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			createCircle (View.Bounds.Width / 2, View.Bounds.Height / 2);

		}

		public override void TouchesBegan(MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch.View.Tag == CIRCLE_VIEW_CONTROLLER_TAG) {

				createCircle(touch.LocationInView(this.View).X, touch.LocationInView(this.View).Y);
			} 
		}

		public void createCircle(float xLocation, float yLocation)
		{
			UIView.AnimateAsync (0.3, () => {

				CircleView	circleView = new CircleView (xLocation,yLocation);
				circleView.changeBackgroupColor ();
				View.AddSubview(circleView); 
			});
		}
	}
}

