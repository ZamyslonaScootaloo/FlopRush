using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshmovement : MonoBehaviour
{
    public NavMeshAgent _navMeshAgent;
    
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer; //Ustalamy Layer Podłogi i Gracza

	//Patrolowanie
	public Vector3 walkPoint; // Miejsce gdzie przeciwnik wraca do tego miejsca
	bool isPatrolling; //czy Patroluje
	public float walkPointRange; // Wyznacza zasięg dla ,,walkPoint"

	//Atakowanie
	public float timeBetwenAttacking; //Czas pomiędzy strzałami
	public bool isAttack; //Czy Atakuje
	public GameObject bullet; //Czym przeciwnik ma strzelać
    public float speedBullet; //Szybkośc strzału

    Vector3 lookAtPlayer;

	//Wartości
	public float sightRange, attackRange;   //sightRange - W jakim polu widzi gracza i idzie do niego, AttackRange- miejsce gdzie przeciwnik atakuje gracz
	public bool playerInSightRange, playerInAttackRange; // Czy gracz jest w zasiegu pola widzenia, Czy gracz jest w zasięgu atakowania

	private void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	public void Update()
	{
        lookAtPlayer = new Vector3(player.position.x, 1.08f, player.position.z);
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); //Wyznaczamy Granice dla pola widzenia
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); //Wyznaczamy Granice dla Atakowania
        if (!playerInSightRange && !playerInAttackRange) Patroling(); //Patroluj jeżeli Przeciwnik nie widzi i nie atakue gracz
		if (playerInSightRange && !playerInAttackRange) ChasePlayer(); // Idz w strone gracza jezeli go widzisz
		if (playerInAttackRange && playerInSightRange) AttackPlayer(); // Atakuj gracza jezeli jest w zasiegu pola widzenia i ataku
	}


	public void Patroling() //System Patrolowania
	{
		if (!isPatrolling) SearchWalkPoint();

		if (isPatrolling)
			_navMeshAgent.SetDestination(walkPoint);

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		//jeżeli jest w zasięgu początkowego miejsca
		if (distanceToWalkPoint.magnitude < 1f)
			isPatrolling = false;
	}

    private void SearchWalkPoint() // Wyznacz zasięg miejsca przeciwnika
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            isPatrolling = true;
    }

    private void ChasePlayer() //Idz w strone gracza
    {
        _navMeshAgent.SetDestination(player.position);
    }

    private void AttackPlayer() // Atakuj Gracza
    {
        //Make sure enemy doesn't move
        _navMeshAgent.SetDestination(transform.position);

        transform.LookAt(lookAtPlayer);
        

        if (!isAttack)
        {
            ///Wystrzał pocisku
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * speedBullet, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f *(player.transform.position.y * 1.3f), ForceMode.Impulse);
            ///Koniec Wystrzału pocisku

            isAttack = true;
            Invoke(nameof(ResetAttack), timeBetwenAttacking);
        }
    }
    private void ResetAttack() //Resetuje Atak 
    {
        isAttack = false;
    }

    private void DestroyEnemy() // Dodać pózniej niszczenie jeżeli przeciwnik straci zdrowie
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //Kolor czerwony 
        Gizmos.DrawWireSphere(transform.position, attackRange);  //Wyznacz linie dla zasięgu ataku
        Gizmos.color = Color.yellow; 
        Gizmos.DrawWireSphere(transform.position, sightRange); //Wyznacz linie dla zasięgu pola widzenia
    }
}
