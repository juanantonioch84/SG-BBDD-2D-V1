using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

[RequireComponent(typeof(UILineRenderer))]
public class UILineRendererController : MonoBehaviour
{
    public RectTransform _rt1;
    public RectTransform _rt2;
    public RectTransform _cardinality;
    public Dropdown Dropdown { get; set; }

    private NodeType _node1Type;
    private NodeType _node2Type;

    private UILineRenderer _line;

    // Use this for initialization
    void Awake()
    {
        // Get the UI Line Renderer of the GameObject
        _line = gameObject.GetComponent<UILineRenderer>();
        Dropdown = _cardinality.gameObject.activeSelf ? _cardinality.gameObject.GetComponent<Dropdown>() : null;

        if (_rt1 != null) {
            _node1Type = _rt1.gameObject.GetComponent<NodeController>()._nodeType;
        }

        if (_rt2 != null) {
            _node2Type = _rt2.gameObject.GetComponent<NodeController>()._nodeType;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update position of the two vertex of the UI Line Renderer
        _line.Points = new Vector2[] {
            _rt1.localPosition,
            _rt2.localPosition,
        };

        // Update cardinality position
        _cardinality.anchoredPosition = new Vector2(
            _rt1.anchoredPosition.x + (_rt2.anchoredPosition.x - _rt1.anchoredPosition.x) / 2,
            _rt1.anchoredPosition.y + (_rt2.anchoredPosition.y - _rt1.anchoredPosition.y) / 2
        );
    }

    // Set the nodes to link
    public void SetLinkNodes(RectTransform node1, RectTransform node2)
    {
        _rt1 = node1;
        _rt2 = node2;

        _node1Type = _rt1.gameObject.GetComponent<NodeController>()._nodeType;
        _node2Type = _rt2.gameObject.GetComponent<NodeController>()._nodeType;
    }

    // Cardinality value changed
    public void OnCardinalityChanged()
    {
        // Update cardinality in global controller
        string lineName = ERLine.CreateName(_rt1.gameObject.name, _rt2.gameObject.name);
        GlobalController.Instance.StoredLines[lineName].Cardinality = Dropdown.value;

        // Store the action
        ActionInfo actionInfo = new ActionInfo();
        actionInfo.Add("node1_name", _rt1.gameObject.name);
        actionInfo.Add("node1_type", NodeModel.NodeTypeString(_node1Type));
        actionInfo.Add("node2_name", _rt1.gameObject.name);
        actionInfo.Add("node2_type", NodeModel.NodeTypeString(_node2Type));
        actionInfo.Add("line_name", lineName);
        actionInfo.Add("cardinality", Dropdown.options[Dropdown.value].text);
        GameDataController.Add("set_cardinality", actionInfo);
    }

}
