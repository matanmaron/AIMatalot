using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSelector
{
    public class LevelSelector : MonoBehaviour
    {
        public void On1()
        {
            SceneManager.LoadScene(1);
        }
        public void On2()
        {
            SceneManager.LoadScene(2);
        }
        public void On3()
        {
            SceneManager.LoadScene(3);
        }
        public void On4()
        {
            SceneManager.LoadScene(4);
        }
        public void On5()
        {
            SceneManager.LoadScene(5);
        }
        public void On6()
        {
            SceneManager.LoadScene(6);
        }
        public void OnQ()
        {
            Application.Quit();
        }
    }
}