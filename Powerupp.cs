using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
namespace Breakout;

public class Powerupp
{
    public Sprite sprite;
    public const float Diameter = 15.0f;
    public const float Radius = Diameter * 0.5f;
    public Vector2f direction = new Vector2f(0, 1);
   
    public Powerupp()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/ball.png");
        sprite.Position = new Vector2f(250, 1000);
        Vector2f ballTextureSize = (Vector2f) sprite.Texture.Size;
        sprite.Origin = 0.5f * ballTextureSize;
        sprite.Scale = new Vector2f(Diameter/ ballTextureSize.X, Diameter/ballTextureSize.Y);
        sprite.Color = Color.Blue;
        
    }
    public void Reflect(Vector2f normal)
    {
        direction -= normal * (2 * (direction.X * normal.X + direction.Y * normal.Y));
    }

    public void Update(float deltaTime, Tiles tiles, Paddle paddle, Powerupp powerupp)
    {
       
            var newPos = sprite.Position;
            newPos += direction * deltaTime * 100.0f;
            sprite.Position = newPos;
        
    }

    public void Draw(RenderTarget target, Powerupp powerupp)
    {
            target.Draw(sprite);
    }
}