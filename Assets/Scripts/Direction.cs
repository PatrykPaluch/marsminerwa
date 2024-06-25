using System;
using UnityEngine;

namespace Marsminerwa
{
    public enum Direction
    {
        Top,
        Right,
        Bottom,
        Left
    }

    public static class DirectionExtension
    {
        public static Direction Opposite(this Direction direction)
        {
            return direction switch
            {
                Direction.Top => Direction.Bottom,
                Direction.Right => Direction.Left,
                Direction.Bottom => Direction.Top,
                Direction.Left => Direction.Right,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

        public static Vector2 ToVector(this Direction direction)
        {
            return direction switch
            {
                Direction.Top => Vector2.up,
                Direction.Right => Vector2.right,
                Direction.Bottom => Vector2.down,
                Direction.Left => Vector2.left,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

        public static Direction FromVector(Vector2 vec)
        {
            if (vec == Vector2.zero)
                throw new ArgumentException("Vector cannot be zero");
            
            if (vec.y > Mathf.Abs(vec.x))
                return Direction.Top;
            if (vec.y < -Mathf.Abs(vec.x))
                return Direction.Bottom;
            if (vec.x > 0)
                return Direction.Right;
            
            return Direction.Left;
            
        }
    }
}