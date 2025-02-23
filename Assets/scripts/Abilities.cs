using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    // this script stores various states of the player for easier access and updation

    public bool isSlime = true;
    public bool canSwing = false;
    public bool canShoot = true;
    public bool canGlide = true;
    public bool hasKey = false;


    void Start()
    {
        isSlime = true;
        canShoot = true;
        canGlide = true;
        canSwing = false;
    }
}
