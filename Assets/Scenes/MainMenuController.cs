using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   public CanvasGroup OptionsPanel;

   public void PlayGame()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   public void Options()
   {
       OptionsPanel.alpha = 1;
       OptionsPanel.blocksRaycasts = true;
   }


   public void Back()
   {
       OptionsPanel.alpha = 0;
       OptionsPanel.blocksRaycasts = false;
   }

   public void Quit()
   {
       Application.Quit();
   }
}
