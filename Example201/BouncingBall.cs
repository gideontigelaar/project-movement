
using System.Numerics; // Vector2
using Raylib_cs; // Color
using System; // Console

/*
In this class, we have the properties:
- Vector2  Position
- float    Rotation
- Vector2  Scale
- Vector2 TextureSize
- Vector2 Pivot
- Color Color
Methods:
- AddChild(Node child)
- RemoveChild(Node child, bool keepAlive = false)
*/

namespace Movement
{
	class BouncingBall : MoverNode
	{
		// your private fields here (add Velocity, Acceleration, addForce method)
		private Random random = new Random();
		private Vector2 wind;
		private Vector2 gravity = new Vector2(0.0f, 900f);

		// constructor + call base constructor
		public BouncingBall() : base("resources/ball.png")
		{
			Position = new Vector2(Settings.ScreenSize.X / 6, Settings.ScreenSize.Y / 4);
			Color = Color.BLUE;
			float force = (float)random.Next(0, 150);
			wind = new Vector2(force, 0);
		}

		// Update is called every frame
		public override void Update(float deltaTime)
		{
			Fall(deltaTime);
			BounceEdges();
		}

		// your own private methods
		private void Fall(float deltaTime)
		{
			AddForce(wind);
			AddForce(gravity);

			Velocity += Acceleration * deltaTime;
			Acceleration *= 0;
			Position += Velocity * deltaTime;
		}

		private void BounceEdges()
		{
			float scr_width = Settings.ScreenSize.X;
			float scr_height = Settings.ScreenSize.Y;
			float spr_width = TextureSize.X;
			float spr_height = TextureSize.Y;

            if (Position.X + spr_width / 2 > scr_width)
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }
            else if (Position.X - spr_width / 2 < 0)
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }
            if (Position.Y + spr_height / 2 > scr_height)
            {
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
            else if (Position.Y - spr_height / 2 < 0)
            {
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
			if (Position.X > scr_width || Position.X < 0 || Position.Y > scr_height || Position.Y < 0)
			{
				Position = new Vector2(scr_width / 2, scr_height / 2);
			}
		}
	}
}

//Gideon