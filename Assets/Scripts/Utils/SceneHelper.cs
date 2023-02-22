using UnityEngine.SceneManagement;

namespace EG.Tower.Utils
{
    public static class SceneHelper
    {
        private const int CREATE_HERO_SCENE_INDEX = 0;
        private const int DIALOGUE_SCENE_INDEX = 1;
        private const int MAP_SCENE_INDEX = 2;
        private const int BATTLE_SCENE_INDEX = 3;

        public static void LoadCreateHeroScene()
        {
            SceneManager.LoadScene(CREATE_HERO_SCENE_INDEX, LoadSceneMode.Single);
        }

        public static void LoadMapScene()
        {
            SceneManager.LoadScene(MAP_SCENE_INDEX, LoadSceneMode.Single);
        }

        public static void LoadDialogueScene()
        {
            SceneManager.LoadScene(DIALOGUE_SCENE_INDEX, LoadSceneMode.Single);
        }

        public static void LoadBattleScene()
        {
            SceneManager.LoadScene(BATTLE_SCENE_INDEX, LoadSceneMode.Single);
        }
    }
}
