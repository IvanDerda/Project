using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{
    public Text GPSstatus;
    public Text Lan;
    public Text Lon;
    public Text Al;
    public Text Horiz;
    public Text Time;
    public bool isUpdating;
    public bool useGPS=false;

    public void Use()
    {
        useGPS = !useGPS;
    }

    private void Update()
    {
       
            if (!isUpdating)
            {
                StartCoroutine(GPSLoc());
                isUpdating = !isUpdating;
            }
        
    }

    IEnumerator GPSLoc()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }


        if (!Input.location.isEnabledByUser)
        {
            yield return new WaitForSeconds(3);
        }
        Input.location.Start();

        int maxWait = 3;
        while (Input.location.status==LocationServiceStatus.Initializing && maxWait>0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            GPSstatus.text = "Time out";
            Debug.Log(GPSstatus.text);
            yield break;
        }

        if(Input.location.status== LocationServiceStatus.Failed)
        {
            GPSstatus.text = "Unable to determine device location";
            Debug.Log(GPSstatus.text);
            yield break;
        }
        else
        {
            GPSstatus.text = "Running";
            Lan.text = Input.location.lastData.latitude.ToString();
            Lon.text = Input.location.lastData.longitude.ToString();
            Al.text = Input.location.lastData.altitude.ToString();
            Horiz.text = Input.location.lastData.horizontalAccuracy.ToString();
            Time.text = Input.location.lastData.timestamp.ToString();
        }

        isUpdating = !isUpdating;
        Input.location.Stop();

    }

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            GPSstatus.text = "Running";
            Lan.text= Input.location.lastData.latitude.ToString();
            Lon.text = Input.location.lastData.longitude.ToString();
            Al.text = Input.location.lastData.altitude.ToString();
            Horiz.text = Input.location.lastData.horizontalAccuracy.ToString();
           Time.text = Input.location.lastData.timestamp.ToString();

        }

        else
        {
            GPSstatus.text = "Stop";
        }
    }

}
