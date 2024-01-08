using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DragDrop : MonoBehaviour
{
    private bool selected;
    private Vector3 initPos;
    private Vector3 PrevPos;
    bool bSave = false;
    private void Start()
    {
        if(bSave == false)
            initPos = transform.position;

        StartCoroutine(ChekcZ());
        
    } 
    IEnumerator ChekcZ()
    {
        yield return new WaitForSeconds(3);
        if(this.transform.localPosition.z !=0)
        {
            Vector3 myVec = this.transform.localPosition;
            myVec.z = 0;
            this.transform.localPosition = myVec;
        }
    }
    private void Update()
    {
        if (selected == true)
        {
            if (bOutside == true)
            {
                this.transform.position = PrevPos;
                bOutside = false;
                selected = false;
            }
            else
            {
                Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(cursorPos.x, cursorPos.y, 0);
                Vector3 cursorPos_Local = new Vector3(0, 0, 0);
                cursorPos_Local = transform.localPosition;
                cursorPos_Local.z = 0;
                transform.localPosition = cursorPos_Local;
            }        
          
        }
    }
    private GameObject Clone = null;
    public void OnResetMiner()
    {
        if (Clone != null)
        {
            this.transform.parent.GetComponent<DropParent>().enabled = false;
            this.transform.parent.GetComponent<Button>().enabled = false;
            
            Destroy(Clone);            
            transform.position = initPos;
            this.GetComponent<Animator>().SetBool("isStop", true);
            this.GetComponent<MinerGoldSrc>().SetStartMine(false);
            this.GetComponent<MinerGoldSrc>().SetDeleteMat();
            this.transform.Find("shadow").gameObject.SetActive(false);
            SelectMap = false;
            GameManager.Instance.MinerPosMap[int.Parse(this.transform.parent.name) - 1] = false;
            isMap = false;
        }
    }    
    private void OnMouseDown()
    {
        //if (Input.GetMouseButtonDown(0))
        {
            int number = 0;
            if(int.TryParse(this.name,out number))
            {
                GameManager.Instance.ColliderChange(false, int.Parse(this.name));
                //this.GetComponent<Animator>().SetBool("isStop", true);
            }            
        }    
        if(isMap == true)
        {
            PrevPos = this.transform.position;            
        }
    }
    public void SetInitMap()
    {
        bSave = true;
        initPos = transform.position;
        isMap = true;
        if (isMap == true)
        {
            if (Clone == null)
            {
                this.transform.position = GameManager.Instance.MinerMapVector[int.Parse(this.transform.parent.name) - 1];                
                Clone = Instantiate(this.gameObject);
                Clone.transform.SetParent(this.transform.parent);
                Clone.transform.position = initPos;
                Clone.transform.localScale = transform.localScale;
                Clone.GetComponent<DragDrop>().enabled = false;
                Clone.GetComponent<BoxCollider2D>().enabled = false;
                Color myColor = new Color(1, 1, 1, 0.5f);
                Clone.GetComponent<SpriteRenderer>().color = myColor;
                Clone.GetComponent<Animator>().enabled = false;
                this.GetComponent<Animator>().SetBool("isStop", false);
                this.GetComponent<MinerGoldSrc>().SetStartMine(true);                
                this.transform.parent.GetComponent<DropParent>().enabled = true;
                this.transform.parent.GetComponent<Button>().enabled = true;
                this.transform.Find("shadow").gameObject.SetActive(true);
                SelectMap = true;
            }
        }
    }
    private bool SelectMap = false;
    private void OnMouseUp()
    {
        //if (Input.GetMouseButtonUp(0))
        {
            if(isMap ==true)
            {
                GameManager.Instance.MinerMapVector[int.Parse(this.transform.parent.name) - 1] = this.transform.position;
                GameManager.Instance.MinerMapVector[int.Parse(this.transform.parent.name) - 1].z = 0;
            }
            //this.GetComponent<Animator>().SetBool("isStop", false);
            int number = 0;
            if (int.TryParse(this.name, out number) ==false)
            {
                return;
            }          
            GameManager.Instance.ColliderChange(true, int.Parse(this.name));
            if (isMap == true)
            {   
                if(Clone == null)
                {
                    if (GameManager.Instance.TutorialIndex == 3)
                    {
                        TutorialManager.Instance.SetMoveMinerTutorial();
                    }
                    Clone = Instantiate(this.gameObject);
                    Clone.transform.SetParent(this.transform.parent);
                    Clone.transform.position = initPos;
                    Clone.transform.localScale = transform.localScale;
                    Clone.GetComponent<DragDrop>().enabled = false;
                    Clone.GetComponent<MinerGoldSrc>().enabled = false;
                    Clone.GetComponent<BoxCollider2D>().enabled = false;
                    Color myColor = new Color(1,1,1,0.5f);
                    Clone.GetComponent<SpriteRenderer>().color = myColor;
                    Clone.GetComponent<Animator>().enabled = false;
                    this.GetComponent<Animator>().SetBool("isStop", false);           
                    this.GetComponent<MinerGoldSrc>().SetStartMine(true);
                    this.transform.parent.GetComponent<DropParent>().enabled = true;
                    this.transform.parent.GetComponent<Button>().enabled = true;
                    GameManager.Instance.MinerPosMap[int.Parse(this.transform.parent.name) - 1] = true;
                    GameManager.Instance.MinerMapVector[int.Parse(this.transform.parent.name) - 1] = this.transform.position;
                    GameManager.Instance.MinerMapVector[int.Parse(this.transform.parent.name) - 1].z = 0;
                    this.transform.Find("shadow").gameObject.SetActive(true);
                    SelectMap = true;
                }
                this.GetComponent<MinerGoldSrc>().SetEffect();
            }
            if (isChangePos == true)
            {
                GameManager.Instance.ChangePos(this.transform.parent.name, changeName, this.name);
                Destroy(this.gameObject);
            }
            if (selected == true && isMerge == false && isMap == false && isChangePos==false)
            {
                transform.position = initPos;
                if (isMap == false)
                {
                    Destroy(Clone);
                    this.GetComponent<Animator>().SetBool("isStop", true);
                    this.GetComponent<MinerGoldSrc>().SetStartMine(false);           
                }                    
            }            
            selected = false;
            if (isMerge == true)
            {
                OnResetMiner();
                bool result = GameManager.Instance.SetMerge(strMergeParent);
                if(result == true)
                {
                    GameManager.Instance.MinerPos[int.Parse(this.transform.parent.name) - 1] = 0;
                    GameManager.Instance.SetCollider(int.Parse(this.transform.parent.name));
                    GameManager.Instance.SetInitMiner(strMergeParent);
                    this.transform.parent.GetComponent<DropParent>().DisableNumber();
                    GameManager.Instance.MinerPosMap[int.Parse(this.transform.parent.name) - 1] = false;
                    Destroy(this.gameObject);
                }
                else
                {
                    transform.position = initPos;
                }
             
            }

            if (isTrash == true)
            {
                GameManager.Instance.TrashObj(this.transform.parent.name, this.name);
                GameManager.Instance.SetCollider(int.Parse(this.transform.parent.name));
                this.transform.parent.GetComponent<DropParent>().DisableNumber();
                Destroy(this.gameObject);
                
                if (Clone != null)
                {
                    Destroy(Clone);
                }
            }         
        }
    }
    
    private bool isMerge = false;

    private bool isTrash = false;
    private bool isChangePos = false;
    private string changeName;
    private string strMergeParent;
    bool isMap = false;
    private bool bOutside = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {           
        if (collision.name == "Map")
        {
            if(isMap ==false)
            {
                isMerge = false;
                isChangePos = false;
                //StayMap = true;
                //isMap = true;
            }          
        
        }
        if(collision.name =="Map" || collision.name == "TopMenu")
        {
            if (isMap == true && SelectMap ==true)
            {
                bOutside = true;
            }
        }
       
    
        if (isMap == false)
        {
            if (collision.name == this.name && collision.tag != "Pos")
            {
                isMerge = true;
                //isMap = false;
                isChangePos = false;
                strMergeParent = collision.transform.parent.name;
            }
            if (collision.tag == "Pos")
            {
                changeName = collision.transform.name;
                //isMap = false;
                isMerge = false;
                isChangePos = true;
            }           

        }
        if (collision.tag == "trashcan")
        {
            isTrash = true;
            isMerge = false;
            isChangePos = false;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.name =="Map")
        {

            Quaternion targetRotation = Quaternion.LookRotation(collision.transform.position - transform.position, Vector3.up);


            if (targetRotation.x < 0) 
            {
                //Debug.Log("Down");
                isMap = false;
            }
            if (targetRotation.x > 0)
            {
                //Debug.Log("Up");
                isMap = true;
            }
            
            
        }
        if (collision.name != this.name)
        {
            isMerge = false;
        }            
        if (collision.tag == "Pos")
        {
            isChangePos = false;
            
        }
        if (collision.tag == "trashcan")
        {
            isTrash = false;
        }

    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
    }
}
