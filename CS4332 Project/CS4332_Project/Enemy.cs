using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4332_Project
{
	class Enemy
	{
		///attributes
		public Texture2D texture;
		public Vector2 position;
		private float speed;
		private Vector2 origin;
		private Vector2 moveVector;
		bool upDirection = false;

		public BoundingBox box;

		public List<Bullet> bullets = new List<Bullet>();
		Texture2D bulletTexture;


		public Enemy(Vector2 pos, Texture2D newTexture, Texture2D newBulletTexture)
		{
			this.texture = newTexture;
			this.bulletTexture = newBulletTexture;
			this.position = pos;
			this.box = new BoundingBox(new Vector3(position.X, position.Y, 0),
									   new Vector3(position.X + texture.Width, position.Y + texture.Height, 0)
									   );
			speed = 1.75f;
			origin = pos;
		}
		
		public void move()
		{
			moveVector = Vector2.Zero;
			position += new Vector2(0, 1);

			if (position.Y >= (origin.Y + 500))
			{
				moveVector.Y -= 1;
				upDirection = true;
			}	
			else if(position.Y <= (origin.Y - 500))
			{
				moveVector.Y += 1;
				upDirection = false;
			}
			else if (position.Y < (origin.Y) && upDirection)
			{
				moveVector.Y -= 1;
			}
			else if (position.Y < (origin.Y) && !upDirection)
			{
				moveVector.Y += 1;
			}
			else if (position.Y > (origin.Y) && upDirection)
			{
				moveVector.Y -= 1;
			}
			else if (position.Y > (origin.Y) && !upDirection)
			{
				moveVector.Y += 1;
			}


			this.box = new BoundingBox(new Vector3(position.X, position.Y, 0),
									   new Vector3(position.X + texture.Width, position.Y + texture.Height, 0)
									   );

			moveVector *= speed;

			position += moveVector;
		}

		float shots = 0;

		public void Update(GraphicsDevice gd, GameTime gt)
		{
			shots += (float) gt.ElapsedGameTime.TotalSeconds;
			if (shots > 1)
			{
				shots = 0;
				shoot();
			}
			updateBullets();

		}


		public void updateBullets()
		{
			foreach (Bullet bullet in bullets)
			{
				bullet.position += bullet.moveVector;
				bullet.box = new BoundingBox(new Vector3(bullet.position.X, bullet.position.Y, 0),
									   new Vector3(bullet.position.X + bullet.texture.Width, bullet.position.Y + bullet.texture.Height, 0)
									   );
				if (bullet.position.Y > (position.Y + texture.Height + 300))
					bullet.isVisible = false;
			}
			for (int i = 0; i < bullets.Count; i++)
			{
				if(!bullets[i].isVisible)
				{
					bullets.RemoveAt(i);
					i--;
				}
			}
		}

		public void shoot()
		{
			Vector2 bulletPosition = new Vector2(position.X,
												position.Y + (texture.Height));
			Bullet newBullet = new Bullet(bulletTexture, bulletPosition);
			newBullet.moveVector.Y += 2.0f;
			
			newBullet.isVisible = true;
			if(bullets.Count < 2)
			{
				bullets.Add(newBullet);
			}
		}

		public void setPosition(Vector2 pos)
		{
			this.position = pos;
		}
		public Vector2 getPosition()
		{
			return this.position;
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(texture, position, Color.White);
			foreach (Bullet bullet in bullets)
			{
				bullet.Draw(sb);
			}
			
		}



	}
}
