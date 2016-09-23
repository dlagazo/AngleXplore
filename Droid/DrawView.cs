
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
		public int moveStatus = 0;
		float strokeWidth = 100, length = 100, multiplier = 1.2f;
		PointF pt1, pt2, pt1a, pt1b, pt2a, pt2b;
		public bool showAngles = false;
		Context context;
		long vibrateLength = 50;

		public DrawView(Context _context) :
			base(_context)
		{
			//Initialize();
			context = _context;
			this.SetBackgroundColor(Color.White);
			var metrics = Resources.DisplayMetrics;
			strokeWidth = ConvertPixelsToDp(metrics.WidthPixels)/30;
			length = (float)(ConvertPixelsToDp(metrics.HeightPixels)/1.5);


		}

		public string getAngles()
		{


			float A = (float)(Math.Atan2(pt1a.Y - pt1b.Y, pt1a.X - pt1b.X)*360/Math.PI);

			float B = (float)(Math.Atan2(pt2a.Y - pt2b.Y, pt2a.X - pt2b.X)*360/Math.PI);

			float length1A = (float)Math.Sqrt(Math.Pow((pt1a.X - pt1.X), 2) + Math.Pow((pt1a.Y - pt1.Y), 2));
			float length1B = (float)Math.Sqrt(Math.Pow((pt1b.X - pt1.X), 2) + Math.Pow((pt1b.Y - pt1.Y), 2));
			float length1C = (float)Math.Sqrt(Math.Pow(pt1b.X-pt1a.X, 2) + Math.Pow(pt1b.Y - pt1a.Y,2));
			float angleA = (float)(Math.Acos(((length1A * length1A) + (length1B * length1B) - (length1C * length1C)) / (2 * length1A * length1B))*180/Math.PI);

			float length2A = (float)Math.Sqrt(Math.Pow((pt2a.X - pt2.X), 2) + Math.Pow((pt2a.Y - pt2.Y), 2));
			float length2B = (float)Math.Sqrt(Math.Pow((pt2b.X - pt2.X), 2) + Math.Pow((pt2b.Y - pt2.Y), 2));
			float length2C = (float)Math.Sqrt(Math.Pow(pt2b.X - pt2a.X, 2) + Math.Pow(pt2b.Y - pt2a.Y, 2));
			float angleB = (float)(Math.Acos(((length2A * length2A) + (length2B * length2B) - (length2C * length2C)) / (2 * length2A * length2B)) * 180 / Math.PI);



			Console.WriteLine("length1A:" + length1A);
			Console.WriteLine("length1B:" + length1B);
			Console.WriteLine("length1C:" + length1C);

			Console.WriteLine("arcA:" + angleA);

			Console.WriteLine("length2A:" + length2A);
			Console.WriteLine("length2B:" + length2B);
			Console.WriteLine("length2C:" + length2C);

			Console.WriteLine("arcB:" + angleB);
			return "m < A = " + angleA + ",m < B = " + angleB;
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
				paint.SetStyle(Paint.Style.FillAndStroke);
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

				paint.Color = Color.Yellow;
				canvas.DrawCircle(pt1a.X, pt1a.Y, (float)(strokeWidth / 2.5), paint);
				paint.Color = Color.Violet;
				canvas.DrawCircle(pt1b.X, pt1b.Y, (float)(strokeWidth / 2.5), paint);

				paint.Color = Color.Pink;
				canvas.DrawCircle(pt2a.X, pt2a.Y, (float)(strokeWidth / 2.5), paint);
				paint.Color = Color.Cyan;
				canvas.DrawCircle(pt2b.X, pt2b.Y, (float)(strokeWidth / 2.5), paint);

				Log.Debug("AngleXPlore", "status:0");

			}
			else if (status == 3 || status == 5 || status == 7 || status == 9 || status == 11 || status == 13)
			{
				Paint paint = new Paint();
				paint.Color = Color.White;
				paint.SetStyle(Paint.Style.Fill);
				canvas.DrawRect(canvas.ClipBounds, paint);


				paint.Color = Color.Black;
				paint.SetStyle(Paint.Style.FillAndStroke);
				paint.StrokeWidth = strokeWidth;
				canvas.DrawPoint(pt1.X, pt1.Y, paint);
				canvas.DrawPoint(pt2.X, pt2.Y, paint);
				paint.StrokeWidth = strokeWidth / 2;
				paint.Color = Color.DarkGray;

				canvas.DrawLine(pt1.X, pt1.Y, pt1a.X, pt1a.Y, paint);
				canvas.DrawLine(pt1.X, pt1.Y, pt1b.X, pt1b.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2a.X, pt2a.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2b.X, pt2b.Y, paint);

				paint.Color = Color.Yellow;
				canvas.DrawCircle(pt1a.X, pt1a.Y, (float)(strokeWidth / 2.5), paint);
				paint.Color = Color.Violet;
				canvas.DrawCircle(pt1b.X, pt1b.Y, (float)(strokeWidth / 2.5), paint);

				paint.Color = Color.Pink;
				canvas.DrawCircle(pt2a.X, pt2a.Y, (float)(strokeWidth / 2.5), paint);
				paint.Color = Color.Cyan;
				canvas.DrawCircle(pt2b.X, pt2b.Y, (float)(strokeWidth / 2.5), paint);


				status = -1;
				//Log.Debug("AngleXPlore", "status:3");

			}
			else
			{
				Paint paint = new Paint();
				paint.Color = Color.White;
				paint.SetStyle(Paint.Style.Fill);
				canvas.DrawRect(canvas.ClipBounds, paint);


				paint.Color = Color.Black;
				paint.SetStyle(Paint.Style.FillAndStroke);
				paint.StrokeWidth = strokeWidth;
				canvas.DrawPoint(pt1.X, pt1.Y, paint);
				canvas.DrawPoint(pt2.X, pt2.Y, paint);
				paint.StrokeWidth = strokeWidth / 2;
				paint.Color = Color.DarkGray;

				canvas.DrawLine(pt1.X, pt1.Y, pt1a.X, pt1a.Y, paint);
				canvas.DrawLine(pt1.X, pt1.Y, pt1b.X, pt1b.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2a.X, pt2a.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2b.X, pt2b.Y, paint);

				paint.Color = Color.Yellow;
				canvas.DrawCircle(pt1a.X, pt1a.Y, (float)(strokeWidth / 2.5), paint);
				paint.Color = Color.Violet;
				canvas.DrawCircle(pt1b.X, pt1b.Y, (float)(strokeWidth / 2.5), paint);

				paint.Color = Color.Pink;
				canvas.DrawCircle(pt2a.X, pt2a.Y, (float)(strokeWidth / 2.5), paint);
				paint.Color = Color.Cyan;
				canvas.DrawCircle(pt2b.X, pt2b.Y, (float)(strokeWidth / 2.5), paint);


				//Log.Debug("AngleXPlore", "status:3");

			}

			if (moveStatus == 1)
			{
				Paint paint = new Paint();
				paint.Color = Color.White;
				paint.SetStyle(Paint.Style.Fill);
				canvas.DrawRect(canvas.ClipBounds, paint);


				paint.Color = Color.Black;
				paint.SetStyle(Paint.Style.FillAndStroke);
				paint.StrokeWidth = strokeWidth;
				canvas.DrawPoint(pt1.X, pt1.Y, paint);
				canvas.DrawPoint(pt2.X, pt2.Y, paint);
				paint.StrokeWidth = strokeWidth / 2;
				paint.Color = Color.DarkGray;

				canvas.DrawLine(pt1.X, pt1.Y, pt1a.X, pt1a.Y, paint);
				canvas.DrawLine(pt1.X, pt1.Y, pt1b.X, pt1b.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2a.X, pt2a.Y, paint);
				canvas.DrawLine(pt2.X, pt2.Y, pt2b.X, pt2b.Y, paint);

				paint.Color = Color.Yellow;
				canvas.DrawCircle(pt1a.X, pt1a.Y, (float)(strokeWidth / 2.5), paint);
				paint.Color = Color.Violet;
				canvas.DrawCircle(pt1b.X, pt1b.Y, (float)(strokeWidth / 2.5), paint);

				paint.Color = Color.Pink;
				canvas.DrawCircle(pt2a.X, pt2a.Y, (float)(strokeWidth / 2.5), paint);
				paint.Color = Color.Cyan;
				canvas.DrawCircle(pt2b.X, pt2b.Y, (float)(strokeWidth / 2.5), paint);


				moveStatus = 0;
			}


			if (showAngles)
			{
				Paint paint = new Paint();
				paint.SetStyle(Paint.Style.Fill);
				paint.Color = Color.LightGreen;
				//paint.StrokeWidth = (float)(strokeWidth / 2.5);
				paint.TextSize = 30;
				paint.TextAlign = Paint.Align.Left;

				canvas.DrawText(getAngles().Split(',')[0], 50, 50, paint);
				paint.Color = Color.LightBlue;
				canvas.DrawText(getAngles().Split(',')[1], 300, 50, paint);
			}

			float ray2 = (float)(Math.Atan2(pt1.Y - pt1b.Y, pt1.X - pt1b.X) * 180 / Math.PI);
			float ray1 = (float)(Math.Atan2(pt1.Y - pt1a.Y, pt1.X - pt1a.X) * 180 / Math.PI);
			float ray4 = (float)(Math.Atan2(pt2.Y - pt2b.Y, pt2.X - pt2b.X) * 180 / Math.PI);
			float ray3 = (float)(Math.Atan2(pt2.Y - pt2a.Y, pt2.X - pt2a.X) * 180 / Math.PI);

			Log.Debug("AngleXPlore", "ray2:" + ray2);
			Log.Debug("AngleXPlore", "ray1:" + ray1);
			Log.Debug("AngleXPlore", "ray4:" + ray4);
			Log.Debug("AngleXPlore", "ray3:" + ray3);

			RectF arc1 = new RectF(pt1.X - 200, pt1.Y - 200, pt1.X + 200, pt1.Y + 200);
			Paint paintarc = new Paint();
			paintarc.SetStyle(Paint.Style.Fill);
			paintarc.SetARGB(100, 0, 200, 0);

			//paintarc.Color = Color.LightGreen;
			if (ray1 > ray2 && ray1 > 0 && ray2 > 0)
			{
				canvas.DrawArc(arc1, ray2 + 180, Math.Abs(ray2 - ray1), true, paintarc);

			}
			else if (ray1 < 0 && ray2 < 0 && ray1 < ray2)
			{
				canvas.DrawArc(arc1, 180 + ray1, Math.Abs(ray2 - ray1), true, paintarc);

			}
			else if (ray1 < 0 && ray2 < 0 && ray2 < ray1)
			{
				canvas.DrawArc(arc1, 180 + ray2, Math.Abs(ray2 - ray1), true, paintarc);

			}
			else if (ray1 > 0 && ray2 < 0 && ray1 == 180)
			{
				canvas.DrawArc(arc1, 0, Math.Abs(ray2 + ray1), true, paintarc);

			}
			else if (ray1 > 0 && ray2 < 0 && (ray1 - ray2) > 180)
			{
				canvas.DrawArc(arc1, 0, 180 + ray2, true, paintarc);
				canvas.DrawArc(arc1, 180 + ray1, 360 - (180 + ray1), true, paintarc);

			}
			else if (ray1 > 0 && ray2 < 0 && (ray1 - ray2) < 180)
			{
				canvas.DrawArc(arc1, 180 + ray2, ray1 - ray2, true, paintarc);


			}
			else if (ray1 > 0 && ray2 > 0 && ray2 > ray1)
			{
				canvas.DrawArc(arc1, ray1 + 180, Math.Abs(ray2 - ray1), true, paintarc);

			}
			else if (ray1 < 0 && ray2 > 0 && (ray2 - ray1) < 180)
			{
				canvas.DrawArc(arc1, 180 + ray1, ray2 - ray1, true, paintarc);

			}
			else if (ray1 < 0 && ray2 > 0 && (ray2 - ray1) > 180)
			{
				canvas.DrawArc(arc1, 0, 180+ray1, true, paintarc);
				canvas.DrawArc(arc1, 180+ray2, 360-(180 + ray2), true, paintarc);

			}

			RectF arc2 = new RectF(pt2.X - 200, pt2.Y - 200, pt2.X + 200, pt2.Y + 200);
			paintarc.SetARGB(100, 0, 0, 100);
			if (ray3 > ray4 && ray3 > 0 && ray4 > 0)
			{
				canvas.DrawArc(arc2, ray4 + 180, Math.Abs(ray4 - ray3), true, paintarc);

			}
			else if (ray3 < 0 && ray4 < 0 && ray3 < ray4)
			{
				canvas.DrawArc(arc2, 180 + ray3, Math.Abs(ray4 - ray3), true, paintarc);

			}
			else if (ray3 < 0 && ray4 < 0 && ray4 < ray3)
			{
				canvas.DrawArc(arc2, 180 + ray4, Math.Abs(ray4 - ray3), true, paintarc);

			}
			else if (ray3 > 0 && ray4 < 0 && ray3 == 180)
			{
				canvas.DrawArc(arc2, 0, Math.Abs(ray4 + ray3), true, paintarc);

			}
			else if (ray3 > 0 && ray4 < 0 && (ray3 - ray4) > 180)
			{
				canvas.DrawArc(arc2, 0, 180 + ray4, true, paintarc);
				canvas.DrawArc(arc2, 180 + ray3, 360 - (180 + ray3), true, paintarc);

			}
			else if (ray3 > 0 && ray4 < 0 && (ray3 - ray4) < 180)
			{
				canvas.DrawArc(arc2, 180 + ray4, ray3 - ray4, true, paintarc);


			}
			else if (ray3 > 0 && ray4 > 0 && ray4 > ray3)
			{
				canvas.DrawArc(arc2, ray3 + 180, Math.Abs(ray4 - ray3), true, paintarc);

			}
			else if (ray3 < 0 && ray4 > 0 && (ray4 - ray3) > 180)
			{
				canvas.DrawArc(arc2, 0, 180+ray3, true, paintarc);
				canvas.DrawArc(arc2, 180+ray4, 360-180 - ray4, true, paintarc);

			}
			else if (ray3 < 0 && ray4 > 0 && (ray4 - ray3) < 180)
			{
				canvas.DrawArc(arc2, 180 + ray3, ray4 - ray3, true, paintarc);

			}




			//canvas.Draw	

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

						Vibrator.FromContext(context).Vibrate(vibrateLength);


					}
					else if (Math.Abs(touch.X - pt2.X) < strokeWidth && Math.Abs(touch.Y - pt2.Y) < strokeWidth)
					{
						status = 4;
						Log.Debug("AngleXPlore", "status:Point2 touched");
						Vibrator.FromContext(context).Vibrate(vibrateLength);


					}
					else if (Math.Abs(touch.X - pt1a.X) < strokeWidth && Math.Abs(touch.Y - pt1a.Y) < strokeWidth)
					{
						status = 6;
						Log.Debug("AngleXPlore", "status:Ray1 touched");
						Vibrator.FromContext(context).Vibrate(vibrateLength);


					}
					else if (Math.Abs(touch.X - pt1b.X) < strokeWidth && Math.Abs(touch.Y - pt1b.Y) < strokeWidth)
					{
						status = 8;
						Log.Debug("AngleXPlore", "status:Ray2 touched");
						Vibrator.FromContext(context).Vibrate(vibrateLength);


					}
					else if (Math.Abs(touch.X - pt2a.X) < strokeWidth && Math.Abs(touch.Y - pt2a.Y) < strokeWidth)
					{
						status = 10;
						Log.Debug("AngleXPlore", "status:Ray1 touched");
						Vibrator.FromContext(context).Vibrate(vibrateLength);


					}
					else if (Math.Abs(touch.X - pt2b.X) < strokeWidth && Math.Abs(touch.Y - pt2b.Y) < strokeWidth)
					{
						status = 12;
						Log.Debug("AngleXPlore", "status:Ray2 touched");
						Vibrator.FromContext(context).Vibrate(vibrateLength);


					}
					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());
					Log.Debug("AngleXPlore", "pt1x:" + pt1.X + " pt1y:" + pt1.Y);
					Log.Debug("AngleXPlore", "pt2x:" + pt2.X + " pt2y:" + pt2.Y);

					break;

				case MotionEventActions.Move:
					System.Console.WriteLine("Move");
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
						moveStatus = 1;
						this.Invalidate();
						System.Console.WriteLine("Point1 is being dragged");

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
						moveStatus = 1;
						System.Console.WriteLine("Point2 is being dragged");
						this.Invalidate();

					}
					else if (status == 6)
					{

							pt1a.X = e.GetX();

							pt1a.Y = e.GetY();




						moveStatus = 1;
						double angle = (double)(Math.Atan2(e.GetY() - pt1.Y, e.GetX() - pt1.X) * 180 / Math.PI);

						System.Console.WriteLine("Ray1 is being dragged at angle:" + angle);
						this.Invalidate();

					}
					else if (status == 8)
					{
						pt1b.X = e.GetX();

						pt1b.Y = e.GetY();


						moveStatus = 1;
						System.Console.WriteLine("Ray2 is being dragged");
						this.Invalidate();

					}
					else if (status == 10)
					{
						pt2a.X = e.GetX();

						pt2a.Y = e.GetY();


						moveStatus = 1;
						System.Console.WriteLine("Ray3 is being dragged");
						this.Invalidate();

					}
					else if (status == 12)
					{
						pt2b.X = e.GetX();

						pt2b.Y = e.GetY();


						moveStatus = 1;
						System.Console.WriteLine("Ray4 is being dragged");
						this.Invalidate();

					}
				
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

						//float lengthRay = (float)(Math.Sqrt(Math.Pow(e.GetX() - pt1.X, 2) + Math.Pow(e.GetY() - pt1.Y,2)));
						/*
						if (lengthRay < 350)
						{
							//y = mx+b
							//sqrt((y-y1)^2 + (x-x1)^2) = 350
							//(x-x1)^2 + (y-y1)^2 = 350^2
							//(x-n)^2 + (y-mn)^2 = 350^2
							//x^2 -2nx + n^2 + y^2 -2mny + mn^2 = 350^2
							//n^2 + mn^2 -2mny -2nx = 350^2 + x^2 + y^2
							//n(n+mn -2my -2x)
							float slope = (float)(
						}*/

							//pt1a.X = e.GetX();

							//pt1a.Y = e.GetY();
						double angle = (double)(Math.Atan2(e.GetY() - pt1.Y, e.GetX() - pt1.X) * 180 / Math.PI);
						float a = (float)Math.Sin(angle*Math.PI/180) * length;
						float b = (float)Math.Sqrt((length * length) - (a * a));
						if (angle > 0 && angle < 90)
						{
							pt1a.X = pt1.X + b;
							pt1a.Y = pt1.Y + a;
						}
						else if (angle > 90 && angle < 180)
						{
							pt1a.X = pt1.X - b;
							pt1a.Y = pt1.Y + a;
						}
						else if (angle < 0 && angle > -90)
						{
							pt1a.X = pt1.X + b;
							pt1a.Y = pt1.Y + a;
						}
						else if (angle < -90 && angle > -180)
						{
							pt1a.X = pt1.X - b;
							pt1a.Y = pt1.Y + a;
						}
						status = 7;
						Log.Debug("AngleXPlore", "status:Ray1 moved.");
						this.Invalidate();

					}
					else if (status == 8)
					{
						double angle = (double)(Math.Atan2(e.GetY() - pt1.Y, e.GetX() - pt1.X) * 180 / Math.PI);
						float a = (float)(Math.Sin(angle * Math.PI / 180) * length*multiplier);
						float b = (float)Math.Sqrt((length * length*multiplier*multiplier) - (a * a));
						if (angle > 0 && angle < 90)
						{
							pt1b.X = pt1.X + b;
							pt1b.Y = pt1.Y + a;
						}
						else if (angle > 90 && angle < 180)
						{
							pt1b.X = pt1.X - b;
							pt1b.Y = pt1.Y + a;
						}
						else if (angle < 0 && angle > -90)
						{
							pt1b.X = pt1.X + b;
							pt1b.Y = pt1.Y + a;
						}
						else if (angle < -90 && angle > -180)
						{
							pt1b.X = pt1.X - b;
							pt1b.Y = pt1.Y + a;
						}
						status = 9;
						Log.Debug("AngleXPlore", "status:Ray2 moved");
						this.Invalidate();

					}
					else if (status == 10)
					{
						double angle = (double)(Math.Atan2(e.GetY() - pt2.Y, e.GetX() - pt2.X) * 180 / Math.PI);
						float a = (float)(Math.Sin(angle * Math.PI / 180) * length * multiplier);
						float b = (float)Math.Sqrt((length * length * multiplier * multiplier) - (a * a));
						if (angle > 0 && angle < 90)
						{
							pt2a.X = pt2.X + b;
							pt2a.Y = pt2.Y + a;
						}
						else if (angle > 90 && angle < 180)
						{
							pt2a.X = pt2.X - b;
							pt2a.Y = pt2.Y + a;
						}
						else if (angle < 0 && angle > -90)
						{
							pt2a.X = pt2.X + b;
							pt2a.Y = pt2.Y + a;
						}
						else if (angle < -90 && angle > -180)
						{
							pt2a.X = pt2.X - b;
							pt2a.Y = pt2.Y + a;
						}
						status = 11;
						Log.Debug("AngleXPlore", "status:Ray3 moved");
						this.Invalidate();

					}
					else if (status == 12)
					{
						double angle = (double)(Math.Atan2(e.GetY() - pt2.Y, e.GetX() - pt2.X) * 180 / Math.PI);
						float a = (float)(Math.Sin(angle * Math.PI / 180) * length * multiplier);
						float b = (float)Math.Sqrt((length * length * multiplier * multiplier) - (a * a));
						if (angle > 0 && angle < 90)
						{
							pt2b.X = pt2.X + b;
							pt2b.Y = pt2.Y + a;
						}
						else if (angle > 90 && angle < 180)
						{
							pt2b.X = pt2.X - b;
							pt2b.Y = pt2.Y + a;
						}
						else if (angle < 0 && angle > -90)
						{
							pt2b.X = pt2.X + b;
							pt2b.Y = pt2.Y + a;
						}
						else if (angle < -90 && angle > -180)
						{
							pt2b.X = pt2.X - b;
							pt2b.Y = pt2.Y + a;
						}
						status = 13;
						Log.Debug("AngleXPlore", "status:Ray4 moved");
						this.Invalidate();

					}

					Log.Debug("AngleXPlore", "x:" + e.GetX() + " y:" + e.GetY());
					Log.Debug("AngleXPlore", "pt1x:" + pt1.X + " pt1y:" + pt1.Y);
					Log.Debug("AngleXPlore", "pt2x:" + pt2.X + " pt2y:" + pt2.Y);

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

