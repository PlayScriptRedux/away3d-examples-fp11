
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using MonoMac.Foundation;
using MonoMac.AppKit;

namespace PlayScriptApp
{
        public partial class MainWindowController : MonoMac.AppKit.NSWindowController
        {

                PlayScript.Player player;
		bool isAnimating;

                #region Constructors

                // Call to load from the XIB/NIB file
                public MainWindowController () : base("MainWindow")
                {
 
                }

                #endregion

                //strongly typed window accessor
                public new MainWindow Window {
                        get { return (MainWindow)base.Window; }
                }

		public override void AwakeFromNib ()
		{
			// Allocate the Player object
			player = new PlayScript.Player (openGLView.Bounds);

			// load the main application class
//			player.LoadClass(typeof(_root.Basic_View));
//			player.LoadClass(typeof(_root.Basic_SkyBox));
//			player.LoadClass(typeof(_root.Basic_Particles));
//			player.LoadClass(typeof(_root.Intermediate_ParticleExplosions));
			//player.LoadClass(typeof(_root.Basic_Shading));
			//player.LoadClass(typeof(_root.Basic_Load3DS));
			//player.LoadClass(typeof(_root.Basic_Fire));
			player.LoadClass(typeof(_root.Intermediate_Globe));

			// set window title
			Window.Title = player.Title;

			// Assign the player to the view
			openGLView.Player = player;

			// reset the viewport and update OpenGL Context
			openGLView.UpdateView ();
			
			// Activate the display link now
			openGLView.StartAnimation ();
			
			isAnimating = true;
		}


		public void startAnimation ()
		{
			if (isAnimating)
				return;
			
			openGLView.StartAnimation ();

			isAnimating = true;
		}

		public void stopAnimation ()
		{
			if (!isAnimating)
				return;
			
			openGLView.StopAnimation ();

			isAnimating = false;
		}

        
        }
}

