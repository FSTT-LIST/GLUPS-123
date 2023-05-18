using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Resolution currentResolution = Screen.currentResolution;
            // Set the desired width and height
            int targetWidth = (int)(currentResolution.width / 3.42f);

            // Retrieve the screen height in pixels
            int screenHeight = (int) (currentResolution.height / 1.15f);
            // Set the resolution in windowed mode
            Screen.SetResolution(targetWidth, screenHeight, FullScreenMode.Windowed);
        }
    }
}