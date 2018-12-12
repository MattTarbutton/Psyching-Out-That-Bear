using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers = new SpriteRenderer[15];
    public Text[] text = new Text[3];
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite zSprite;
    public Sprite xSprite;
    public Sprite cSprite;
    public Sprite vSprite;
    public Dictionary<Buttons, Sprite> spriteLookup = new Dictionary<Buttons, Sprite>();

	// Use this for initialization
	void Start ()
    {
        spriteLookup = new Dictionary<Buttons, Sprite>();
        spriteLookup.Add(Buttons.UP, upSprite);
        spriteLookup.Add(Buttons.DOWN, downSprite);
        spriteLookup.Add(Buttons.LEFT, leftSprite);
        spriteLookup.Add(Buttons.RIGHT, rightSprite);
        spriteLookup.Add(Buttons.ACTION1, zSprite);
        spriteLookup.Add(Buttons.ACTION2, xSprite);
        spriteLookup.Add(Buttons.ACTION3, cSprite);
        spriteLookup.Add(Buttons.ACTION4, vSprite);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateKeys(MoveList moves)
    {
        Move[] moveArray = moves.GetMoveList();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (j > moveArray[i].sequence.Length - 1)
                {
                    spriteRenderers[i * 5 + j].enabled = false;
                }
                else
                {
                    spriteRenderers[i * 5 + j].enabled = true;
                    //if (spriteLookup.ContainsKey(moveArray[i].sequence[j]))
                    //{
                        spriteRenderers[i * 5 + j].sprite = spriteLookup[moveArray[i].sequence[j]];
                    //}
                    //else
                    //    Debug.Log(moveArray[i].sequence[j]);
                }
            }
            text[i].text = moveArray[i].name;
        }
    }

    public void ClearKeys()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].enabled = false;
        }
        for (int i = 0; i < 3; i++)
            text[i].text = "";
    }
}
