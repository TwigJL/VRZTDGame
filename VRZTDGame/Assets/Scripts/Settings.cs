using UnityEngine;

public class Settings : MonoBehaviour
{
   public void ResumeGame()
   {
      Time.timeScale = 1f; // Resume the game by setting the timescale to 1
      GameManager.instance.SetPauseState(false);
   }

   public void PauseGame()
   {
      Time.timeScale = 0f; // Pause the game by setting the timescale to 0
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
