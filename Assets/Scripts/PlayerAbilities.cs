using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAbilities : MonoBehaviour
{
    public enum mutationType { NULL, ROBOT, PLANT, ANIMAL };
    public enum mutationAbility { GRAPPLE, SUPER_JUMP, SLIDE };

    public float grappleRange;

    public GameObject grappleUIObject;

    public List<Mutation> mutations;

    public GameObject grappleOriginPoint, grappleLine;
    public float reelSpeed;
    public LayerMask grappleLayerMask;
    private Rigidbody2D rb;
    private Vector2 grapplePoint;
    private float swingLength;
    private bool swinging, startedOnReel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mutations = new List<Mutation>();
        rb = GetComponent<Rigidbody2D>();
        swinging = startedOnReel = false;
        grappleLine.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            CheatAllAbilities(mutationType.ROBOT);
        }

        mutationType grappleType = hasAbility(mutationAbility.GRAPPLE);
        mutationType superJumpType = hasAbility(mutationAbility.SUPER_JUMP);
        mutationType slideType = hasAbility(mutationAbility.SLIDE);

        if(grappleType != mutationType.NULL)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D mouseHit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            if(swinging || mouseHit.collider && mouseHit.collider.gameObject.tag == "GrapplePoint")
            {
                Vector2 grappleRay = Camera.main.ScreenToWorldPoint(Input.mousePosition)-grappleOriginPoint.transform.position;
                Debug.DrawRay(grappleOriginPoint.transform.position, grappleRay, Color.white);
                RaycastHit2D grappleHit = Physics2D.Raycast(grappleOriginPoint.transform.position, grappleRay, grappleRay.magnitude, grappleLayerMask);
                if(swinging || grappleHit && grappleHit.collider.gameObject.tag == "GrapplePoint")
                {
                    grappleUIObject.GetComponent<Image>().color = Color.green;
                    if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                    {
                        if(swinging)
                        {
                            startedOnReel = false;
                        }
                        if(!swinging)
                        {
                            startedOnReel = Input.GetMouseButtonDown(1);
                            startSwing((Vector2)grappleHit.collider.gameObject.transform.position + (Vector2)(grappleHit.collider.gameObject.transform.rotation * grappleHit.collider.offset));
                        }
                    }
                    else if(Input.GetMouseButtonUp(0) || (Input.GetMouseButtonUp(1) && startedOnReel))
                    {
                        Debug.Log(startedOnReel);
                        endSwing();
                    }
                }
            }
            else
            {
                grappleUIObject.GetComponent<Image>().color = Color.white;
            }

            #region visuals
            if(!swinging)
            {
                Vector2 playerPosition = transform.position;
                Vector2 cursorPos = Input.mousePosition;
                Vector2 cursorPosWorld = Camera.main.ScreenToWorldPoint(cursorPos);
                if((cursorPosWorld-playerPosition).magnitude > grappleRange)
                {
                    cursorPos = (Vector2)Camera.main.WorldToScreenPoint(playerPosition+(cursorPosWorld-playerPosition).normalized * grappleRange);
                }
                grappleUIObject.transform.position = cursorPos;
            }
            else
            {
                grappleUIObject.transform.position = Camera.main.WorldToScreenPoint(grapplePoint);
            }
            #endregion
            
            if(swinging)
            {
                if(GetComponent<PlayerMovement>().grounded && !startedOnReel) 
                {
                    endSwing();
                }
                if(Input.GetMouseButton(1))
                {
                    swingLength -= reelSpeed * (startedOnReel ? 2 : 1) * Time.deltaTime;
                    if(swingLength < 1.5f && !startedOnReel)
                    {
                        swingLength = 1.5f;
                    }
                    else if(swingLength < .25f && startedOnReel)
                    {
                        endSwing();
                        rb.velocity = (grapplePoint-(Vector2)transform.position).normalized * reelSpeed * 1.5f;
                        return;
                    }
                    if(startedOnReel) rb.velocity = Vector2.zero;
                }

                Vector2 currVelocity = rb.velocity;
                Vector2 grappleDir = (grapplePoint-(Vector2)transform.position).normalized;
                Vector2 swingDir1 = new Vector2(grappleDir.y, -grappleDir.x);
                Vector2 swingDir2 = new Vector2(-grappleDir.y, grappleDir.x);
                float velocityMagnitude = currVelocity.magnitude;
                Vector2 swingVector;
                if(Vector2.Dot(currVelocity, swingDir1) < 0)
                {
                    swingVector = swingDir2 * velocityMagnitude;
                }
                else
                {
                    swingVector = swingDir1 * velocityMagnitude;
                }
                rb.velocity = swingVector;
                
                if((grapplePoint - (Vector2)transform.position).magnitude - swingLength != 0)
                {
                    transform.position = grapplePoint-(grapplePoint - (Vector2)transform.position).normalized * swingLength;
                }

                grappleLine.transform.position = (grapplePoint + (Vector2)grappleOriginPoint.transform.position) / 2;
                grappleLine.transform.localScale = new Vector3(0.25f, (grapplePoint - (Vector2)grappleOriginPoint.transform.position).magnitude, 1f);
                Quaternion grappleRotation = Quaternion.Euler(0f, 0f, -Mathf.Atan(grappleDir.x/grappleDir.y) * 180 / Mathf.PI);
                Quaternion grappleOriginRotation = Quaternion.Euler(0f, 0f, -Mathf.Atan(grappleDir.x/grappleDir.y) * 180 / Mathf.PI + 180);
                grappleOriginPoint.transform.rotation = grappleOriginRotation;
                grappleLine.transform.rotation = grappleRotation;
            }
        }
        if(superJumpType != mutationType.NULL)
        {
            
        }
        if(slideType != mutationType.NULL)
        {
            
        }
    }

    private void startSwing(Vector2 anchor)
    {
        grappleUIObject.SetActive(false);
        swinging = true;
        grapplePoint = anchor;
        swingLength = (grapplePoint - (Vector2)transform.position).magnitude;
        grappleLine.SetActive(true);
        GetComponent<PlayerMovement>().freeBody = true;
    }
    private void endSwing()
    {
        grappleUIObject.SetActive(true);
        startedOnReel = false;
        swinging = false;
        grappleLine.SetActive(false);
        GetComponent<PlayerMovement>().freeBody = false;
    }

    public bool GiveMutation(mutationAbility ability, mutationType type)
    {
        if(hasAbility(ability) != mutationType.NULL)
        {
            return false;
        }
        else
        {
            mutations.Add(new Mutation(type, ability));
            return true;
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

    private void CheatAllAbilities(mutationType type, mutationType type2 = mutationType.NULL, mutationType type3 = mutationType.NULL)
    {
        if(type2 == mutationType.NULL) type2 = type;
        if(type3 == mutationType.NULL) type3 = type;
        if(hasAbility(mutationAbility.GRAPPLE) == mutationType.NULL)
        {
            GiveMutation(mutationAbility.GRAPPLE, type);
        }
        if(hasAbility(mutationAbility.SLIDE) == mutationType.NULL)
        {
            GiveMutation(mutationAbility.SLIDE, type);
        }
        if(hasAbility(mutationAbility.SUPER_JUMP) == mutationType.NULL)
        {
            GiveMutation(mutationAbility.SUPER_JUMP, type);
        }
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
