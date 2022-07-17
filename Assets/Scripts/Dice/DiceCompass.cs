using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCompass : MonoBehaviour
{    
    [SerializeField]
    private Image[] _images;

    public void OnDiceFacesChanged(Dice dice)
    {
        int[] diceCompass = dice.GetDiceCompassFaceSides();
        Sprite[] faces = dice.FacesSprites;

        for (int i = 0; i < diceCompass.Length ; i++)
        {
            _images[i].sprite = faces[diceCompass[i]-1];
        }
    }
}
