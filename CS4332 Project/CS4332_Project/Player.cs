using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4332_Project
{
	class Player
	{
		//attributes
		public Vector2 position;
		public Vector2 moveVector;
		public float width;
		public float height;
		private float speed;
		public BoundingBox box;
		Texture2D texture;

		public Player(Texture2D PlayerTexture, Vector2 pos)
		{
			this.texture = PlayerTexture;
			this.position = pos;
			this.box = new BoundingBox(new Vector3(position.X, position.Y, 0),
									   new Vector3(position.X + texture.Width, position.Y + texture.Height, 0)
									   );
			this.width = texture.Width;
			this.height = texture.Height;
			this.speed = 5.1f;
		}

		///wasd movement only
		private void updatePosition(KeyboardState ks)
		{
			moveVector = Vector2.Zero;

			if (ks.IsKeyDown(Keys.D))
			{
				moveVector.X += 1;
			}
			if (ks.IsKeyDown(Keys.A))
			{
				moveVector.X -= 1;
			}
			if (ks.IsKeyDown(Keys.W))
			{
				moveVector.Y -= 1;
			}
			if (ks.IsKeyDown(Keys.S))
			{
				moveVector.Y += 1;
			}

			///normalizes speed so that if you are going diagonal, you aren't moving faster than 
			if (moveVector != Vector2.Zero)
			{
				moveVector.Normalize();
				moveVector *= speed;
			}

			position += moveVector;
			if (position.X < 0 || position.X > 2000)
				position.X -= moveVector.X;
			if (position.Y < 0 || position.Y > 2000)
				position.Y -= moveVector.Y;

		}

		private void updateBox()
		{
			box = new BoundingBox(new Vector3(position.X, position.Y, 0),
									   new Vector3(position.X + texture.Width, position.Y + texture.Height, 0)
									   );
		}

		public void update(KeyboardState ks)
		{
			updatePosition(ks);
			updateBox();
		}

		public void draw()
		{

		}




	}
}
