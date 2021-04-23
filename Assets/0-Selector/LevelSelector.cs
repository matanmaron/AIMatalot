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
    }
}