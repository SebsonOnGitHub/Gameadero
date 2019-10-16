using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public Text trocaText;
    public Text capText;

    private Text trocaNumber;
    private Text[] capsNumbers = new Text[2];
    private int capCounter = 0;
    private int letterDist = 24;

    public void Start() {
        Init();
    }

    public void Init() {
        trocaText.text = "";
        capText.text = "";

        PrintLeftToRight(trocaText, "TROCADERO " + 0);
        PrintRightToLeft(capText, "BOTTLE CAPS " + 0 + "/" + 0);
    }

    public void Update(){
        int maxCaps = Player.capsCollected + FindObjectOfType<GameMaster>().capsInWorld;

        trocaNumber.text = Mathf.Floor(Player.trocaCollected).ToString();
        capsNumbers[1].text = Player.capsCollected.ToString();
        capsNumbers[0].text = maxCaps.ToString();
    }

    public void PrintLeftToRight(Text origin, string printString) {
        Color32[] trocaColors = {new Color32(228, 30, 20, 255), new Color32(0, 92, 169, 255), new Color32(0, 124, 71, 255)};

        for (int i = 0; i < printString.Length; i++) {
            Text letter = Instantiate(origin) as Text;
            if (char.IsDigit(printString[i])) {
                trocaNumber = letter;
            }
            letter.transform.SetParent(transform, false);

            Vector3 oldPos = origin.transform.position;
            Vector3 newPos = new Vector3(oldPos.x + (letterDist * i), oldPos.y, oldPos.z);

            letter.text = printString[i].ToString();
            letter.color = trocaColors[i % 3];
            letter.transform.position = newPos;
        }
    }

    public void PrintRightToLeft(Text origin, string printString) {
        Color32[] trocaColors = {new Color32(0, 124, 71, 255), new Color32(228, 30, 20, 255), new Color32(0, 92, 169, 255)};

        char[] charArray = printString.ToCharArray();
        Array.Reverse(charArray);
        printString = new string(charArray);
        int startingColor;

        if (printString.Length % 3 == 0) {
            startingColor = 2;
        }
        else if (printString.Length % 3 == 1) {
            startingColor = 1;
        }
        else {
            startingColor = 0;
        }

        for (int i = 0; i < printString.Length; i++) {
            Text letter = Instantiate(origin) as Text;
            if (char.IsDigit(printString[i])) {
                capsNumbers[capCounter] = letter;
                capCounter++;
            }
            letter.transform.SetParent(transform, false);

            Vector3 oldPos = origin.transform.position;
            Vector3 newPos = new Vector3(oldPos.x - (letterDist * i), oldPos.y, oldPos.z);

            letter.text = printString[i].ToString();
            letter.color = trocaColors[(startingColor + i) % 3];
            letter.transform.position = newPos;
        }
    }
}
