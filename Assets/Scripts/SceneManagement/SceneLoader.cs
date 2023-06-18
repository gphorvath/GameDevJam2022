using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        private GameManager _gm;

        private void Start() {
            _gm = GameObject.FindObjectOfType<GameManager>();
        }


        public void LoadScene(string sceneName)
        {
            _gm.LoadSceneByName(sceneName);
        }
    }
}