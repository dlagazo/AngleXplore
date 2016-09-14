
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
using Android.Graphics;

namespace AngleXplore.Droid
{
	public class DrawView : View//, View.IOnTouchListener
	{
		public int status = 0; //0-none, 1-clear, 2-pt1 down, -1 clear
		float strokeWidth = 60, length = 50;
		PointF pt1, pt2, pt1a, pt1b, pt2a, pt2b;
		public bool showAngles = false;

		public DrawView(Context context) :
			base(context)
		{
			//Initialize();
			this.SetBackgroundColor(Color.White);
			var metrics = Resources.DisplayMetrics;
			strokeWidth = ConvertPixelsToDp(metrics.WidthPixels)/30;
			length = (float)(ConvertPixelsToDp(metrics.HeightPixels)/1.5);


		}

		public string getAngles()
		{


			float A = (float)(Math.Atan2(pt1a.Y - pt1b.Y, pt1a.X - pt1b.X)*360/Math.PI);

			float B = (float)(Math.Atan2(pt2a.Y - pt2b.Y, pt2a.X - pt2b.X)*360/Math.PI);



			return "m < A = " + A + "    m < B = " + B;
		}

		private float ConvertPixelsToDp(float pixelValue)
		{
			var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
			return dp;
		}
		/*
		public bool OnTouch(View v, MotionEvent e)
		{
			switch (e.Action)
			{
				case MotionEventActions.Down:
					PointF touch = new PointF(e.GetX(), e.GetY());
					if (Math.Abs(touch.X - pt1.X) < 30 && Math.Abs(touch.Y - pt1.Y) < 30)
					{
						status = 2;
						Log.Debug("AngleXPlore", "status:2");

						break;
					}
					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());
					Log.Debug("AngleXPlore", "pt1x:" + pt1.X + " pt1y:" + pt1.Y);
					break;

				case MotionEventActions.Move:
					Log.Debug("AngleXPlore", "status:move");

					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());

					break;
				case MotionEventActions.Up:
					if (status == 2)
					{
						pt1.X = e.GetX();

						pt1.Y = e.GetY();
						status = 3;
						Log.Debug("AngleXPlore", "status:3");


					}
					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());
					Log.Debug("AngleXPlore", "pt1x:" + pt1.X + " pt1y:" + pt1.Y);
					break;

			}
			return true;
		}*/

		public DrawView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			
		}

		public DrawView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			
		}

		public void clear()
		{
			//this.width = _width;
			//this.height = _height;
			status = 1;
			this.Invalidate();

		}

		protected override void OnDraw(Canvas canvas)
		{
			
			if (status == 1)
			{
				Paint paint = new Paint();
				paint.Color = Color.White;
				paint.SetStyle(Paint.Style.Fill);
				canvas.DrawRect(canvas.ClipBounds, paint);
				Log.Debug("AngleXPlore", "status:1");

				status = 0;
				this.Invalidate();

			}
			else if (status == 0)
			{
				Paint paint = new Paint();
				paint.Color = Color.Black;
				paint.SetStyle(Paint.Style.Stroke);
				paint.StrokeWidth = strokeWidth;
				int width = canvas.ClipBounds.Width();
				int height = canvas.ClipBounds.Height();
				pt1 = new PointF(width / 4, height / 2);
				pt2 = new PointF(width / 4 * 3, height / 2);
				pt1a = new PointF(pt1.X + length, pt1.Y);
				pt1b = new PointF(pt1.X, pt1.Y - length);
				pt2a = new PointF(pt2.X + length, pt2.Y);
				pt2b = new PointF(pt2.X, pt2.Y - length);
				canvas.DrawPoint(pt1.X, pt1.Y, paint);
				canvas.DrawPoint(pt2.X, pt2.Y, paint);
				paint.StrokeWidth = strokeWidth / 2;
				paint.Color = Color.DarkGray;
				canvas.DrawLine(pt1.X, pt1.Y, pt1a.X, pt1a.Y, paint);
				canvas.DrawLine(pt1.X, pt1.Y, pt1b.X, pt1b.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2a.X, pt2a.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2b.X, pt2b.Y, paint);

				Log.Debug("AngleXPlore", "status:0");

			}
			else if (status == 3 || status == 5 || status == 7 || status == 9 || status == 11 || status == 13)
			{
				Paint paint = new Paint();
				paint.Color = Color.White;
				paint.SetStyle(Paint.Style.Fill);
				canvas.DrawRect(canvas.ClipBounds, paint);


				paint.Color = Color.Black;
				paint.SetStyle(Paint.Style.Stroke);
				paint.StrokeWidth = strokeWidth;
				canvas.DrawPoint(pt1.X, pt1.Y, paint);
				canvas.DrawPoint(pt2.X, pt2.Y, paint);
				paint.StrokeWidth = strokeWidth / 2;
				paint.Color = Color.DarkGray;

				canvas.DrawLine(pt1.X, pt1.Y, pt1a.X, pt1a.Y, paint);
				canvas.DrawLine(pt1.X, pt1.Y, pt1b.X, pt1b.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2a.X, pt2a.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2b.X, pt2b.Y, paint);
				status = -1;
				//Log.Debug("AngleXPlore", "status:3");

			}
			if (showAngles)
			{
				Paint paint = new Paint();
				paint.SetStyle(Paint.Style.Fill);
				paint.Color = Color.Black;
				//paint.StrokeWidth = (float)(strokeWidth / 2.5);
				paint.TextSize = 30;
				paint.TextAlign = Paint.Align.Left;

				canvas.DrawText(getAngles(), 50, 50, paint);
			}

			//_shape.Draw(canvas);

			//canvas.DrawPoint

		}

		public override bool OnTouchEvent(MotionEvent e)
		{



			switch (e.Action)
			{

				case MotionEventActions.Down:
					System.Console.WriteLine("Down");
					PointF touch = new PointF(e.GetX(), e.GetY());
					if (Math.Abs(touch.X - pt1.X) < strokeWidth && Math.Abs(touch.Y - pt1.Y) < strokeWidth)
					{
						status = 2;
						Log.Debug("AngleXPlore", "status:Point1 touched");


					}
					else if (Math.Abs(touch.X - pt2.X) < strokeWidth && Math.Abs(touch.Y - pt2.Y) < strokeWidth)
					{
						status = 4;
						Log.Debug("AngleXPlore", "status:Point2 touched");


					}
					else if (Math.Abs(touch.X - pt1a.X) < strokeWidth && Math.Abs(touch.Y - pt1a.Y) < strokeWidth)
					{
						status = 6;
						Log.Debug("AngleXPlore", "status:Ray1 touched");


					}
					else if (Math.Abs(touch.X - pt1b.X) < strokeWidth && Math.Abs(touch.Y - pt1b.Y) < strokeWidth)
					{
						status = 8;
						Log.Debug("AngleXPlore", "status:Ray2 touched");


					}
					else if (Math.Abs(touch.X - pt2a.X) < strokeWidth && Math.Abs(touch.Y - pt2a.Y) < strokeWidth)
					{
						status = 10;
						Log.Debug("AngleXPlore", "status:Ray1 touched");


					}
					else if (Math.Abs(touch.X - pt2b.X) < strokeWidth && Math.Abs(touch.Y - pt2b.Y) < strokeWidth)
					{
						status = 12;
						Log.Debug("AngleXPlore", "status:Ray2 touched");


					}
					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());
					Log.Debug("AngleXPlore", "pt1x:" + pt1.X + " pt1y:" + pt1.Y);
					Log.Debug("AngleXPlore", "pt2x:" + pt2.X + " pt2y:" + pt2.Y);

					break;

				case MotionEventActions.Up:
					System.Console.WriteLine("Up");
					if (status == 2)
					{
						float xdiff = pt1.X - e.GetX();
						float ydiff = pt1.Y - e.GetY();
						pt1a.X -= xdiff;
						pt1a.Y -= ydiff;
						pt1b.X -= xdiff;
						pt1b.Y -= ydiff;

						pt1.X = e.GetX();

						pt1.Y = e.GetY();
						status = 3;
						Log.Debug("AngleXPlore", "status:Point1 moved");
						this.Invalidate();

					}
					else if (status == 4)
					{
						float xdiff = pt2.X - e.GetX();
						float ydiff = pt2.Y - e.GetY();
						pt2a.X -= xdiff;
						pt2a.Y -= ydiff;
						pt2b.X -= xdiff;
						pt2b.Y -= ydiff;


						pt2.X = e.GetX();

						pt2.Y = e.GetY();
						status = 5;
						Log.Debug("AngleXPlore", "status:Point2 moved");
						this.Invalidate();

					}
					else if (status == 6)
					{
						pt1a.X = e.GetX();

						pt1a.Y = e.GetY();
						status = 7;
						Log.Debug("AngleXPlore", "status:Ray1 moved");
						this.Invalidate();

					}
					else if (status == 8)
					{
						pt1b.X = e.GetX();

						pt1b.Y = e.GetY();
						status = 9;
						Log.Debug("AngleXPlore", "status:Ray2 moved");
						this.Invalidate();

					}
					else if (status == 10)
					{
						pt2a.X = e.GetX();

						pt2a.Y = e.GetY();
						status = 11;
						Log.Debug("AngleXPlore", "status:Ray3 moved");
						this.Invalidate();

					}
					else if (status == 12)
					{
						pt2b.X = e.GetX();

						pt2b.Y = e.GetY();
						status = 13;
						Log.Debug("AngleXPlore", "status:Ray4 moved");
						this.Invalidate();

					}

					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());
					Log.Debug("AngleXPlore", "pt1x:" + pt1.X + " pt1y:" + pt1.Y);
					Log.Debug("AngleXPlore", "pt2x:" + pt2.X + " pt2y:" + pt2.Y);

					break;

				case MotionEventActions.Move:
					//int x_cord = (int)m_event.GetX;
					//  int y_cord = (int)m_event.GetY;
					//System.Console.WriteLine("Move");
					break;

			}
			return true;
		}
		/*
		public override bool OnTouchEvent(MotionEvent e)
		{
			switch (e.Action)
			{
				case MotionEventActions.Down:
					System.Console.WriteLine("Down");
					break;

					PointF touch = new PointF(e.GetX(), e.GetY());
					if (Math.Abs(touch.X - pt1.X) < 30 && Math.Abs(touch.Y - pt1.Y) < 30)
					{
						status = 2;
						Log.Debug("AngleXPlore", "status:2");


					}
					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());
					Log.Debug("AngleXPlore", "pt1x:" + pt1.X + " pt1y:" + pt1.Y);

				case MotionEventActions.Move:
					System.Console.WriteLine("Move");
					break;

					Log.Debug("AngleXPlore", "status:move");

					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());

				case MotionEventActions.Up:
					System.Console.WriteLine("Up");
					break;

					if (status == 2)
					{
						pt1.X = e.GetX();

						pt1.Y = e.GetY();
						status = 3;
						Log.Debug("AngleXPlore", "status:3");


					}
					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());
					Log.Debug("AngleXPlore", "pt1x:" + pt1.X + " pt1y:" + pt1.Y);

			}




			return base.OnTouchEvent(e);
		}*/


	}
}

