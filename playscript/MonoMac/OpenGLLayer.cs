
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;
using MonoMac.CoreGraphics;
using MonoMac.CoreVideo;
using MonoMac.OpenGL;
using System.Runtime.InteropServices;

using flash.display;
using flash.display3D;
using OpenGLLayer;

namespace OpenGLLayer
{
	public partial class OpenGLLayer : MonoMac.CoreAnimation.CAOpenGLLayer
	{
		bool animate;

		public OpenGLLayer () : base()
		{
			Initialize ();
		}

		// Called when created from unmanaged code
		public OpenGLLayer (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public OpenGLLayer (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		// Shared initialization code
		void Initialize ()
		{
			Animate = true;
			this.Asynchronous = true;

		}

		public bool Animate {
			get { return animate; }
			set { animate = value; }
		}

		public override bool CanDrawInCGLContext (CGLContext glContext, CGLPixelFormat pixelFormat, double timeInterval, ref CVTimeStamp timeStamp)
		{
			return animate;
		}

		// this is our flash display stage
		flash.display.Stage    mStage;
		flash.display.Sprite   mDemoSprite;

		public override void DrawInCGLContext (CGLContext glContext, CGLPixelFormat pixelFormat, double timeInterval, ref CVTimeStamp timeStamp)
		{
			if (mStage == null) {
				// construct flash stage
				// $$TODO handle resizing
				mStage = new flash.display.Stage ( (int)this.Frame.Width, (int)this.Frame.Height);
			}

			if (mDemoSprite == null) {
				// construct demo
				// $$TODO come up with a better demo chooser!
				flash.display.DisplayObject.globalStage = mStage;
				mDemoSprite = new _root.Basic_View();
				//mDemoSprite = new _root.Basic_SkyBox();
				//mDemoSprite = new _root.Basic_Particles();
				//mDemoSprite = new _root.Intermediate_ParticleExplosions();
				//mDemoSprite = new _root.Basic_Shading();
				flash.display.DisplayObject.globalStage = null;
			}

			//GL.ShadeModel (ShadingModel.Smooth);
			GL.ClearColor (NSColor.Blue); //NSColor.Clear.UsingColorSpace (NSColorSpace.CalibratedRGB));
			GL.Clear (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			if (mStage != null) {
				mStage.onEnterFrame ();
			}

			// update all timer objects
			flash.utils.Timer.advanceAllTimers();

			GL.Flush ();
		}

		public override CGLPixelFormat CopyCGLPixelFormatForDisplayMask (uint mask)
		{
			// make sure to add a null value
			CGLPixelFormatAttribute[] attribs = new CGLPixelFormatAttribute[] { 
				CGLPixelFormatAttribute.Accelerated, 
				CGLPixelFormatAttribute.DoubleBuffer,
				CGLPixelFormatAttribute.ColorSize,
				(CGLPixelFormatAttribute)24,
				CGLPixelFormatAttribute.DepthSize,
				(CGLPixelFormatAttribute)16,
				(CGLPixelFormatAttribute)0
			};
                        
			int numPixs = -1;
			CGLPixelFormat pixelFormat = new CGLPixelFormat (attribs, out numPixs);
			return pixelFormat;
		}


 
	}
}

