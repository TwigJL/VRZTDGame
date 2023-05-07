using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyboardButton : MonoBehaviour
{
   Keyboard keyboard;
   TextMeshProUGUI buttonText;

   // Start is called before the first frame update
   void Start()
   {
      keyboard = GetComponentInParent<Keyboard>();
      buttonText = GetComponentInChildren<TextMeshProUGUI>();
      if (buttonText.text.Length == 1)
      {
         NameToButtonText();
         ButtonVR buttonVR = GetComponentInChildren<ButtonVR>();
         buttonVR.onPress.AddListener(delegate { keyboard.InsertChar(buttonText.text); });
      }
   }

   public void NameToButtonText()
   {
      buttonText.text = gameObject.name;
   }

}
