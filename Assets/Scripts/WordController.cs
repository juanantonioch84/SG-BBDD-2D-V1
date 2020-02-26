using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;

public class WordController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject erEntityPrefab;
    public GameObject erAttributePrefab;
    public GameObject erRelationPrefab;

    public RectTransform erEntityObject;
    public RectTransform erAttributeObject;
    public RectTransform erRelationObject;

    public RectTransform tooltip;

    private Image _buttonGraphic;
    private Image _erEntityObjectGraphic;
    private Image _erAttributeObjectGraphic;
    private Image _erRelationObjectGraphic;
    private Text _text;
    private int _originalFont;

    public NodeType NodeType { get; set; }
    private GameObject _nodeInstance;
    private GameObject _diagramWindow;
    private bool _wordAdded;
    private Graphic _currentGraphic;
    private bool _pointerIsInside;

    // Init in the awake instead start because we will access to the words objects to load last state of diagram
    void Awake()
    {
        // Attributes used to load previous state of the diagram scene
        _currentGraphic = gameObject.GetComponent<Image>();
        _wordAdded = false;
        NodeType = NodeType.None;
        _buttonGraphic = gameObject.GetComponent<Image>();
        _erEntityObjectGraphic = erEntityObject.gameObject.GetComponent<Image>();
        _erRelationObjectGraphic = erRelationObject.gameObject.GetComponent<Image>();
        _erAttributeObjectGraphic = erAttributeObject.gameObject.GetComponent<Image>();
        _diagramWindow = GameObject.Find("DiagramContent");
        _text = gameObject.GetComponentInChildren<Text>();
        _originalFont = _text.fontSize;
        
        _pointerIsInside = false;
    }

    private void Start()
    {
    }

    void Update()
    {
        // Paint using a different color if the word can be added with a click
        if (! _wordAdded) {
            if (_pointerIsInside && Input.GetKey(KeyCode.LeftControl) && NodeType != NodeType.None) {
                _currentGraphic.color = Color.yellow;
            } else if (NodeType != NodeType.None) {
                _currentGraphic.color = Color.white;
            }
        }
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerIsInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerIsInside = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // If the word has not been added, click + ctrl and the nodetype has been selected, add the node
        if (!_wordAdded && Input.GetKey(KeyCode.LeftControl) && NodeType != NodeType.None) {

            switch(NodeType) {
                case NodeType.Entity:
                    _nodeInstance = Instantiate(erEntityPrefab, Vector2.zero, Quaternion.identity) as GameObject;
                    break;
                case NodeType.Attribute:
                    _nodeInstance = Instantiate(erAttributePrefab, Vector2.zero, Quaternion.identity) as GameObject;
                    break;
                case NodeType.Relation:
                    _nodeInstance = Instantiate(erRelationPrefab, Vector2.zero, Quaternion.identity) as GameObject;
                    break;
            }

            _nodeInstance.name = gameObject.name;
            _nodeInstance.transform.SetParent(_diagramWindow.transform, false);

            _nodeInstance.transform.SetAsLastSibling();
            _nodeInstance.GetComponentInChildren<Text>().text = _text.text;
            _nodeInstance.GetComponent<NodeController>().SetWordOrigin(gameObject);

            DisableWord();

            // Update GlobalController state
            GlobalController.Instance.StoredNodes.Add(gameObject.name, new NodeModel(Vector2.zero, NodeType, gameObject.name, _text.text));

            // Store the action
            ActionInfo actionInfo = new ActionInfo();
            actionInfo.Add("node_name", gameObject.name);
            actionInfo.Add("node_type", NodeModel.NodeTypeString(NodeType));
            GameDataController.Add("add_node", actionInfo);
        }
        //Disable the button for a short time
        else if (!_wordAdded && Input.GetKey(KeyCode.LeftControl) && NodeType == NodeType.None) {

            StartCoroutine(TemporalDisableButton());
        }
        // If the word has not been added, change the type of the node
        else if (!_wordAdded) {

            // Store the action
            ActionInfo actionInfo = new ActionInfo();
            actionInfo.Add("node_name", gameObject.name);
            actionInfo.Add("node_type_from", NodeModel.NodeTypeString(NodeType));

            // Change the node type: Button -> Entity -> Attribute -> Relation -> Entity...
            switch (NodeType) {
                case NodeType.None:
                case NodeType.Relation:
                    ChangeNodeType(NodeType.Entity);
                    break;
                case NodeType.Entity:
                    ChangeNodeType(NodeType.Attribute);
                    break;
                case NodeType.Attribute:
                    ChangeNodeType(NodeType.Relation);
                    break;
            }

            actionInfo.Add("node_type_to", NodeModel.NodeTypeString(NodeType));
            GameDataController.Add("change_node_type", actionInfo);
        }
    }

    // Change the node type
    public void ChangeNodeType(NodeType nodeType)
    {
        switch (nodeType) {
            case NodeType.Entity:
                _buttonGraphic.enabled = false;
                _erRelationObjectGraphic.enabled = false;
                _erAttributeObjectGraphic.enabled = false;
                _erEntityObjectGraphic.enabled = true;
                NodeType = NodeType.Entity;
                _currentGraphic = _erEntityObjectGraphic;
                // Return the text to original values
                _text.text = gameObject.name;
                _text.fontSize = _originalFont;
                break;
            case NodeType.Attribute:
                _buttonGraphic.enabled = false;
                _erRelationObjectGraphic.enabled = false;
                _erAttributeObjectGraphic.enabled = true;
                _erEntityObjectGraphic.enabled = false;
                NodeType = NodeType.Attribute;
                _currentGraphic = _erAttributeObjectGraphic;
                // Change the text
                _text.text = gameObject.name.Length >= 6 ? gameObject.name.Substring(0, 6).ToLower() : gameObject.name.ToLower();
                _text.fontSize = _originalFont - 2;
                break;
            case NodeType.Relation:
                _buttonGraphic.enabled = false;
                _erRelationObjectGraphic.enabled = true;
                _erAttributeObjectGraphic.enabled = false;
                _erEntityObjectGraphic.enabled = false;
                NodeType = NodeType.Relation;
                _currentGraphic = _erRelationObjectGraphic;
                // Return the text to original values
                _text.text = gameObject.name;
                _text.fontSize = _originalFont;
                break;
        }
    }

    private IEnumerator TemporalDisableButton()
    {
        gameObject.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Button>().interactable = true;
    }

    // Enable the word to be used
    public void EnableWord()
    {
        _currentGraphic.color = Color.white;
        _wordAdded = false;
    }
    
    // Disable the word to be used
    public void DisableWord()
    {
        _currentGraphic.color = Color.yellow;
        _wordAdded = true;
    }
}
