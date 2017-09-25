
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
using System.Threading.Tasks;
using System.Threading;

namespace AngleXplore.Droid
{
	[Activity(Label = "AngleXplore", Icon = "@mipmap/icon_big", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape,
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

                EditText etAngle = FindViewById<EditText>(Resource.Id.etAngle);
                int angle = int.Parse(etAngle.Text);
                if(angle >= 90 && angle <=180)
                {
                    var activity2 = new Intent(this, typeof(MainActivity));
                    activity2.PutExtra("angle", angle);
                    StartActivity(activity2);

                    //StartActivity(typeof(MainActivity));
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Please input an angle between 90 and 180 degrees", ToastLength.Long).Show();

                }

            };
		}

        
    }
}
