using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshmovement : MonoBehaviour
{
    [SerializeField]
    Transform pointA; //Obiekt do którego ma iść przeciwnik
    NavMeshAgent _navMeshAgent; //NavMeshAgent musi być w przeciwniku 
    void Start()
    {
        _navMeshAgent = this.gameObject.transform.GetComponent<NavMeshAgent>(); // pobierz _nMA z tego obieku


    }
	public void Update()
	{
		if(_navMeshAgent != null) //jeżeliw obiekcie jest _navMeshAgent to idz w strone pointA
		{
            SetDistination();

        }
		else
		{
            Debug.Log("Nie ma NavMeshu w gameObjecie");
		}
    }

	public void SetDistination() // Bierzemy pozycje z PointA i ustawiamy wartość dla NavMeshu
	{
        Vector3 targetPoint = pointA.transform.position;
        _navMeshAgent.SetDestination(targetPoint);
    }
}
