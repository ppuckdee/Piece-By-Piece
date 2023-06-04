using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public enum mutationType { NULL, ROBOT, PLANT, ANIMAL };
    public enum mutationAbility { GRAPPLE, SUPER_JUMP, SLIDE }

    public float grappleRange;

    public GameObject grappleUIObject;

    public List<Mutation> mutations;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mutations = new List<Mutation>();
    }

    // Update is called once per frame
    void Update()
    {
        mutationType grappleType = hasAbility(mutationAbility.GRAPPLE);
        mutationType superJumpType = hasAbility(mutationAbility.SUPER_JUMP);
        mutationType slideType = hasAbility(mutationAbility.SLIDE);
        if(grappleType != mutationType.NULL || true)
        {
            Vector2 playerPosition = transform.position;
            Vector2 playerPositionScreen = (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            Vector2 cursorPos = Input.mousePosition;
            Vector2 cursorPosWorld = Camera.main.ScreenToWorldPoint(cursorPos);
            if((cursorPosWorld-playerPosition).magnitude > grappleRange)
            {
                cursorPos = (Vector2)Camera.main.WorldToScreenPoint(playerPosition+(cursorPosWorld-playerPosition).normalized * grappleRange);
            }
            
            grappleUIObject.transform.position = cursorPos;
        }
        if(superJumpType != mutationType.NULL)
        {
            
        }
        if(slideType != mutationType.NULL)
        {
            
        }
    }

    private mutationType hasAbility(mutationAbility ability)
    {
        for(int i = 0; i < mutations.Count; i++)
        {
            if(mutations[i]. ability == ability)
            {
                return mutations[i].type;
            }
        }
        return mutationType.NULL;
    }

    public struct Mutation
    {
        public mutationType type;
        public mutationAbility ability;
        public Mutation(mutationType type, mutationAbility ability)
        {
            this.type = type;
            this.ability = ability;
        }
    }
}
