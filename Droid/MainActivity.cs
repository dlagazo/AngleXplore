using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Graphics;

namespace AngleXplore.Droid
{
	[Activity(Label = "AngleXplore", MainLauncher = false, Icon = "@mipmap/icon_big", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape,
	         Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
	public class MainActivity : Activity
	{

		bool show = false;
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
			Button btnExit = FindViewById<Button>(Resource.Id.btnExit);
			btnExit.Click += delegate
			{

				Finish();
			};

			Button btnAngles = FindViewById<Button>(Resource.Id.btnAngles);
			btnAngles.Click += delegate
			{
				if (!show)
				{
					
					show = true;
					btnAngles.Text = "Hide Angle Measurements";
					canvas.showAngles = true;
					canvas.status = 3;
					canvas.Invalidate();
				}
				else {
					
					btnAngles.Text = "Show Angle Measurements";
					show = false;
					canvas.showAngles = false;
					canvas.status = 3;
					canvas.Invalidate();
				}
				
			};

			Button btnUp = FindViewById<Button>(Resource.Id.btnUp);
			btnUp.Click += delegate
			{

				canvas.strokeWidth += 10;
				canvas.length *= 1.2f;
				canvas.multiplier += .1f;

					canvas.Invalidate();


			};

			Button btnDown = FindViewById<Button>(Resource.Id.btnDown);
			btnDown.Click += delegate
			{

				canvas.strokeWidth -= 10;
				canvas.length /= 1.2f;
				canvas.multiplier -= .1f;

				canvas.Invalidate();


			};


		}
	}
}


