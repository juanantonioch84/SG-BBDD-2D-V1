using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum NodeType { None, Entity, Relation, Attribute }

public class NodeController : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler
{
    public NodeType _nodeType;

    protected GameObject _wordOrigin;
    protected DiagramController _diagramController;
    protected Graphic _currentGraphic;
    protected bool _isBeingEdited;
    protected RectTransform _rt;
    protected RectTransform _parentRt;
    protected Vector2 _startMousePos;
    protected Vector2 _startPos;
    protected float _myWidth;
    protected float _myHeight;
    protected GameObject _nodeMenu;
    protected GameObject _canvas;


    protected void Start()
    {
        _diagramController = transform.parent.gameObject.GetComponent<DiagramController>();
        _rt = gameObject.GetComponent<RectTransform>();

        // Use to control the drag movement
        _parentRt = transform.parent.gameObject.GetComponent<RectTransform>();
        _myWidth = (_rt.rect.width + 5) / 2;
        _myHeight = (_rt.rect.height + 5) / 2;

        // Checks if the user is linking this node with another
        _isBeingEdited = false;

        _currentGraphic = gameObject.GetComponent<Image>();
        _canvas = GameObject.FindGameObjectWithTag("DiagramCanvas").gameObject;
        _nodeMenu = _canvas.transform.Find("NodeMenu").gameObject;
    }

    // Storage the positions when user clicks on node
    public void OnPointerDown(PointerEventData e)
    {
        _startPos = transform.position;
        _startMousePos = e.position;
    }

    // Control the drag and drop of the node
    public void OnDrag(PointerEventData e)
    {
        transform.position = _startPos + e.position - _startMousePos;

        bool restrictX = (transform.localPosition.x < 0 - ((_parentRt.rect.width / 2) - _myWidth) || transform.localPosition.x > ((_parentRt.rect.width / 2) - _myWidth));
        bool restrictY = (transform.localPosition.y < 0 - ((_parentRt.rect.height / 2) - _myHeight) || transform.localPosition.y > ((_parentRt.rect.height / 2) - _myHeight));

        if (restrictX) {
            float fakeX = transform.localPosition.x < 0 ? 0 - (_parentRt.rect.width / 2) + _myWidth : (_parentRt.rect.width / 2) - _myWidth;
            transform.localPosition = new Vector2(fakeX, transform.localPosition.y); ;
        }

        if (restrictY) {
            float fakeY = transform.localPosition.y < 0 ? 0 - (_parentRt.rect.height / 2) + _myHeight : (_parentRt.rect.height / 2) - _myHeight;
            transform.localPosition = new Vector2(transform.localPosition.x, fakeY);
        }
    }

    public virtual void OnPointerClick(PointerEventData e)
    {
        // With left click, link/unlink this node with the one which started the edition
        if (IsAnotherNodeBeingEdited() && e.button == PointerEventData.InputButton.Left) {

            ERLine erLine = GlobalController.Instance.SearchERLine(_diagramController.GetFirstNode().gameObject.name, gameObject.name);

            // Link this node if another node is trying to be linked and they are not linked
            if (IsAnotherNodeBeingLinked() && erLine == null) {
                LinkNode();
            }
            // Unlink this node if another node is trying to be unlinked and they are linked
            else if (!IsAnotherNodeBeingLinked() && erLine != null) {
                UnlinkNode();
            }
        } else if (IsAnotherNodeBeingEdited()) {
            StopEditingNode();
        }

        // Right click open the menu
        if (e.button == PointerEventData.InputButton.Right) {
            _nodeMenu.GetComponent<NodeMenuController>().Init(gameObject, _canvas.transform.InverseTransformPoint(e.position));
        }
    }

    // Set the word origin used to add the node into the diagramº
    public void SetWordOrigin(GameObject wordOrigin)
    {
        _wordOrigin = wordOrigin;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // Is user is editing another node of a different type, set node to link/unlink
        if (IsAnotherNodeBeingEdited() && _nodeType != _diagramController.GetNodeType()) {

            //ERLineModel erLine = GlobalController.Instance.SearchLink(_diagramController.GetFirstNode(), _rt);
            ERLine erLine = GlobalController.Instance.SearchERLine(_diagramController.GetFirstNode().gameObject.name, gameObject.name);

            // Set the node as the second node if another node is trying to be linked and they are not linked
            if (IsAnotherNodeBeingLinked() && erLine == null) {
                _currentGraphic.color = Color.green;
                _diagramController.SetSecondNode(_rt);
            }
            // Set the node as the second node if another node is trying to be unlinked and they are linked
            else if (! IsAnotherNodeBeingLinked() && erLine != null) {
                _currentGraphic.color = Color.red;
                _diagramController.SetSecondNode(_rt);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // If user is linking another node, return original color
        if (IsAnotherNodeBeingEdited()) {
            _currentGraphic.color = Color.white;
            _diagramController.UnsetSecondNode();
        }
    }

    // When the dragging is ended
    public void OnEndDrag(PointerEventData eventData)
    {
        // Update the position in Global controller, stored nodes
        GlobalController.Instance.StoredNodes[gameObject.name].SetPosition(transform.localPosition);
    }

    // Enable the word origin again and destroy this node and its links
    public void DestroyNode()
    {
        // Store the action
        ActionInfo actionInfo = new ActionInfo();
        actionInfo.Add("node_name", gameObject.name);
        actionInfo.Add("node_type", NodeModel.NodeTypeString(_nodeType));
        GameDataController.Add("destroy_node", actionInfo);


        // Update global controller information
        GlobalController.Instance.RemoveNodeERLines(gameObject.name, transform.parent);
        GlobalController.Instance.StoredNodes.Remove(gameObject.name);

        _wordOrigin.GetComponent<WordController>().EnableWord();
        Destroy(gameObject);
    }

    // Is the user clicks over link/unlink button, start editing process
    public virtual void StartEditingNode(bool isLinking)
    {
        _currentGraphic.color = isLinking ? Color.green : Color.red;

        _isBeingEdited = true;

        _diagramController.StartEditing(_rt, _nodeType, isLinking);
    }

    // Stop the link/unlink process
    public void StopEditingNode()
    {
        StopBeingEdited();
        _diagramController.StopEditing();
    }

    // Stop to be the linking node
    public void StopBeingEdited()
    {
        _currentGraphic.color = Color.white;
        _isBeingEdited = false;
    }

    // Check if the user is editing another node
    protected bool IsAnotherNodeBeingEdited()
    {
        return _diagramController.GetEditionType() != EditionType.None && !_isBeingEdited;
    }

    // Check if the user is linking another node
    protected bool IsAnotherNodeBeingLinked()
    {
        return _diagramController.GetEditionType() == EditionType.Link && !_isBeingEdited;
    }

    // Make the link between nodes
    protected virtual void LinkNode()
    {
        _diagramController.LinkNodes();
        StopEditingNode();
    }

    // Make the unlink between nodes
    protected virtual void UnlinkNode()
    {
        _diagramController.UnlinkNodes();
        StopEditingNode();
    }
}
