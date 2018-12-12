using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public HotDogCountdown hotDogCountdown;
    public MoveList moveList;
    public KeyDisplay keyDisplay;
    private Move[] moves;
    private List<Buttons> buffer;
    public Animator thumbsAnimator;
    public GameObject nick;
    public GameObject chris;
    public GameObject jake;
    public GameObject honeypot;
    public GameObject babyBearLeft;
    public GameObject babyBearRight;
    public GameObject singingBear;
    public GameObject bearTrap;
    public float bufferTimeout;
    public float lastInputTime;
    public float mergeInputTime;
    private byte lastKeyDownState;
    private byte currentKeyDownState;
    private float cooldown;
    private bool coolingDown;
    //private byte lastKeyUpState;
    //private byte currentKeyUpState;
    
	// Use this for initialization
	void Start ()
    {
        //honeypot.SetActive(false);

        buffer = new List<Buttons>(5);

        moves = new Move[]
        {
            new Move(1, "Give honey", 3f, 3, Buttons.ACTION1, Buttons.ACTION2, Buttons.ACTION1, Buttons.ACTION2),
            new Move(2, "\"You stink\"", 2f, 2, Buttons.UP, Buttons.DOWN, Buttons.UP),
            //new Move(3, "You suck", 1f, 2, Buttons.LEFT, Buttons.RIGHT),
            new Move(4, "\"Dumb baby\"", 2f, 2, Buttons.ACTION3, Buttons.ACTION1, Buttons.ACTION2),
            //new Move(5, "Flex", 2f, 4, Buttons.UP, Buttons.ACTION1, Buttons.DOWN, Buttons.ACTION1),
            new Move(6, "Singing Bear", 3f, 3, Buttons.ACTION2, Buttons.ACTION3, Buttons.ACTION2, Buttons.ACTION3),
            new Move(7, "Bear Trap", 3f, 3, Buttons.UP, Buttons.LEFT, Buttons.DOWN, Buttons.RIGHT)
        };

        cooldown = .1f;
        coolingDown = true;
        keyDisplay.ClearKeys();
        moveList = GetRandomMoveList(moves);
        //keyDisplay.UpdateKeys(moveList);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hotDogCountdown.gameGoing)
        {
            if (cooldown > 0)
                cooldown -= Time.deltaTime;

            if (cooldown <= 0 && coolingDown)
                CooldownComplete();

            lastKeyDownState = currentKeyDownState;
            //lastKeyUpState = currentKeyUpState;
            GetCurrentKeyStates();

            float timeSinceLast = Time.time - lastInputTime;

            if (timeSinceLast > bufferTimeout)
                buffer.Clear();

            // Get the buttons that are down this frame that weren't down the last frame
            int buttonsThisFrame = currentKeyDownState & ~(currentKeyDownState & lastKeyDownState);

            //bool merge = (buffer.Count > 0 && timeSinceLast < mergeInputTime);

            if (buttonsThisFrame != 0)
            {
                if (buffer.Count == buffer.Capacity)
                    buffer.RemoveAt(0);
                buffer.Add((Buttons)buttonsThisFrame);

                lastInputTime = Time.time;
            }

            Move newMove = moveList.DetectMove(this);
            if (cooldown <= 0 && newMove != null)
            {
                // DO THAT MOVE DOG
                switch (newMove.moveId)
                {
                    case 1: //Give honey
                        {
                            if (!honeypot.activeInHierarchy)
                            {
                                honeypot.SetActive(true);
                                foreach (MonoBehaviour mb in honeypot.GetComponents<MonoBehaviour>())
                                {
                                    mb.enabled = true;
                                }
                                honeypot.transform.position = nick.transform.position;
                                thumbsAnimator.SetTrigger("ThrowObject");
                                MoveActivated(newMove);
                            }
                            break;
                        }
                    case 2: //You stink
                        {
                            thumbsAnimator.SetTrigger("StinkyBearTaunt");
                            MoveActivated(newMove);
                            break;
                        }
                    case 4: //You are a baby
                        {
                            babyBearLeft.SetActive(true);
                            babyBearRight.SetActive(true);
                            foreach (MonoBehaviour mb in babyBearLeft.GetComponents<MonoBehaviour>())
                            {
                                mb.enabled = true;
                            }
                            foreach (MonoBehaviour mb in babyBearRight.GetComponents<MonoBehaviour>())
                            {
                                mb.enabled = true;
                            }
                            thumbsAnimator.SetTrigger("BabyBearTaunt");
                            MoveActivated(newMove);
                            break;
                        }
                    case 6: //Singing bear
                        {
                            if (!singingBear.activeInHierarchy)
                            {
                                singingBear.SetActive(true);
                                foreach (MonoBehaviour mb in singingBear.GetComponents<MonoBehaviour>())
                                {
                                    mb.enabled = true;
                                }
                                singingBear.transform.position = nick.transform.position;
                                thumbsAnimator.SetTrigger("ThrowObject");
                                MoveActivated(newMove);
                            }
                            break;
                        }
                    case 7: // Bear trap
                        {
                            if (!bearTrap.activeInHierarchy)
                            {
                                bearTrap.SetActive(true);
                                foreach (MonoBehaviour mb in bearTrap.GetComponents<MonoBehaviour>())
                                {
                                    mb.enabled = true;
                                }
                                bearTrap.transform.position = nick.transform.position;
                                thumbsAnimator.SetTrigger("ThrowObject");
                                MoveActivated(newMove);
                            }
                            break;
                        }
                    default:
                        {
                            MoveActivated(newMove);
                            break;
                        }
                }
            }
        }
    }
    
    private void GetCurrentKeyStates()
    {
        currentKeyDownState = 0;
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
            currentKeyDownState += (int)Buttons.UP;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            currentKeyDownState += (int)Buttons.DOWN;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentKeyDownState += (int)Buttons.LEFT;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentKeyDownState += (int)Buttons.RIGHT;
        if (Input.GetButtonDown("Action1"))
            currentKeyDownState += (int)Buttons.ACTION1;
        if (Input.GetButtonDown("Action2"))
            currentKeyDownState += (int)Buttons.ACTION2;
        if (Input.GetButtonDown("Action3"))
            currentKeyDownState += (int)Buttons.ACTION3;

        //currentKeyUpState = 0;

        //if (Input.GetKeyUp(KeyCode.UpArrow))
        //    currentKeyUpState += (int)Buttons.UP;
        //if (Input.GetKeyUp(KeyCode.DownArrow))
        //    currentKeyUpState += (int)Buttons.DOWN;
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        //    currentKeyUpState += (int)Buttons.LEFT;
        //if (Input.GetKeyUp(KeyCode.RightArrow))
        //    currentKeyUpState += (int)Buttons.RIGHT;
        //if (Input.GetButtonUp("Action1"))
        //    currentKeyUpState += (int)Buttons.ACTION1;
        //if (Input.GetButtonUp("Action2"))
        //    currentKeyUpState += (int)Buttons.ACTION2;
        //if (Input.GetButtonUp("Action3"))
        //    currentKeyUpState += (int)Buttons.ACTION3;
    }

    private void MoveActivated(Move move)
    {
        buffer.Clear();
        hotDogCountdown.AddToBearTimer(move.timeIncrease);
        cooldown = move.cooldownTime;
        coolingDown = true;
        keyDisplay.ClearKeys();
    }

    private void CooldownComplete()
    {
        coolingDown = false;
        buffer.Clear();
        this.moveList = GetRandomMoveList(this.moves);
        keyDisplay.UpdateKeys(moveList);
    }

    // Use linq to give a random order to the list and take the top three.
    private MoveList GetRandomMoveList(Move[] moves)
    {
        System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
        
        Move[] newList = new Move[3];
        //newList = moves.Select(t => new {
        //    Index = Random.Range(0, moves.Length),
        //    Value = t
        //})
        //    .OrderBy(p => p.Index)
        //    .Select(p => p.Value).ToArray();

        newList = moves.OrderBy(x => rand.Next()).Take(3).ToArray();
        //System.Array.Copy(moves, newList, 3);

        return new MoveList(newList);
    }

    public bool Matches(Move move)
    {
        if (buffer.Count < move.sequence.Length)
            return false;

        //Move backwards through the sequence to test most recent input
        for (int i = 1; i <= move.sequence.Length; ++i)
        {
            if (buffer[buffer.Count - i] != move.sequence[move.sequence.Length - i])
            {
                return false;
            }
        }

        return true;
    }
}
