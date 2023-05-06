using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
   public Slider healthSlider;
   public Text activeZombiesText;
   public Text waveCountText;

   void Update()
   {
      // Update the health slider
      healthSlider.value = GameManager.instance.health;

      // Update the active zombies text
      activeZombiesText.text = $"Active Zombies: {GameManager.activeZombies.Count}";

      // Update the wave count text
      waveCountText.text = $"Wave: {GameManager.instance.waveCT}";
   }
}
