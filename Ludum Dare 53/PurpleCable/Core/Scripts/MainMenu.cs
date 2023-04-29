using Godot;

namespace PurpleCable
{
	public partial class MainMenu : Control
	{
		public override void _Ready()
		{   
			//SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		}

		public void StartGame()
		{
			GoToScene("Main");
		}

		public void ShowCredits()
		{
			GoToScene("Credits");
		}

		public void ShowControls()
		{
			GoToScene("Controls");
		}

		public void ShowSettings()
		{
			GoToScene("Settings");
		}

		public void ShowOptions()
		{
			GoToScene("Options");
		}

		public void GoToMenu()
		{
			GoToScene("Menu");
		}

		public void QuitGame()
		{
			GetTree().Quit();
		}

		public void GoToScene(string sceneName)
		{
			NodeExtensions.GoToScene(this, sceneName);
		}

		//private void SceneManager_sceneLoaded(PackedScene arg0, LoadSceneMode arg1)
		//{
		//    FadeInOut.FadeIn();
		//}
	}
}
