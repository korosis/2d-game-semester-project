using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4332_Project
{
	class Bullet
	{
		public Vector2 position;
		public Vector2 moveVector;

		public bool isVisible;

		private float speed;
		public BoundingBox box;
		public Texture2D texture;

		public Bullet(Texture2D newTexture, Vector2 newPosition)
		{
			this.texture = newTexture;
			position = newPosition;
			this.box = new BoundingBox(new Vector3(position.X, position.Y, 0),
									   new Vector3(position.X + texture.Width, position.Y + texture.Height, 0)
									   );

			speed = 1.75f;
			this.isVisible = false;
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(texture, position, Color.White);
		}



	}
}
