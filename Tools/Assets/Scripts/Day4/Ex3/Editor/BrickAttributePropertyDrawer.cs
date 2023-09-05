using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BrickAttribute))]
public class BrickAttributePropertyDrawer : PropertyDrawer
{
    #region Fields
    List<Brick> bricks;
    Player player;
    Ball ball;
    #endregion
    
    #region Parent methods
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        BrickAttribute brickAttribute = (BrickAttribute)attribute;
        if (bricks == null)
        {
            bricks = new List<Brick>();
            for (int i = 0; i < 6; i++)
            {
              bricks.Add(new Brick(new Rect(position.x + i*80, position.y, 70,10), GUI.color = Color.red));
            }

            ball = new Ball(new Rect(position.x, position.y + position.height / 2, position.width / 30, position.height / 30), GUI.color = Color.black);
            player = new Player(new Rect(position.x, position.y + position.height - 6, position.width / 4, position.height / 30), GUI.color = Color.blue);
        }

        foreach (Brick b in bricks)
            b.Draw();

        ball.Draw();
        ball.Move(position);        

        player.Draw();
        player.Move();
        player.IsTouch(position);   
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 20;
    }
    #endregion

    #region The game Elements
    public class Player
    {
        public Rect pos;
        Color color;
        int xSpeed;

        public Player(Rect pos, Color color)
        {
            this.pos = pos;
            this.color = color;
            xSpeed = 6;
        }

        public void Move()
        {
            Event e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.RightArrow)
                    pos.x += xSpeed;
                if (e.keyCode == KeyCode.LeftArrow)
                    pos.x -= xSpeed;
                e.Use();
            }                
        }
        
        public void IsTouch(Rect bounds)
        {
            if (pos.x < bounds.x)
                pos.x = bounds.x;
            if (pos.x + pos.width > bounds.x + bounds.width)
                pos.x = bounds.x + bounds.width - pos.width;
        }

        public void Draw()
        {
            EditorGUI.DrawRect(pos, color);
        }
    }

    public class Ball
    {
        public Rect pos;
        Color color;
        int xSpeed;
        int ySpeed;

        public Ball(Rect pos, Color color)
        {
            this.pos = pos;
            this.color = color;
            xSpeed = 5;
            ySpeed = 5;
        }

        public void Move(Rect bounds)
        {
            pos.x+= xSpeed;
            pos.y+= ySpeed;

            if (pos.x < bounds.x || pos.x + pos.width > bounds.x + bounds.width)
                xSpeed *= -1;
            if (pos.y < bounds.y || pos.y + pos.height > bounds.y + bounds.height)
                ySpeed *= -1;
        }

        public void Draw()
        {
            EditorGUI.DrawRect(pos, color);
        }
    }

    public class Brick
    {
        Rect pos;
        Color color;

        public Brick(Rect pos, Color color)
        {
            this.pos = pos;
            this.color = color;
        }

        public void Draw()
        {
            EditorGUI.DrawRect(pos, color);
        }
    }
    #endregion
}
