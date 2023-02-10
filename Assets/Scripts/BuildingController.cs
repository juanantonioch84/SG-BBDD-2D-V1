using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour
{
    public VideoClip _video;
    public string _stageName;
    public bool _isVideoStage;
    public string _buildingCode;
    public string _tooltipName;
    [Range(0.01f, 1.0f)]
    public float _lightIncrement;
    [Range(0.01f, 1.0f)]
    public float _lightIncrementInterval;
    public Dictionary<string, string> _notes;
    public List<string> _words;

    private Light _spotLight;
    private GameObject _inventoryBar;
    private ScenesHandler _sceneHandler;
    private VideoPlayer _videoPlayer;
    private float _lightIntensity;

    private void Awake()
    {
        // Retrieve the notes and the words of this building and load them into the properties
        _notes = NotesData.GetNoteDictionary(_buildingCode);
        _words = NotesData.GetWordList(_buildingCode);
    }

    private void Start()
    {
        _inventoryBar = GameObject.Find("InventoryBar");
        _sceneHandler = _inventoryBar.GetComponent<ScenesHandler>();
        _videoPlayer = Camera.main.gameObject.GetComponent<UnityEngine.Video.VideoPlayer> ();
        _spotLight = gameObject.GetComponent(typeof(Light)) as Light;

        // It this building was visited, do not shine
        if (! GlobalController.Instance._visitedBuildings.Contains(_buildingCode)) {
            InvokeRepeating("toggleLight", 0.1f, _lightIncrementInterval);
        }
    }

    void toggleLight()
    {
        _lightIntensity += _lightIncrement;
        _spotLight.intensity = Mathf.Abs(Mathf.Sin(_lightIntensity));
    }
 
    void OnMouseDown()
    {
        if (_sceneHandler.isGamePaused == false) {
            SaveVisit();
            SceneManager.LoadScene(_stageName);
        }
        /*else if (_isVideoStage && !_videoPlayer.isPlaying) {

            _videoPlayer.clip = _video;

            // Remove the video once it is finished
            _videoPlayer.loopPointReached += EndReached;

            _videoPlayer.Play();

            // Hide inventory bar
            _inventoryBar.SetActive(false);

            // Save visit
            SaveVisit();
        }
        */
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        /*vp.Stop();

        // Show inventory bar
        _inventoryBar.SetActive(true);
        */
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
