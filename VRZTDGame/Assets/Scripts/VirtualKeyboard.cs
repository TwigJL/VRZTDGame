using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour
{
   public InputField legacyInputField;
   private StringBuilder inputText;
   private bool caps;

   private void Start()
   {
      inputText = new StringBuilder();
      caps = false;
   }

   public void AppendLetter(string letter)
   {
      if (caps)
      {
         inputText.Append(letter.ToUpper());
      }
      else
      {
         inputText.Append(letter.ToLower());
      }

      UpdateInputField();
   }

   public void ToggleCaps()
   {
      caps = !caps;
   }

   public void AddSpace()
   {
      inputText.Append(" ");
      UpdateInputField();
   }

   public void Backspace()
   {
      if (inputText.Length > 0)
      {
         inputText.Remove(inputText.Length - 1, 1);
         UpdateInputField();
      }
   }

   private void UpdateInputField()
   {
      legacyInputField.text = inputText.ToString();
   }
}
