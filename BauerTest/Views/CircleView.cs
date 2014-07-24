using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Threading.Tasks;



namespace BauerTest
{
	public class CircleView :UIView
	{
		Boolean isTitleShowed;
		UILabel titleLabel;
		CircleViewColorModel color;
		private bool isSingleTap;

		public CircleView (float x, float y)
		{
			float width = 100;
			float height = width;
			// corner radius needs to be one half of the size of the view
			float cornerRadius = width / 2;


			this.Frame =  new RectangleF(x - width/2, y-height/2, width, height);
			// set corner radius
			this.Layer.CornerRadius = cornerRadius;
			this.Layer.CornerRadius = cornerRadius;
			this.MultipleTouchEnabled = true;

			// Add title lable
			isTitleShowed = false;
			titleLabel = new UILabel{
				Frame = new  RectangleF(0,this.Frame.Height/2-10, this.Frame.Width, 20)
			};
					
			//titleLabel.Center = this.Center;
			titleLabel.Font = UIFont.FromName("Helvetica",20f);
			titleLabel.AdjustsFontSizeToFitWidth = true;
			titleLabel.Hidden = true;
			titleLabel.Lines = 1;
			titleLabel.TextColor = UIColor.Black;
			titleLabel.TextAlignment = UITextAlignment.Center;
			this.AddSubview(titleLabel);
		}
			
		/// <summary>
		/// Wait for a while to determine it's single tap or double tap
		/// </summary>
		async public void SingleTap()
		{
			isSingleTap = true;
			await Task.Delay (300);

			if (isSingleTap) {
				tapCount ();
			}
		}

		public override void TouchesBegan(MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{

			base.TouchesBegan (touches, evt);

			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null) {
				//Double tap show and hide title
				if (touch.TapCount == 1  ) {
					SingleTap ();

				} else if (touch.TapCount == 2 ) {
					isSingleTap = false;
					tapCount ();
				}
			}
		}

		private void tapCount()
		{
			if (isSingleTap) {
				//Console.WriteLine ("Single Tap");
				this.changeBackgroupColor ();
			} else {
				//Console.WriteLine ("Double Tap");
				this.showTitle ();
			}
		} 
			
		public override void TouchesMoved(MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);

			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null  ) {
				// move the shape
				float offsetX = touch.PreviousLocationInView(this).X - touch.LocationInView(this).X;
				float offsetY = touch.PreviousLocationInView(this).Y - touch.LocationInView(this).Y;
				this.Frame = new RectangleF(new PointF(this.Frame.X - offsetX, this.Frame.Y - offsetY), this.Frame.Size);
			}

		}

		public void showTitle()
		{
			UIView.Animate (0.3, () => {
				if (isTitleShowed) {
					titleLabel.Hidden = true;
				} else {
					titleLabel.Hidden = false;
				}
			});
			isTitleShowed = !isTitleShowed;
		}


		/// <summary>
		/// Changes the color of the backgroup.
		/// </summary>
		public async void changeBackgroupColor ()
		{
			Task<CircleViewColorModel> taskColor =  CircleViewModel.parseXML ();
			color = await taskColor;
			//slowly show the color;
			animateShowBackgoundColor ();
		}

		 private void animateShowBackgoundColor()
		{
			 UIView.AnimateAsync (0.3, () => {
				this.BackgroundColor = color.randomColor ();
				titleLabel.Text = color.Title;
			});
		}


	}
}

