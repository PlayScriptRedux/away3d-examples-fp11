using playscript;

namespace PlayScriptAway3D
{
	public class MainClass
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			//Application.run(args, typeof(_root.Basic_View));
			//Application.run(args, typeof(_root.Basic_SkyBox));
			//Application.run(args, typeof(_root.Basic_Particles));
			//Application.run(args, typeof(_root.Basic_Shading));
			//Application.run(args, typeof(_root.Basic_Load3DS));
			//Application.run(args, typeof(_root.Basic_Fire));
			//Application.run(args, typeof(_root.Intermediate_Globe));
			//Application.run(args, typeof(_root.Intermediate_ParticleExplosions));
			Application.run (args, typeof(_root.Intermediate_PlayScriptParticleExplosions));
		}
	}
}
