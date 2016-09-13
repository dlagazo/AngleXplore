
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace AngleXplore.Droid
{
	public class MyCanvas : ImageView
	{
		public MyCanvas(Context context) :
			base(context)
		{
			Initialize();
		}


	}
}

