using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public Text trocaText;
    public Text capText;
    public Text jumpText;
    public Text moveText;
    public Text actionText;
    public Text switchText;

    private List<Text> trocaList;
    private List<Text> capList;
    private List<Text> actionList;
    private List<Text> switchList;
    private float letterDist = 0.88f;

    public void Start() {
        Init();
    }

    public void Init() {
        trocaText.text = "";
        capText.text = "";

        jumpText.text = "";
        moveText.text = "";

        actionText.text = "";
        switchText.text = "";

        trocaList = PrintLeftToRight(trocaText, "TROCADERO " + 0);
        capList = PrintRightToLeft(capText, "BOTTLE CAPS " + 0 + "/" + 0);

        PrintLeftToRight(jumpText, "JUMP: " + PlayerController.keyJump.ToString().ToUpper());
        PrintLeftToRight(moveText, "MOVE: " + PlayerController.keyUp.ToString() + 
                                   "," + PlayerController.keyLeft.ToString() + 
                                   "," + PlayerController.keyDown.ToString() +
                                   "," + PlayerController.keyRight.ToString());

        if (FindObjectOfType<GameMaster>().currPlayer.GetComponent<PlayerMan>()) {
            actionList = PrintRightToLeft(actionText, "GLIDE: " + PlayerController.keyAction.ToString());
            switchList = PrintRightToLeft(switchText, "BALL FORM: " + PlayerController.keySwitch.ToString());
        }
        else {
            actionList = PrintRightToLeft(actionText, "SPAWN: " + PlayerController.keyAction.ToString());
            switchList = PrintRightToLeft(switchText, "DUDE FORM: " + PlayerController.keySwitch.ToString());
        }
    }

    public void Update(){
        int maxCaps = Player.capsCollected + FindObjectOfType<GameMaster>().capsInWorld;

        trocaList[10].text = Mathf.Floor(Player.trocaCollected).ToString();
        capList[capList.Count - 3].text = Player.capsCollected.ToString();
        capList[capList.Count - 1].text = maxCaps.ToString();

        if (FindObjectOfType<GameMaster>().currPlayer.GetComponent<PlayerMan>()) {
            for (int i = 0; i < "GLIDE".Length; i++) {
                actionList[i].text = "GLIDE"[i].ToString();
            }
            for (int i = 0; i < "BALL FORM".Length; i++) {
                switchList[i].text = "BALL FORM"[i].ToString();
            }
        }
        else {
            for (int i = 0; i < "SPAWN".Length; i++) {
                actionList[i].text = "SPAWN"[i].ToString();
            }
            for (int i = 0; i < "DUDE FORM".Length; i++) {
                switchList[i].text = "DUDE FORM"[i].ToString();
            }
        }
    }

    public List<Text> PrintLeftToRight(Text origin, string printString) {
        Color32[] trocaColors = {new Color32(228, 30, 20, 255), new Color32(0, 92, 169, 255), new Color32(0, 124, 71, 255)};
        List<Text> words = new List<Text>();

        for (int i = 0; i < printString.Length; i++) {
            Text letter = Instantiate(origin) as Text;
            words.Add(letter);
            letter.transform.SetParent(transform, false);

            Vector3 oldPos = origin.transform.position;
            Vector3 newPos = new Vector3(oldPos.x + (letterDist * origin.fontSize * i), oldPos.y, oldPos.z);

            letter.text = printString[i].ToString();
            letter.color = trocaColors[i % 3];
            letter.transform.position = newPos;
        }

        return words;
    }

    public List<Text> PrintRightToLeft(Text origin, string printString) {
        Color32[] trocaColors = {new Color32(0, 124, 71, 255), new Color32(228, 30, 20, 255), new Color32(0, 92, 169, 255)};
        List<Text> words = new List<Text>();

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
            words.Add(letter);
            letter.transform.SetParent(transform, false);

            Vector3 oldPos = origin.transform.position;
            Vector3 newPos = new Vector3(oldPos.x - (letterDist * origin.fontSize * i), oldPos.y, oldPos.z);

            letter.text = printString[i].ToString();
            letter.color = trocaColors[(startingColor + i) % 3];
            letter.transform.position = newPos;
        }

        words.Reverse();
        return words;
    }
}
