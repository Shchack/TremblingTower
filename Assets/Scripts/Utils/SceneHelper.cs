using UnityEngine.SceneManagement;

namespace EG.Tower.Utils
{
    public static class SceneHelper
    {
        private const int CREATE_HERO_SCENE_INDEX = 0;
        private const int GAMEPLAY_SCENE_INDEX = 1;

        public static void LoadCreateHeroScene()
        {
            SceneManager.LoadScene(CREATE_HERO_SCENE_INDEX, LoadSceneMode.Single);
        }

        public static void LoadGameplayScene()
        {
            SceneManager.LoadScene(GAMEPLAY_SCENE_INDEX, LoadSceneMode.Single);
        }
    }
}
