using System.Numerics; // Vector2
using Raylib_cs; // Color
using System;

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
	class AcceleratingBall : SpriteNode
	{
		// your private fields here (add Velocity, Acceleration, and MaxSpeed)
		private Vector2 Velocity = new Vector2(200, 200);
		private Vector2 Acceleration = new Vector2(40, 30);
		
		float MaxSpeed = 1000;
		bool AcceleratingX = true;
		bool AcceleratingY = true;


		// constructor + call base constructor
		public AcceleratingBall() : base("resources/ball.png")
		{
			Position = new Vector2(Settings.ScreenSize.X / 2, Settings.ScreenSize.Y / 4);
			Color = Color.RED;
		}

		// Update is called every frame
		public override void Update(float deltaTime)
		{
			Move(deltaTime);
			WrapEdges();
		}

		// your own private methods
		private void Move(float deltaTime)
		{
			// TODO implement
			Position += Velocity * deltaTime;

			// accelerate your ball (40, 30) every frame
			// limit to a maximum speed of 1000 pixels/second

			if (Velocity.X < MaxSpeed && AcceleratingX)
			{
				if (Velocity.X > 0)
				{
					Velocity.X += Acceleration.X * deltaTime;
				}
				if (Velocity.X < 0)
				{
					Velocity.X -= Acceleration.X * deltaTime;
				}
			} else {
				AcceleratingX = false;
				Velocity.X = MaxSpeed;
			}

			if (Velocity.Y < MaxSpeed && AcceleratingY) 
			{
				if (Velocity.Y > 0)
				{
					Velocity.Y += Acceleration.Y * deltaTime;
				}
				if (Velocity.Y < 0)
				{
					Velocity.Y -= Acceleration.Y * deltaTime;
				}
			} else {
				AcceleratingY = false;
				Velocity.Y = MaxSpeed;
			}
		}

		private void WrapEdges()
		{
			float scr_width = Settings.ScreenSize.X;
			float scr_height = Settings.ScreenSize.Y;
			float spr_width = TextureSize.X;
			float spr_height = TextureSize.Y;

			if (Position.X > scr_width)
			{
				Position = new Vector2(0, Position.Y);
			}
			else if (Position.X < 0)
			{
				Position = new Vector2(scr_width, Position.Y);
			}
			if (Position.Y > scr_height)
			{
				Position = new Vector2(Position.X, 0);
			}
			else if (Position.Y < 0)
			{
				Position = new Vector2(Position.X, scr_height);
			}
		}
	}
}

//Gideon