using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4332_Project
{
	class Camera2D
	{

		public Matrix transform;
		Viewport view;
		Vector2 center;
		public Vector3 position;

		public Camera2D(Viewport newView)
		{
			this.view = newView;
		}

		public void Update(GameTime gameTime, Player player, GraphicsDevice gd)
		{
			center = new Vector2(player.position.X + (player.width / 2) - (gd.Viewport.Width / 2), player.position.Y + (player.height / 2) - (gd.Viewport.Height / 2));
			transform = Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));

		}




	}
}
