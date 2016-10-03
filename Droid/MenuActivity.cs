
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AngleXplore.Droid
{
	[Activity(Label = "AngleXplore", MainLauncher = true, Icon = "@mipmap/icon_big", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape,
			 Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
	public class MenuActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Menu);
			// Create your application here
			Button btnStart = FindViewById<Button>(Resource.Id.btnStart);
			btnStart.Click += delegate
			{

				StartActivity(typeof(MainActivity));
				Finish();
			};
		}
	}
}
