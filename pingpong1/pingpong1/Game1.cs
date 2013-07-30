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

namespace pingpong1
{
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Texture2D barra;
        Texture2D bola;

        Rectangle rectangulobarra;
        Rectangle rectangulocontra;
        Rectangle rectangulobola;

        BoundingBox colisionador1;
        BoundingBox colisionador2;
        BoundingBox colisionador3;
        BoundingBox colisionador4;
        BoundingBox colisionador5;


        int score1 = 0;
        int score2 = 0;
        int score3 = 0;
        int score4 = 0;

        SpriteFont fuente;

        bool arriba = false;
        bool izquierda = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            rectangulobarra = new Rectangle(0, 0, 20, 180);
            rectangulocontra = new Rectangle(780, 0, 20, 180);
            rectangulobola = new Rectangle(50, 50, 30, 30);

            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            barra = Content.Load<Texture2D>("barra");
            bola = Content.Load<Texture2D>("pentag");
            fuente = Content.Load<SpriteFont>("coli");
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            //envolver
            colisionador1 = new BoundingBox(new Vector3(rectangulobola.X, rectangulobola.Y, 0),
                                           new Vector3(rectangulobola.X + rectangulobola.Width,
                                                       rectangulobola.Y + rectangulobola.Height, 0));


            colisionador2 = new BoundingBox(new Vector3(rectangulobarra.X, rectangulobarra.Y, 0),
                                            new Vector3(rectangulobarra.X + rectangulobarra.Width,
                                                        ( rectangulobarra.Height / 2)
                                                        + rectangulobarra.Y, 0));

            colisionador3 = new BoundingBox(new Vector3(rectangulobarra.X + rectangulobarra.Width,
                                                        ( rectangulobarra.Height / 2)
                                                        + rectangulobarra.Y, 0),
                                            new Vector3(rectangulobarra.X + rectangulobarra.Width,
                                                        rectangulobarra.Y + rectangulobarra.Height, 0));


            colisionador4 = new BoundingBox(new Vector3(rectangulocontra.X, rectangulocontra.Y, 0),
                                            new Vector3(rectangulocontra.X + rectangulocontra.Width,
                                                        ( rectangulocontra.Height / 2) +
                                                        rectangulocontra.Y, 0));

            colisionador5 = new BoundingBox(new Vector3(rectangulocontra.X ,
                                                       (rectangulocontra.Height / 2) + rectangulocontra.Y, 0),
                                            new Vector3(rectangulocontra.X + rectangulocontra.Width,
                                                        rectangulocontra.Y + rectangulocontra.Height, 0));
                                        
           

           




            if (colisionador1.Intersects(colisionador2))
            {
                arriba = true;
                izquierda = !izquierda;

                score1 += 1;
                
                
            } 

            if (colisionador1.Intersects(colisionador3))
            {
                arriba = !arriba;
                izquierda = !izquierda;

                score2 += 1;
                
            }

            if (colisionador1.Intersects(colisionador4))
            {
                arriba = true;
                izquierda = true;

                score3 += 1;

            }

            if (colisionador1.Intersects(colisionador5))
            {
                arriba = !arriba;
                izquierda = true;

                score4 += 1;

            }




            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.A) && rectangulobarra.Y > 0) //poniendo ya limites a primer barra
                rectangulobarra.Y -= 9;

            if (kbs.IsKeyDown(Keys.Z) && rectangulobarra.Y < graphics.PreferredBackBufferWidth - rectangulobarra.Width
                                      && rectangulobarra.Y < graphics.PreferredBackBufferHeight - rectangulobarra.Height)
                rectangulobarra.Y += 9;


            if (kbs.IsKeyDown(Keys.Up) && rectangulocontra.Y > 0) // segunda barra
                rectangulocontra.Y -= 9;

            if (kbs.IsKeyDown(Keys.Down) && rectangulocontra.Y < graphics.PreferredBackBufferWidth - rectangulocontra.Width
                                         && rectangulocontra.Y < graphics.PreferredBackBufferHeight - rectangulocontra.Height)
                rectangulocontra.Y += 9;


            
            //validar limites de bola arriba y abajo

            if (rectangulobola.Y <= 0) arriba = false;
            else if (rectangulobola.Y + rectangulobola.Height >= graphics.PreferredBackBufferHeight) arriba = true;

            if (rectangulobola.X <= 0) izquierda = false;
            else if (rectangulobola.X + rectangulobola.Width >= graphics.PreferredBackBufferWidth) izquierda = true;
           

            //Rebote
            if (izquierda)
                rectangulobola.X -= 5;
            else
                rectangulobola.X += 5;

            if (arriba)
                rectangulobola.Y -= 5;
            else
                rectangulobola.Y += 5;

            









            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(barra, rectangulobarra, Color.White);

            spriteBatch.Draw(barra, rectangulocontra,Color.Yellow);

            spriteBatch.Draw(bola,rectangulobola,Color.White);

            spriteBatch.DrawString(fuente, "1a: " + score1.ToString(), new Vector2(200, 0), Color.Brown);

            spriteBatch.DrawString(fuente, "2a: " + score2.ToString(), new Vector2(300, 0), Color.Black);

            spriteBatch.DrawString(fuente, "3a: " + score3.ToString(), new Vector2(400, 0), Color.BlueViolet);

            spriteBatch.DrawString(fuente, "4a: " + score4.ToString(), new Vector2(500, 0), Color.Cyan);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
