using Godot;
using System;

public partial class AttackSprite2D : Sprite2D
{
    private Vector2 From { get; set; } 
    public Vector2 To { get; set; }
    public double Duration { get; set; }

    private double _elapsed = 0.0;


    public AttackSprite2D(Vector2 from, Vector2 to, double duration, Texture2D texture)
    {
        From = from;
        To = to;
        Duration = duration;
        Texture = texture;

        Position = from;
    }

    public override void _Process(double delta)
    {
        if (_elapsed > Duration)
        {
            QueueFree();
            return;
        }

        double progress = _elapsed / Duration;

        Position = From + (To - From) * (float)progress;

        _elapsed += delta;
    }
}