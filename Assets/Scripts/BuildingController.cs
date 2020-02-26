using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BuildingController : MonoBehaviour
{
    public VideoClip _video;
    public string _stageName;
    public bool _isVideoStage;
    public string _buildingCode;
    public float _lightIntensity;
    public Dictionary<string, string> _notes;
    public List<string> _words;

    private Light _spotLight;
    private GameObject _inventoryBar;
    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        // Retrieve the notes and the words of this building and load them into the properties
        _notes = NotesData.GetNoteDictionary(_buildingCode);
        _words = NotesData.GetWordList(_buildingCode);
    }

    private void Start()
    {
        _inventoryBar = GameObject.Find("InventoryBar");
        _videoPlayer = Camera.main.gameObject.GetComponent<UnityEngine.Video.VideoPlayer> ();
        _spotLight = gameObject.GetComponent(typeof(Light)) as Light;

        // It this building was visited, do not shine
        if (! GlobalController.Instance._visitedBuildings.Contains(_buildingCode)) {
            InvokeRepeating("toggleLight", 0.1f, 1.0f);
        }
    }

    void toggleLight()
    {
        _spotLight.intensity = _spotLight.intensity == _lightIntensity ? 0 : _lightIntensity;
    }

    void OnMouseDown()
    {
        if (! _isVideoStage) {

            SaveVisit();
            SceneManager.LoadScene(_stageName);

        } else if (_isVideoStage && !_videoPlayer.isPlaying) {

            _videoPlayer.clip = _video;

            // Remove the video once it is finished
            _videoPlayer.loopPointReached += EndReached;

            _videoPlayer.Play();

            // Hide inventory bar
            _inventoryBar.SetActive(false);

            // Save visit
            SaveVisit();
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Stop();

        // Show inventory bar
        _inventoryBar.SetActive(true);
    }

    void SaveVisit()
    {
        // Add Notes, Words and visited buildings to the global notebook
        GlobalController.Instance.AddNotes(_notes, _buildingCode);
        GlobalController.Instance.AddWords(_words);
        GlobalController.Instance.AddVisitedBuilding(_buildingCode);

        // Never shine the sprite again
        _lightIntensity = 0.0f;
    }
}
