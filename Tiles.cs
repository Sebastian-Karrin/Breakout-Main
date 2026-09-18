using System.Drawing;
using breakout;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
namespace Breakout;

public class Tiles
{
    public const float Diameter = 20.0f;
    public const float Radius = Diameter * 0.5f;
    public Sprite bluesprite;
    public Sprite greensprite;
    public Sprite pinksprite;
    public Vector2f size;
    public List<Vector2f> positions;

    public Tiles()
    {
        
        bluesprite = new Sprite();
        greensprite = new Sprite();
        pinksprite = new Sprite();
        bluesprite.Texture = new Texture("assets/tileBlue.png");
        greensprite.Texture = new Texture("assets/tileGreen.png");
        pinksprite.Texture = new Texture("assets/tilePink.png");

        Vector2f bluetilesTextureSize = (Vector2f)bluesprite.Texture.Size;{
            bluesprite.Scale = new Vector2f(Diameter / bluetilesTextureSize.Y, Diameter / bluetilesTextureSize.Y);
            bluesprite.Origin = 0.5f * bluetilesTextureSize;
            size = new Vector2f(
                bluesprite.GetGlobalBounds().Width,
                bluesprite.GetGlobalBounds().Height
            );
        }
        
        Vector2f greentilesTextureSize = (Vector2f)greensprite.Texture.Size;
        {
            greensprite.Scale = new Vector2f(Diameter / greentilesTextureSize.Y, Diameter / greentilesTextureSize.Y);
            greensprite.Origin = 0.5f * greentilesTextureSize;
            size = new Vector2f(
                greensprite.GetGlobalBounds().Width,
                greensprite.GetGlobalBounds().Height
            );
        }
        Vector2f pinktilesTextureSize = (Vector2f)pinksprite.Texture.Size;
        {
            pinksprite.Scale = new Vector2f(Diameter / pinktilesTextureSize.Y, Diameter / pinktilesTextureSize.Y);
            pinksprite.Origin = 0.5f * pinktilesTextureSize;
            size = new Vector2f(
                pinksprite.GetGlobalBounds().Width,
                pinksprite.GetGlobalBounds().Height
            );
        }
        
        positions = new List<Vector2f>();
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                var pos = new Vector2f(Program.ScreenW * 0.5f + i * 96.0f, Program.ScreenH * 0.3f + j * 48.0f);
                positions.Add(pos);
            }
        }
       
    }

    public void Update(Ball ball, float deltaTime, Powerupp powerupp)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            var pos = positions[i];
            if (Collision.CircleRectangle(ball.sprite.Position, Ball.Radius, pos, size, out Vector2f hit))
            {
                ball.sprite.Position += hit;
                ball.Reflect(hit.Normalized());
               
                if (new Random().Next(1,11) <= 7)
                {
                    new Powerupp();
                   
                    powerupp.sprite.Position = positions[i];
                }
                positions.RemoveAt(i);
                i = 0;
                
                ball.score += 100;
            }
        }
    }

    public void Draw(RenderTarget target)
    {
        for (int i = 0; i < positions.Count; i++)
        {
                switch (positions[i].Y)
                {
                    case < 115.0f and >113.0f:
                        bluesprite.Position = positions[i];
                        target.Draw(bluesprite);
                        break;
                    case < 163.0f and > 161.0f:
                        greensprite.Position = positions[i];
                        target.Draw(greensprite);
                        break;
                    case < 211.0f and > 209.0f:
                        pinksprite.Position = positions[i];
                        target.Draw(pinksprite);
                        break;
                    case < 259.0f and > 257.0f:
                        greensprite.Position = positions[i];
                        target.Draw(greensprite);
                        break;
                    case < 307.0f and > 305.0f:
                        bluesprite.Position = positions[i];
                        target.Draw(bluesprite);
                        break;
                }
        }
    }
}