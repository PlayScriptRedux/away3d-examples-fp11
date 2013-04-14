
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace OpenGLLayer
{
	public partial class MainWindow : MonoMac.AppKit.NSWindow
	{
		#region Constructors

		// Called when created from unmanaged code
		public MainWindow (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public MainWindow (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		// Shared initialization code
		void Initialize ()
		{
			this.AcceptsMouseMovedEvents = true;
		}

		public override void MouseMoved (NSEvent theEvent)
		{
			// big hack here using the static, need to fix the monomac app at some point..
			if (MyView.movingLayer != null)	MyView.movingLayer.MouseMoved(theEvent);
		}


		
		#endregion
	}
}

