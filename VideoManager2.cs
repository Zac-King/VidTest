using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
class zVideo
{
    public string name;        // Identifier    // Mainly for the benifit of the Developer
    public string videoUrl;    // Dowload link to video 
    public bool waitAtEnd = false;  // Wait on completion to go to next  // Defualt to false
    public bool loop = false;       // Loop video on end
    public List<zVideo> potentialVideos;    //
    public MediaPlayerCtrl videoPlayerCtrl; // 
}

public class VideoManager2 : MonoBehaviour
{
    // Variables ///////////////////////////////////////////////////////////////////////////////////////
    [SerializeField] private GameObject VideoTextureScreen;     // Target Object to use this movie Texture
    public UnityEngine.UI.Text DebugScreenText;                 // Debug info to screen     // Remove before ship
    [SerializeField] private List<zVideo> allVideos = new List<zVideo>();   // List of all video that will be handled by this VideoManager
    zVideo currentVideo;                                        // Current video in use
    
    // MonoBehavior ////////////////////////////////////////////////////////////////////////////////////
    [ContextMenu("Test")]
    void Start ()   // Use this for initialization
    {
        foreach(zVideo v in allVideos)
        {
            GameObject g = new GameObject();
            g.transform.parent = transform;
            g.AddComponent<MediaPlayerCtrl>();

            v.videoPlayerCtrl = g.GetComponent<MediaPlayerCtrl>();      // 
            v.videoPlayerCtrl.m_strFileName = v.videoUrl;   // Setting each video's target path to the value passed into the list 
            v.videoPlayerCtrl.m_TargetMaterial = new GameObject[1];
            v.videoPlayerCtrl.m_TargetMaterial[0] = VideoTextureScreen; // Setting video's screens. In this case only the one
            v.videoPlayerCtrl.m_bLoop = v.loop;             // Does Video loop
        }

        //currentVideo = allVideos[0];

        if(allVideos.Count > 0) // As long as there is something in the list of videos
        {
            currentVideo = allVideos[0]; // Set intial video
            StartCoroutine(LoadVideo(currentVideo));
        }
    }
	
	void Update ()  // Update is called once per frame
    {
        DebugScreenText.text = allVideos.FindIndex(a => a == currentVideo).ToString() + " " + currentVideo.videoPlayerCtrl.GetCurrentState().ToString() + " " +currentVideo.videoPlayerCtrl.GetCurrentSeekPercent().ToString();
	}

    // Functions ///////////////////////////////////////////////////////////////////////////////////////
    IEnumerator LoadVideo(zVideo loadTarget)
    {
        MediaPlayerCtrl vc = loadTarget.videoPlayerCtrl;    // mediaPlayerCtrl we will be messing with

        vc.Load(loadTarget.videoUrl);   // Load video
        
        while(vc.GetCurrentState() != MediaPlayerCtrl.MEDIAPLAYER_STATE.READY)
            yield return false;  // Wait until video has been loaded

        vc.Play();

        Debug.Log("Done");

        yield return true;
    }

    //IEnumerator LoadBranches()
    //{
    //    foreach (zVideo branch in currentVideo.potentialVideos)
    //    {

    //    }
    //    yield return null;
    //}
    

}
