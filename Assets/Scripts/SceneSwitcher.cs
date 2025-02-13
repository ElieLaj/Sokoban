using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void ReloadGame()
    {
        GameManager.Instance.ChangeState(GameState.RELOAD);
    }
}
