using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidPuzzleManager : MonoBehaviour
{
    public static KidPuzzleManager Instance;
    public int TilePlacedCount;
    public GameObject ConfettiParticles;
    public GameObject FinalAnim;
    void Awake()
    {
        Instance=this;
        Reset();
    }
    public void Reset()
    {
        TilePlacedCount = 0;
        ConfettiParticles.SetActive(false);
        FinalAnim.SetActive(false);
    }
    // Update is called once per frame
    public void TilePlaced()
    {
        TilePlacedCount++;
        if (TilePlacedCount >= 3)
        {
            Debug.Log("Game Over");
            ConfettiParticles.SetActive(true);
            Invoke(nameof(PlayFinalAnim), .6f);
            //Reset();
            
        }
    }
    void PlayFinalAnim()
    {
        ConfettiParticles.SetActive(false);
        FinalAnim.SetActive(true);
    }
}
