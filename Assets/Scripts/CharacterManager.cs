using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Transform characterParent;
    private List<Character> characters = new List<Character>();

    private void Awake()
    {
        characters.Clear();

        foreach (Transform child in characterParent)
        {

            Character character = new Character();
            character.charName = child.name;
            character.characterObj = child.gameObject;
            characters.Add(character);

            if (character != null)
            {
                characters.Add(character);
                character.characterObj.SetActive(false);
            }
        }
    }

    public void CharacterIn(string charName)
    {
        if(charName == "Gadis Baru")
        {
            charName = "Stephani";
        }
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].charName == charName)
            {

                GameManager.Instance.animationManager.spriteRenderer =
                    characters[i].characterObj.GetComponent<SpriteRenderer>();
                GameManager.Instance.animationManager.characterAnimator =
                    characters[i].characterObj.GetComponent<Animator>();

                GameManager.Instance.animationManager.FadeIn();
            }
        }
    }
    public void CharacterOut(string charName)
    {
        if (charName == "Gadis Baru")
        {
            charName = "Stephani";
        }
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].charName == charName)
            {

                GameManager.Instance.animationManager.spriteRenderer =
                    characters[i].characterObj.GetComponent<SpriteRenderer>();
                GameManager.Instance.animationManager.characterAnimator =
                    characters[i].characterObj.GetComponent<Animator>();

                GameManager.Instance.animationManager.FadeOut();
            }
        }
    }

    public void SwitchCharacter(string charName)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].charName == charName)
            {
                characters[i].characterObj.SetActive(true);

                GameManager.Instance.animationManager.spriteRenderer = 
                    characters[i].characterObj.GetComponent<SpriteRenderer>();
                GameManager.Instance.animationManager.characterAnimator =
                    characters[i].characterObj.GetComponent<Animator>();
            }
            else
            {
                characters[i].characterObj.SetActive(false);
            }
        }


    }


}

[System.Serializable]
public class Character
{
    public string charName;
    public GameObject characterObj;
}
