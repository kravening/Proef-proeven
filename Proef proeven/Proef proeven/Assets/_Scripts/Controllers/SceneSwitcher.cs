using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
	public class SceneSwitcher : MonoBehaviour
	{
		private const string SCENE_MAIN = "MainScene";
		public static SceneSwitcher Instance;
		

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
		
		private void Update()
		{
			KeyboardEvent();
		}
		private void KeyboardEvent()
		{
			if (Input.anyKeyDown)
			{
				LoadScene();
			}
		}

		public void Quit()
		{
			Application.Quit();
		}

		public void LoadScene()
		{
			SceneManager.LoadScene(SCENE_MAIN);
		}
	}
}
