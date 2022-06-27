using System; // Console
using System.Numerics; // Vector2
using Raylib_cs; // Color

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
	class Pointer : SpriteNode
	{
		// your private fields here (add Velocity, Acceleration, and MaxSpeed)
		private Vector2 Acceleration;
		private Vector2 Velocity;
		float MaxSpeed = 1000;


		// constructor + call base constructor
		public Pointer() : base("resources/spaceship.png")
		{
			Position = new Vector2(Settings.ScreenSize.X / 2, Settings.ScreenSize.Y / 2);
			Color = Color.YELLOW;
		}

		// Update is called every frame
		public override void Update(float deltaTime)
		{
			Follow(deltaTime);
			BounceEdges();
		}

		// your own private methods
		private void Follow(float deltaTime)
		{
			Vector2 mouse = Raylib.GetMousePosition();
			
			Vector2 direction = mouse - Position;

			float distance = Vector2.Distance(mouse, Position);

			if(Position != mouse)
			{
				Vector2.Normalize(direction);

				Acceleration = direction;
				
				if (Velocity.X > MaxSpeed || Velocity.X < MaxSpeed) {
					Velocity.X += Acceleration.X * deltaTime * 2;
				} else if (Velocity.X > -MaxSpeed && Velocity.X < -MaxSpeed) {
					Velocity.X -= Acceleration.X * deltaTime * 2;
				} if (Velocity.Y < MaxSpeed || Velocity.Y > MaxSpeed) {
					Velocity.Y += Acceleration.Y * deltaTime * 2;
				} else if (Velocity.Y > -MaxSpeed && Velocity.Y < -MaxSpeed) {
					Velocity.Y -= Acceleration.Y * deltaTime * 2;
				}

				Position += Velocity * deltaTime;
				Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);
				Acceleration *= 0;
			}
		}

		private void BounceEdges()
		{
			float scr_width = Settings.ScreenSize.X;
			float scr_height = Settings.ScreenSize.Y;
			float spr_width = TextureSize.X;
			float spr_height = TextureSize.Y;

            if (Position.X + spr_width / 2 > scr_width)
            {
				Position = new Vector2(scr_width - spr_width / 2, Position.Y);
            }
            else if (Position.X - spr_width / 2 < 0)
            {
				Position = new Vector2(spr_width / 2, Position.Y);
            }
            if (Position.Y + spr_height / 2 > scr_height)
            {
				Position = new Vector2(Position.X, scr_height - spr_height / 2);
            }
            else if (Position.Y - spr_height / 2 < 0)
            {
				Position = new Vector2(Position.X, spr_height / 2);
            }
			if (Position.X > scr_width || Position.X < 0 || Position.Y > scr_height || Position.Y < 0)
			{
				Position = new Vector2(scr_width / 2, scr_height / 2);
			}
		}
	}
}

//Gideon