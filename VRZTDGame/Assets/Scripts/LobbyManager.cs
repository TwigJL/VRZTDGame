using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
   public GameObject mainMenu;
   public GameObject controlsMenu;
   public GameObject howToBuyMenu;
   public GameObject howToPlayMenu;
   public GameObject mapMenu;
   public GameObject scoreboard;
   void Start()
   {
      SetAllInactive();
      mainMenu.SetActive(true);
      scoreboard.SetActive(true);
   }
   public void Tutorial()
   {
      SetAllInactive();
      controlsMenu.SetActive(true);
      howToBuyMenu.SetActive(true);
      howToPlayMenu.SetActive(true);
   }
   public void BacktoMainMenu()
   {
      SetAllInactive();
      mainMenu.SetActive(true);
      scoreboard.SetActive(true);
   }
   public void PlayMapsOpen()
   {
      SetAllInactive();
      mapMenu.SetActive(true);
      mainMenu.SetActive(false);
   }
   public void Mountain()
   {
      SceneManager.LoadScene("MountainMap");
   }
   public void Mall()
   {
      SceneManager.LoadScene("MallMap");
   }
   public void Quit()
   {
      Application.Quit();
   }

   // Update is called once per frame
   private void SetAllInactive()
   {
      mainMenu.SetActive(false);
      controlsMenu.SetActive(false);
      howToBuyMenu.SetActive(false);
      howToPlayMenu.SetActive(false);
      mapMenu.SetActive(false);
      scoreboard.SetActive(false);
   }
}
