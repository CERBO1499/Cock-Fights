using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //Añadir el namespace para poder implementar sus interfaces

public class Gestures3DCollider : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {

    public Text textBox;

    private float zAxis = 0;
    private Vector3 clickOffset = Vector3.zero;

    private void Start()
    {
        zAxis = transform.position.z;
    }

    //Se ejecuta repetidamente mientras se esté arrastrando
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = CalculateScreenPointToWoldOnPlane(eventData.position, zAxis) + clickOffset;
        //textBox.text = "Está siendo arrastrado";

        transform.gameObject.GetComponent<LaunchedWord>().Speed = 0;
    }

    //Se ejecuta cuando ha empezado a arrastrarse, antes del OnDrag
    public void OnBeginDrag(PointerEventData eventData)
    {
        clickOffset = transform.position - CalculateScreenPointToWoldOnPlane(eventData.position, zAxis);
        //textBox.text = "Va a ser arrastrado";
    }
    
    //Se ejecuta cuando se ha soltado, antes del OnDrop
    public void OnEndDrag(PointerEventData eventData)
    {
        //textBox.text = "Va a ser soltado";
    }
    

    //Se ejecuta al finalizar la pulsación completa (levantar el dedo o ratón)
    public void OnPointerClick(PointerEventData eventData)
    {
        //textBox.text = "Ha sido pulsado";
    }

    //Se ejecuta cuando el punto del ratón pasa por encima
    public void OnPointerEnter(PointerEventData eventData)
    {
        //textBox.text = "El ratón está encima";
    }

    //Se ejecuta cuando el puntero, después de haber pasado por encima, sale de su collider
    public void OnPointerExit(PointerEventData eventData)
    {
        //textBox.text = "El ratón ya NO está encima";
    }

    public Vector3 CalculateScreenPointToWoldOnPlane(Vector3 screenPosition, float zPos)
    {
        float enterDist;
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPos));
        Ray rayCast = Camera.main.ScreenPointToRay(screenPosition);
        plane.Raycast(rayCast, out enterDist);
        return rayCast.GetPoint(enterDist);
    }
}
