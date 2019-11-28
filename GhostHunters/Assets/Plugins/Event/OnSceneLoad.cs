using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.CandleBoy
{
	public class OnSceneLoad : MonoBehaviour
	{
		public GameEvent onSceneLoad;

		void Start()
		{
			SceneManager.sceneLoaded += (Scene, LoadSceneMode) => onSceneLoad.Raise();
			onSceneLoad.Raise();
		}
	}
}