using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationPickup : MonoBehaviour
{
    public PlayerAbilities.mutationAbility ability;
    public PlayerAbilities.mutationType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerAbilities>().GiveMutation(ability, type);
            MutationPickup[] allPickups = FindObjectsOfType<MutationPickup>();
            for(int i = 0; i < allPickups.Length; i++)
            {
                Destroy(allPickups[i].gameObject);
            }
        }
    }
}
