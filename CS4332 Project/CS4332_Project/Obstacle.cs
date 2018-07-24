using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4332_Project
{
	class Obstacle
	{
		Texture2D texture;
		private Vector2 position;
		private float speed;

		public BoundingBox box;

		public Obstacle(Vector2 pos, Texture2D newTexture)
		{
			texture = newTexture;
			position = pos;
			this.box = new BoundingBox(new Vector3(position.X, position.Y, 0),
									   new Vector3(position.X + texture.Width, position.Y + texture.Height, 0)
									   );
		}
	}
}
