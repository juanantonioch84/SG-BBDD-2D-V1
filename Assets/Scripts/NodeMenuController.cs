using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeMenuController : MonoBehaviour, IPointerExitHandler
{
    public GameObject Node { get; set; }
    public GameObject _linkButton;
    public GameObject _unlinkButton;

    private NodeController _nodeController;

    public void Init(GameObject node, Vector2 pos)
    {
        Node = node;
        transform.localPosition = new Vector2(pos.x, pos.y + 10.0f);
        _nodeController = Node.GetComponent<NodeController>();

        if (_nodeController._nodeType == NodeType.Attribute) {
            if (Node.GetComponent<NodeAttributeController>().isLinked()) {
                _linkButton.SetActive(false);
                _unlinkButton.SetActive(true);
            } else {
                _linkButton.SetActive(true);
                _unlinkButton.SetActive(false);
            }
        } else {
            _linkButton.SetActive(true);
            _unlinkButton.SetActive(true);
        }

        gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseMenu();
    }

    public void OnRemoveClick()
    {
        if (Node != null) {
            _nodeController.DestroyNode();
        }
        CloseMenu();
    }

    public void OnLinkClick()
    {
        if (Node != null) {
            _nodeController.StartEditingNode(true);
        }
        CloseMenu();
    }

    public void OnUnlinkClick()
    {
        if (Node != null) {
            _nodeController.StartEditingNode(false);
        }
        CloseMenu();
    }

    private void CloseMenu()
    {
        Node = null;
        _nodeController = null;
        gameObject.SetActive(false);
    }
}
