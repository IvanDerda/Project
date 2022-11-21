using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Camera : MonoBehaviour
{
    int currentCamIndex=0;

    WebCamTexture tex;

    public RawImage display;

    

    DateTime indeficator;
    string name;

    Vector3 scale;

    public void SwamCam_clicked()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            currentCamIndex += 1;
            currentCamIndex %= WebCamTexture.devices.Length;
            if(currentCamIndex == 1)
            {
                display.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (currentCamIndex == 0)
            {
                display.transform.rotation = Quaternion.Euler(0, 0, -90);
            }

            if(tex != null)
            {
                StopWebCam();
                StartStopCam_Clicked();
            }
        }
    }

    public void StartStopCam_Clicked()
    {
        if (tex != null)
        {
            StopWebCam();
        }
        else
        {
            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            tex = new WebCamTexture(device.name);
            display.texture = tex;
            tex.Play();
           
        }

      
    }

    public void StopWebCam()
    {
 display.texture = null;
            tex.Stop();
            tex = null;
            
    }


    public void SaveImage()
    {
        indeficator = DateTime.Now;
        name = indeficator.ToString();

        name = name.Replace(' ', '_');
        name = name.Replace('.','_');
        name = name.Replace(':', '_');
        
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(display.texture.width, display.texture.height, TextureFormat.ARGB32, false);

        //Save the image to the Texture2D
        texture.SetPixels(tex.GetPixels());
        texture.Apply();

        //Encode it as a PNG.
        byte[] bytes = texture.EncodeToPNG();
        Debug.Log(name);
        //Save it in a file.
      
        File.WriteAllBytes("/storage/emulated/0/DCIM/Camera/" + "IMG" + name + ".png", bytes);
    }
}

    