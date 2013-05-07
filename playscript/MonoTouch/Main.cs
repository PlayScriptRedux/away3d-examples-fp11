using System;
using PlayScript.Application;

namespace PlayScriptAway3D
{
	public class MainClass
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			//			Application.Run(args, typeof(_root.Basic_View));
						Application.Run(args, typeof(_root.Basic_SkyBox));
			//			Application.Run(args, typeof(_root.Basic_Particles));
			//			Application.Run(args, typeof(_root.Intermediate_ParticleExplosions));
			//			Application.Run(args, typeof(_root.Basic_Shading));
			//			Application.Run(args, typeof(_root.Basic_Load3DS));
			//			Application.Run(args, typeof(_root.Basic_Fire));
//			Application.Run(args, typeof(_root.Intermediate_Globe));
		}
	}
}
