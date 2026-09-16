using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Breakout
{
    class Program
    {
        public const int ScreenW = 500;
        public const int ScreenH = 700;
        
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(ScreenW, ScreenH), "Breakout"))
            {
                window.Closed += (o, e) => window.Close();

                Clock clock = new Clock();
                Ball ball = new Ball();
                Paddle paddle = new Paddle();
                Tiles tiles = new Tiles();
                Powerupp powerupp = new Powerupp();
                
                while (window.IsOpen)
                {
                    float deltaTime = clock.Restart().AsSeconds();
                    window.DispatchEvents();
                    ball.Update(deltaTime, paddle);
                    paddle.Update(ball, powerupp, deltaTime);
                    tiles.Update(ball, deltaTime, powerupp);
                    powerupp.Update(deltaTime, tiles, paddle, powerupp);

                    window.Clear(new Color(131, 197, 235));
                    ball.Draw(window);
                    paddle.Draw(window);
                    tiles.Draw(window);
                    powerupp.Draw(window, powerupp);

                    if (ball.health <= 0)
                    {
                        ball = new Ball();
                        paddle = new Paddle();
                        tiles = new Tiles();
                        powerupp = new Powerupp();
                        
                        ball.score = 0;
                        ball.health = 3;
                       

                    }

                    if (tiles.positions.Count == 0)
                    {
                        tiles = new Tiles();
                    }
                    window.Display();
                }
            }

        }
    }
}
