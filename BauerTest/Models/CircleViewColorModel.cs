using System;
using MonoTouch.UIKit;

namespace BauerTest
{
	public class CircleViewColorModel
	{

		private string _id, _title;

		private int _redValue, _greenValue, _blueValue;

		public CircleViewColorModel (string id, string title, int redValue, int greenValue, int blueValue)
		{
			_id = id;
			_title = title;
			_redValue = redValue;
			_greenValue = greenValue;
			_blueValue = blueValue;
		}


		public string ID
		{
			get{ return _id;}
		}

		public string Title
		{
			get{ return _title;}
		}

		/// <summary>
		/// Return a UIColor object
		/// </summary>
		/// <returns>The color.</returns>
		public UIColor randomColor()
		{

			return UIColor.FromRGB (_redValue, _greenValue, _blueValue);
		}
	}
}

