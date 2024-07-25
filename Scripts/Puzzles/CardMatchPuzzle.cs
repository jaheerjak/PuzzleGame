using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardMatchPuzzle : MonoBehaviour
{
    [SerializeField] private Sprite[] cardSprites;
    [SerializeField] private Image[] cards;
    [SerializeField] private Sprite defaultCardSprite;

    int[] spriteOrder = new int[] { 0,2,1,5,2,0, 3,4,5,1,4,3 };

    public int prevCardIndex = 0;
    bool isPlaying = false;
    void OnEnable()
    {
        InitCardMatchPuzzle();
    }

    // Update is called once per frame
    void InitCardMatchPuzzle()
    {
        prevCardIndex = -1;
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].sprite = defaultCardSprite;
            cards[i].GetComponent<Button>().interactable = true;
        }
    }
    
    public void OnClick_CardIndex(int cardIndex)
    {
        if (isPlaying) return;

        isPlaying = true;
        cards[cardIndex].transform.DOScaleX(0f, 0.3f)
          .OnComplete(() =>
          {
              cards[cardIndex].sprite = cardSprites[spriteOrder[cardIndex]];
              cards[cardIndex].transform.DOScaleX(1f, 0.3f)
              .OnComplete(() =>
              {
                  cards[cardIndex].GetComponent<Button>().interactable = false;
                  if (prevCardIndex == -1)
                  {

                      prevCardIndex = cardIndex;
                      isPlaying = false;
                  }
                  else
                  {
                      Debug.Log("check index " + prevCardIndex + ":::" + cardIndex);
                      if (spriteOrder[prevCardIndex] == spriteOrder[cardIndex])
                      {
                          Invoke(nameof(CheckWin), 0.5f);
                      }
                      else
                      {
                          cards[cardIndex].GetComponent<Button>().interactable = true;
                          cards[prevCardIndex].GetComponent<Button>().interactable = true;
                          ResetCardSprite();
                      }


                  }

              });
          });
        
        
    }
    void CheckWin()
    {
        isPlaying = false;
        prevCardIndex = -1;
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<Button>().interactable)
                return;
        }
        Debug.Log("win");
    }
    private void ResetCardSprite()
    {
        Debug.Log("Reset");
        isPlaying = false;
        prevCardIndex = -1;
        for (int i=0;i<cards.Length;i++)
        {
            if (cards[i].GetComponent<Button>().interactable)
            {
                cards[i].sprite = defaultCardSprite;
            }
        }
    }
}
