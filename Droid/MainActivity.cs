using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Graphics;

namespace AngleXplore.Droid
{
	[Activity(Label = "AngleXplore", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape,
	         Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
	public class MainActivity : Activity
	{
		

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			DrawView canvas = new DrawView(this);

			LinearLayout llCanvas = FindViewById<LinearLayout>(Resource.Id.llCanvas);
			llCanvas.AddView(canvas);

			Button btnClear = FindViewById<Button>(Resource.Id.btnClear);
			btnClear.Click += delegate {
				canvas.clear();
			};

			Button btnAngles = FindViewById<Button>(Resource.Id.btnAngles);
			btnAngles.Click += delegate
			{
				TextView angles = FindViewById<TextView>(Resource.Id.txtAngles);
				angles.Text = canvas.getAngles();
			};
		}
	}
}


