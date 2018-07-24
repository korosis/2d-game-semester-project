using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS4332_Project
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;



		SpriteBatch spriteBatch;
		Camera2D camera;

		Texture2D playerText;
		Texture2D background;
		Texture2D menu;
		Texture2D gameover;
		Texture2D gamewon;
		Texture2D moveEnemyTexture;
		Texture2D staticEnemyTexture;

		Texture2D goalTexture;
		Texture2D obstacleTexture;

		Player player;

		BoundingBox goalBox = new BoundingBox(new Vector3(900, 1700, 0), new Vector3(900, 1700, 0));


		Vector2[] staticPosition =
		{
			new Vector2(1000,200),
			new Vector2(1400,200),

			new Vector2(700,700),
			new Vector2(900,700),
			new Vector2(1100,700),
			new Vector2(1300,700)

		};
		Vector2[] movePosition =
		{
			new Vector2(700,550),
			new Vector2(1100,550),
			new Vector2(1500,550),

			new Vector2(700,1500),
			new Vector2(1300,1600)
		};
		Vector2[] obstaclePosition =
		{
			new Vector2()
		};


		Obstacle[] obstacles = new Obstacle[8];
		Enemy[] staticEnemy = new Enemy[6];
		Enemy[] moveEnemy = new Enemy[5];

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			

			camera = new Camera2D(GraphicsDevice.Viewport);
			playerText = Content.Load<Texture2D>("player");
			background = Content.Load<Texture2D>("background");
			menu = Content.Load<Texture2D>("menu");
			gameover = Content.Load<Texture2D>("gameover");
			gamewon = Content.Load<Texture2D>("gamewon");
			
			goalTexture = Content.Load<Texture2D>("goal");
			goalBox = new BoundingBox(new Vector3(900, 1700, 0), new Vector3(900 + (goalTexture.Width), 1700 + (goalTexture.Height), 0));

			player = new Player(playerText, new Vector2(0, 0));
			for (int i = 0; i < staticEnemy.Length; i++)
			{
				staticEnemy[i] = new Enemy(staticPosition[i], Content.Load<Texture2D>("staticEnemy"), Content.Load<Texture2D>("bullet"));
			}
			for (int i = 0; i < moveEnemy.Length; i++)
			{
				moveEnemy[i] = new Enemy(movePosition[i], Content.Load<Texture2D>("moveEnemy"), Content.Load<Texture2D>("bullet"));
			}






			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		enum GameState
		{
			menu,
			activeGame,
			gameOver,
			gameWon
		}
		GameState gs = GameState.menu;

		KeyboardState ks;


		bool enemyCollision()
		{
			bool collide = false;

			foreach (Enemy enemy in staticEnemy)
			{
				foreach (Bullet bullet in enemy.bullets)
				{
					if (player.box.Intersects(bullet.box))
					{
						collide = true;
						break;
					}
				}
				if (player.box.Intersects(enemy.box) || collide)
				{
					collide = true;
					break;
				}
			}
			foreach (Enemy enemy in moveEnemy)
			{
				if (player.box.Intersects(enemy.box) || collide)
				{
					collide = true;
					break;
				}
			}
			
			return collide;
		}

		void wallCollision()
		{
			foreach (Obstacle obstacle in obstacles)
			{
				if (player.box.Intersects(obstacle.box))
				{
					player.moveVector.X = 0;
					player.moveVector.Y = 0;
				}
			}
		}


		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			// TODO: Add your update logic here

			ks = Keyboard.GetState();


			if (gs == GameState.menu)
			{
				camera.transform = Matrix.CreateTranslation(Vector3.Zero);
				if (Keyboard.GetState().IsKeyDown(Keys.Space))
				{
					gs = GameState.activeGame;
					player.update(ks);
					camera.Update(gameTime, player, GraphicsDevice);
				}
			}

			else if (gs == GameState.activeGame)
			{
				if (enemyCollision())
				{
					gs = GameState.gameOver;
					player.position = new Vector2(0,0);
				}
					
				else
				{
					player.update(ks);
					
					foreach (Enemy enemy in staticEnemy)
					{
						enemy.Update(GraphicsDevice, gameTime);
					}
					foreach (Enemy enemy in moveEnemy)
					{
						enemy.move();
					}
					
					camera.Update(gameTime, player, GraphicsDevice);
					if (player.box.Intersects(goalBox))
					{
						gs = GameState.gameWon;
						player.position = new Vector2(0, 0);
					}
						
				}
			}

			else if (gs == GameState.gameOver)
			{
				camera.transform = Matrix.CreateTranslation(Vector3.Zero);
				if (Keyboard.GetState().IsKeyDown(Keys.Space))
					gs = GameState.menu;
			}

			else if (gs == GameState.gameWon)
			{
				camera.transform = Matrix.CreateTranslation(Vector3.Zero);
				if (Keyboard.GetState().IsKeyDown(Keys.Space))
					gs = GameState.menu;
			}

			base.Update(gameTime);
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

			

			if (gs == GameState.menu)
			{
				spriteBatch.Draw(menu, Vector2.Zero, null, Color.White);
			}
			else if (gs == GameState.activeGame)
			{
				//draw player
				//draw map
				//draw enemies
				spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White);

				spriteBatch.Draw(playerText, player.position, null, Color.White);

				foreach (Enemy enemy in staticEnemy)
				{
					enemy.Draw(spriteBatch);
				}
				foreach (Enemy enemy in moveEnemy)
				{
					enemy.Draw(spriteBatch);
				}


				spriteBatch.Draw(goalTexture, new Vector2(900, 1700), Color.White);

			}
			else if (gs == GameState.gameOver)
			{
				//draw gameover screen
				spriteBatch.Draw(gameover, Vector2.Zero, null, Color.White);
			}
			else if (gs == GameState.gameWon)
			{
				spriteBatch.Draw(gamewon, Vector2.Zero, null, Color.White);
			}


			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
