using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenCapture : MonoBehaviour
{
    public string filename;
    public Shader unlitShader;

    void CamCapture()
    {
        Camera cam = GetComponent<Camera>();
        cam.SetReplacementShader(unlitShader, "");
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        cam.Render();

        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = currentRT;

        var bytes = image.EncodeToPNG();
        Destroy(image);

        File.WriteAllBytes("Floorplans/" + filename + ".png", bytes);
    }

    private void OnApplicationQuit()
    {
        CamCapture();
    }
}
