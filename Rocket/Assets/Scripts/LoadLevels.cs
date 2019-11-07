using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace roket
{
    public class LoadLevels : MonoBehaviour
    {
        #region variables
        public enum levels {Level1, Level2, Level3, Level4, Level5}
        levels lvl = levels.Level1;

        public string levelName;

        public static LoadLevels instance;
        #endregion
        // Start is called before the first frame update
        void Start()
        {

        }

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            Levels();
            LoadNextLevel();
        }

        #region functions and methods

        public void LoadNextLevel()
        {
            int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            int nextLevelNumber = currentSceneNumber + 1;
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (currentSceneNumber == SceneManager.sceneCountInBuildSettings - 1)  // here the sceneCountInBuildSetting is the number of all the scenes in the build setting. 
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(nextLevelNumber);
                }
            }
        }

        public void LoadNextLevelAuto()
        {
            int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            int nextLevelNumber = currentSceneNumber + 1;

                if (currentSceneNumber == SceneManager.sceneCountInBuildSettings - 1)  // here the sceneCountInBuildSetting is the number of all the scenes in the build setting. 
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(nextLevelNumber);
                }
        }



        public void Levels()
        {
            int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            switch (currentSceneNumber)
            {
                case 0:
                    lvl = levels.Level1;
                    levelName = "Level 1";
                    break;
                case 1:
                    lvl = levels.Level2;
                    levelName = "Level 2";
                    break;
                case 2:
                    lvl = levels.Level3;
                    levelName = "Level 3";
                    break;
                case 3:
                    lvl = levels.Level4;
                    levelName = "Level 4";
                    break;
                case 4:
                    lvl = levels.Level5;
                    levelName = "Level 5";
                    break;
                default:
                    lvl = levels.Level1;
                    levelName = "Level 1";
                    break;

            }
        }
        #endregion
    }

}
