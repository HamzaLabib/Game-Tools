using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CardEditor : EditorWindow
{
    #region Variables
    string[] pathIdentifier;
    List<string> pathString;
    string[] texturesPath;

    List<Texture2D> texture2Ds;
    List<ScriptableObject> cards;

    CardData card;

    Texture colorTexture;
    public static EditorWindow cardWindow;
    #endregion

    #region Create Window
    [MenuItem("My Tools/CardMenu")]
    public static void CreateWindow()
    {
        cardWindow = GetWindow<CardEditor>("Card Editor");
        cardWindow.position = new Rect(0, 0, 800, 600);
        cardWindow.minSize = new Vector2(600, 500);
    }

    #region Helper method to create the window
    public static Rect GetRect(float x, float y, float width, float height, Rect position)
    {
        Rect retour;

        float widthUnit = position.width / 12;
        float heightUnit = position.height / 17;


        float widthTemp = width * widthUnit;
        float heightTemp = height * heightUnit;
        float xTemp = (x * widthUnit);
        float yTemp = (y * heightUnit);

        retour = new Rect(xTemp, yTemp, widthTemp, heightTemp);

        return retour;
    }
    #endregion
    #endregion

    #region EditorWindow methods
    void OnEnable()
    {
        colorTexture = EditorGUIUtility.whiteTexture;
        pathIdentifier = AssetDatabase.FindAssets("t:ScriptableObject", new string[] { "Assets/Scripts/Day6/Ex6/Cards" });
        texturesPath = AssetDatabase.FindAssets("t:Texture2D", new string[] { "Assets/Images" });
        Initialization();
        GetAllCards();
    }

    void OnGUI()
    {
        TopPart();
        CardView();
    }
    #endregion
    
    #region Initalize "lists, Card"
    void Initialization()
    {
        card = new CardData();
        pathString = new List<string>();
        cards = new List<ScriptableObject>();
        texture2Ds = new List<Texture2D>();
    }
    #endregion

    #region Top part of Window
    void TopPart()
    {
        CardCreation();
        CardsButtons();
    }

    #region The values of the card, creat and save
    void CardCreation()
    {
        Rect topLeftRect = GetRect(0, 0, 8, 7, position);
        GUILayout.BeginArea(topLeftRect);
        {
            card.name = EditorGUILayout.TextField("Name", card.name);
            card.cost = EditorGUILayout.IntField("Cost", card.cost);
            card.manaType = (ManaEnum)EditorGUILayout.EnumPopup("Mana type", card.manaType);
            card.backGroundColor = EditorGUILayout.ColorField("Card Color", card.backGroundColor);

            #region Text area
            EditorGUILayout.LabelField("Description");
            card.description = EditorGUILayout.TextArea(card.description, GUILayout.ExpandHeight(true));
            #endregion

            #region Images
            EditorGUILayout.LabelField("Image");
            EditorGUILayout.BeginHorizontal();
            {
                GetAllTextures();
                foreach (Texture2D image in texture2Ds)
                {
                    if (GUILayout.Button(image, GUILayout.Width(70), GUILayout.Height(70)))
                        card.sprite = image;
                }
                EditorGUILayout.EndHorizontal();
            }
            #endregion

            #region Buttons
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Create"))
                {
                    AssetDatabase.CreateAsset(card, string.Format("Assets/Scripts/Day6/Ex6/Cards/{0}.asset", card.name));
                    cards.Add(card);
                }

                if (GUILayout.Button("Save"))
                {
                    card = new CardData();
                }
            }
            EditorGUILayout.EndHorizontal();
            #endregion
        }
        GUILayout.EndArea();
    }
    #endregion

    #region List of cards in Buttons
    void CardsButtons()
    {
        Rect topRightRect = GetRect(8, 0, 4, 7, position);
        GUILayout.BeginArea(topRightRect);
        GUI.color = Color.white;
        GUI.DrawTexture(GetRect(0, 0, 12, 17, topRightRect), colorTexture);
        foreach (CardData c in cards)
        {
            if (GUILayout.Button(c.name))
            {
                card = c;
                card.name = c.name;
                card.cost = c.cost;
                card.manaType = c.manaType;
                card.backGroundColor = c.backGroundColor;
                card.description = c.description;
                card.sprite = c.sprite;
            }
        }
        GUILayout.EndArea();
    }
    #endregion

    #region Helper methods to handle The top part of Window
    #region Get all cards exist in the folder of cards
    void GetAllCards()
    {
        foreach (var path in pathIdentifier)
        {
            string thePath = AssetDatabase.GUIDToAssetPath(path);
            pathString.Add(Path.GetFileNameWithoutExtension(thePath));
            cards.Add(AssetDatabase.LoadAssetAtPath<CardData>(thePath));
        }
    }
    #endregion
    #region Get all textures exist in the folder if images
    void GetAllTextures()
    {
        texture2Ds.Clear();
        for (int i = 0; i < texturesPath.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(texturesPath[i]);
            Texture2D t = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            texture2Ds.Add(t);
        }
    }
    #endregion
    #region Control the color of mana cost rect
    Color ManaTypeColor(ManaEnum type)
    {
        Color currentColor;
        switch (card.manaType)
        {
            case ManaEnum.Air:
                {
                    currentColor = Color.cyan;
                    break;
                }
            case ManaEnum.Flame:
                {
                    currentColor = Color.red;
                    break;
                }
            case ManaEnum.Water:
                {
                    currentColor = Color.green;
                    break;
                }
            case ManaEnum.Wind:
                {
                    currentColor = Color.magenta;
                    break;
                }
            default:
                {
                    currentColor = Color.white;
                    break;
                }
        }
        return currentColor;
    }
    #endregion
    #endregion
    #endregion

    #region Buttom part of Window 
    //Card viewing
    void CardView()
    {
        Rect ButtomPart = GetRect(0, 7, 12, 10, position);
        Rect frame = GetRect(4.5f, 2, 3, 12, ButtomPart);
        GUILayout.BeginArea(ButtomPart);
        {
            GUILayout.BeginArea(frame);
            {
                //Card frame
                GUI.color = card.backGroundColor;
                GUI.DrawTexture(GetRect(0, 0, 12, 17, frame), colorTexture);
                //Cost
                GUI.color = ManaTypeColor(card.manaType);
                GUI.DrawTexture(GetRect(10.5f, 0, 1.5f, 1.5f, frame), colorTexture);
                GUI.color = Color.black;
                GUI.Box(GetRect(10.5f, 0f, 1.5f, 1.5f, frame), card.cost.ToString());
                //Name
                GUI.DrawTexture(GetRect(2, 1, 8, 1.5f, frame), colorTexture);
                GUI.color = Color.white;
                GUI.Box(GetRect(2, 1, 8, 1.5f, frame), card.name);
                //Image
                GUI.color = Color.white;
                GUI.DrawTexture(GetRect(1, 3.5f, 10, 8, frame), card.sprite);
                //Description
                GUI.color = Color.white;
                GUI.TextArea(GetRect(1, 12.25f, 10, 4, frame), card.description);
            }
            GUILayout.EndArea();
        }
        GUILayout.EndArea();
    }
    #endregion    
}
