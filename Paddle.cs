using System.Drawing;
using System.Runtime.InteropServices;
using breakout;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
namespace Breakout;

public class Paddle
{
    public const float Diameter = 20.0f;
    public const float Radius = Diameter * 0.5f;
    public Sprite sprite;
    public Vector2f size; //Perharps problem
    public bool haspowerupp = false;
    public float powerupptimestamp = 0;
    
    public Paddle()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/paddle.png");
        sprite.Position = new Vector2f(250, 650);
        Vector2f paddleTextureSize = (Vector2f)sprite.Texture.Size;
        sprite.Origin = 0.5f * paddleTextureSize;
        sprite.Scale = new Vector2f(Diameter/ paddleTextureSize.Y, Diameter/paddleTextureSize.Y);
        size = new Vector2f(  // Hitbox
            sprite.GetGlobalBounds().Width,
            sprite.GetGlobalBounds().Height
        );
    }

    public void Update(Ball ball, Powerupp powerupp, float deltaTime, Clock clock)
    {
        var newPos = sprite.Position;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            newPos.X += deltaTime * 300.0f;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            newPos.X -= deltaTime * 300.0f;
        }

        if (newPos.X > Program.ScreenW - size.X / 2)
        {
            newPos.X = Program.ScreenW - size.X / 2;
        }
        
        if (newPos.X < Program.ScreenW - 500.0f + size.X / 2)
        {
            newPos.X = Program.ScreenW - 500.0f + size.X / 2;
        }

        if (Collision.CircleRectangle(
                ball.sprite.Position, Ball.Radius, this.sprite.Position, size, out Vector2f hit))
        {
            ball.sprite.Position += hit;
            ball.Reflect(hit.Normalized());
        }

      
        if (Collision.CircleRectangle(
                powerupp.sprite.Position, Powerupp.Radius, this.sprite.Position, size, out Vector2f phit))
        {
            haspowerupp = true;
            powerupptimestamp = clock.ElapsedTime.AsSeconds();
            powerupp.sprite.Position += (phit + new Vector2f(0, 100.0f));
            sprite.Scale += new Vector2f(0.1f, 0);
            size = new Vector2f(
                sprite.GetGlobalBounds().Width,
                sprite.GetGlobalBounds().Height
            );
        }
        if (clock.ElapsedTime.AsSeconds() > powerupptimestamp + 4.0f && haspowerupp)
        {
            sprite.Scale -= new Vector2f(0.1f, 0);
            size = new Vector2f(
                sprite.GetGlobalBounds().Width,
                sprite.GetGlobalBounds().Height
            );
            haspowerupp = false;
           clock.Restart();
        }

       

        sprite.Position = newPos;
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(sprite);
    }
    
    
}