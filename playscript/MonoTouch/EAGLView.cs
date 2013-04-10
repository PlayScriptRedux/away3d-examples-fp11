using System;
using System.Diagnostics;

using OpenTK;
using OpenTK.Graphics.ES20;
using GL1 = OpenTK.Graphics.ES11.GL;
using All1 = OpenTK.Graphics.ES11.All;
using OpenTK.Platform.iPhoneOS;

using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using MonoTouch.ObjCRuntime;
using MonoTouch.OpenGLES;
using MonoTouch.UIKit;

using flash.display;
using flash.display3D;

namespace Away3DApp
{
	[Register ("EAGLView")]
	public class EAGLView : iPhoneOSGameView
	{
		// this is our flash display stage
		flash.display.Stage    mStage;
        flash.display.Sprite   mDemoSprite;

		[Export("initWithCoder:")]
		public EAGLView (NSCoder coder) : base (coder)
		{
			LayerRetainsBacking = true;
			LayerColorFormat = EAGLColorFormat.RGBA8;
		}
		
		[Export ("layerClass")]
		public static new Class GetLayerClass ()
		{
			return iPhoneOSGameView.GetLayerClass ();
		}
		
		protected override void ConfigureLayer (CAEAGLLayer eaglLayer)
		{
			eaglLayer.Opaque = true;
		}
		
		protected override void CreateFrameBuffer ()
		{
			try {
				ContextRenderingApi = EAGLRenderingAPI.OpenGLES2;
				base.CreateFrameBuffer ();
			} catch (Exception) {
				ContextRenderingApi = EAGLRenderingAPI.OpenGLES1;
				base.CreateFrameBuffer ();
			}
		}
		
		protected override void DestroyFrameBuffer ()
		{
			base.DestroyFrameBuffer ();
		}


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
		
			foreach (UITouch touch in touches) {
				var p = touch.LocationInView(this);
				// Console.WriteLine ("touches-began {0}", p);

				if (mStage!=null) 
				{
					mStage.mouseX = p.X;
					mStage.mouseY = p.Y;

					// dispatch touch event
					var te = new flash.events.TouchEvent(flash.events.TouchEvent.TOUCH_BEGIN, true, false, 0, true, p.X, p.Y, 1.0, 1.0, 1.0 );
					mStage.dispatchEvent (te);

					// dispatch mouse event
					var me = new flash.events.MouseEvent(flash.events.MouseEvent.MOUSE_DOWN, true, false, p.X, p.Y, mStage);
					mStage.dispatchEvent (me);
				}

			}
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);
		
			foreach (UITouch touch in touches) {
				var p = touch.LocationInView(this);
				// Console.WriteLine ("touches-moved {0}", p);

				if (mStage!=null) 
				{
					mStage.mouseX = p.X;
					mStage.mouseY = p.Y;

					// dispatch touch event
					var te = new flash.events.TouchEvent(flash.events.TouchEvent.TOUCH_MOVE, true, false, 0, true, p.X, p.Y, 1.0, 1.0, 1.0 );
					mStage.dispatchEvent (te);

					// dispatch mouse event
					var me = new flash.events.MouseEvent(flash.events.MouseEvent.MOUSE_MOVE, true, false, p.X, p.Y, mStage);
					mStage.dispatchEvent (me);
				}
			}
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);

			foreach (UITouch touch in touches) {
				var p = touch.LocationInView(this);
				// Console.WriteLine ("touches-ended {0}", p);

				if (mStage!=null) 
				{
					mStage.mouseX = p.X;
					mStage.mouseY = p.Y;

					// dispatch touch event
					var te = new flash.events.TouchEvent(flash.events.TouchEvent.TOUCH_END, true, false, 0, true, p.X, p.Y, 1.0, 1.0, 1.0 );
					mStage.dispatchEvent (te);

					// dispatch mouse event
					var me = new flash.events.MouseEvent(flash.events.MouseEvent.MOUSE_UP, true, false, p.X, p.Y, mStage);
					mStage.dispatchEvent (me);
				}
			}
		}


		
		#region DisplayLink support
		
		int frameInterval;
		CADisplayLink displayLink;
		
		public bool IsAnimating { get; private set; }
		
		// How many display frames must pass between each time the display link fires.
		public int FrameInterval {
			get {
				return frameInterval;
			}
			set {
				if (value <= 0)
					throw new ArgumentException ();
				frameInterval = value;
				if (IsAnimating) {
					StopAnimating ();
					StartAnimating ();
				}
			}
		}
		
		public void StartAnimating ()
		{
			if (IsAnimating)
				return;
			
			CreateFrameBuffer ();
			CADisplayLink displayLink = UIScreen.MainScreen.CreateDisplayLink (this, new Selector ("drawFrame"));
			displayLink.FrameInterval = frameInterval;
			displayLink.AddToRunLoop (NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);
			this.displayLink = displayLink;
			
			IsAnimating = true;
		}
		
		public void StopAnimating ()
		{
			if (!IsAnimating)
				return;
			displayLink.Invalidate ();
			displayLink = null;
			DestroyFrameBuffer ();
			IsAnimating = false;
		}
		
		[Export ("drawFrame")]
		void DrawFrame ()
		{
			OnRenderFrame (new FrameEventArgs ());
		}
		
		#endregion

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);
			
			MakeCurrent ();

			if (mStage == null) {
				// construct flash stage
				mStage = new flash.display.Stage ((int)this.Frame.Width, (int)this.Frame.Height);
			}

			if (mDemoSprite == null) {
				// construct demo
                // $$TODO come up with a better demo chooser!
				flash.display.DisplayObject.globalStage = mStage;
                //mDemoSprite = new _root.Basic_View();
                //mDemoSprite = new _root.Basic_SkyBox();
                //mDemoSprite = new _root.Basic_Particles();
                //mDemoSprite = new _root.Intermediate_ParticleExplosions();
                mDemoSprite = new _root.Basic_Shading();
				flash.display.DisplayObject.globalStage = null;
			}

			if (mStage != null) {
				mStage.onEnterFrame ();
			}
			
			// update all timer objects
			flash.utils.Timer.advanceAllTimers();

			SwapBuffers ();
		}

	}
}
