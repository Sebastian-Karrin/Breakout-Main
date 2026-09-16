using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Breakout;
public class Ball
{
    public Sprite sprite;
    public const float Diameter = 20.0f;
    public const float Radius = Diameter * 0.5f;
    public Vector2f direction = new Vector2f(1, 1)/MathF.Sqrt(2.0f);
    public int health = 3;
    public int score;
    public Text gui;
    public bool newball = false;

    public void Reflect(Vector2f normal)
    {
        direction -= normal * (2 * (direction.X * normal.X + direction.Y * normal.Y));
    }
    

    public Ball() //Constructor
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/ball.png");
        sprite.Position = new Vector2f(250, 450);
        Vector2f ballTextureSize = (Vector2f) sprite.Texture.Size;
        sprite.Origin = 0.5f * ballTextureSize;
        sprite.Scale = new Vector2f(Diameter/ ballTextureSize.X, Diameter/ballTextureSize.Y);
        gui = new Text();
        gui.CharacterSize = 24;
        gui.Font = new Font("assets/future.ttf");
        newball = true;
    }

    public void Update(float deltaTime, Paddle paddle)
    {
        
            var newPos = sprite.Position;
            newPos += direction * deltaTime * 300.0f;
            if (newPos.X > Program.ScreenW - Radius)
            {
                newPos.X = Program.ScreenW - Radius;
                Reflect(new Vector2f(-1, 0));
            }

            if (newPos.X < Program.ScreenW - 500.0f + Radius)
            {
                newPos.X = Program.ScreenW - 500.0f + Radius;
                Reflect(new Vector2f(1, 0));
            }

            if (newPos.Y > Program.ScreenH - Radius)
            {
                newball = true;
                health -= 1;
                newPos.Y = paddle.sprite.Position.Y - Diameter -10;
                newPos.X = paddle.sprite.Position.X;
                
            }

            switch (newball && !Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                case true:
                newPos.Y = paddle.sprite.Position.Y - Diameter -10;
                newPos.X = paddle.sprite.Position.X;
                break;
                case false:
                    newball = false;
                    break;
            }
           

            if (newPos.Y < Program.ScreenH - 700.0f + Radius)
            {
                newPos.Y = Program.ScreenH - 700.0f + Radius;
                Reflect(new Vector2f(0, 1));
            }
           
            sprite.Position = newPos;
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(sprite);

        gui.DisplayedString = $"Health: {health}";
        gui.Position = new Vector2f(12, 8);
        target.Draw(gui);
        
        gui.DisplayedString = $"Score: {score}";
        gui.Position = new Vector2f(Program.ScreenW - gui.GetGlobalBounds().Width - 12, 8);
        target.Draw(gui);
    }
}