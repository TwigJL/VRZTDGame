using UnityEngine;

public class Settings : MonoBehaviour
{
   public void ResumeGame()
   {
      GameManager.instance.SetPauseState(false);
   }

   public void PauseGame()
   {
      GameManager.instance.SetPauseState(true);
   }

   public void ExitGame()
   {
      // Pause the game before quitting
      PauseGame();

      // Close the application
#if UNITY_EDITOR
      UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
   }
}
