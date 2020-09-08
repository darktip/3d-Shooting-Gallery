using UnityEngine;

namespace UI
{
    // SO that holds prefabs for all game screens
    [CreateAssetMenu(fileName = "Game Windows", menuName = "Game Windows", order = 50)]
    public class GameWindows : ScriptableObject
    {
        public WindowBase mainWindow;
        public WindowBase highScoreWindow;
        public WindowBase winScreen;
        public WindowBase looseScreen;
    }
}
