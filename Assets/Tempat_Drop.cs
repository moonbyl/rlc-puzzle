using UnityEngine;
using System.Collections.Generic;


public class Tempat_Drop : MonoBehaviour
{
    public List<int> acceptedIDs = new List<int>(); // ID yang bisa diterima
    public int correctID; // ID yang benar-benar benar
    public bool isCorrectlyPlaced = false;
    [HideInInspector] public int currentID; // ID komponen yang sedang dipasang
    [HideInInspector] public bool isOccupied = false; // Flag untuk menandai slot sudah terisi
    [HideInInspector] public Obj_Drag currentComponent; // Referensi ke komponen yang menempati

    public void ResetTempat()
{
    isCorrectlyPlaced = false;
    isOccupied = false;
    currentID = -1;
    currentComponent = null;
    GetComponent<SpriteRenderer>().enabled = true;
}

}

