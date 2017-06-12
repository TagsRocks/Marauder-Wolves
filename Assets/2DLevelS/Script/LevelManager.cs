using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float speed = 20f;
	public StoryMode vStoryMode;
	public GameObject LineBTWChapter;
	public GameObject EmptyGameObject; //USED to link the same line to another chapter
	private GameObject LevelPanel;
	private Levels vCurrentSelected = null;

	public AudioClip vClickLevel;
	public AudioClip vClosedevel;
	public AudioClip vUnlockedLevel; 

	public Color vRoadNotDone = new Color(0.2f, 0.2f, 0.2f);
	public Color vRoadDone = Color.white;
	public Color vRoadBtwChapter = Color.green;
	
	private Transform vPanelStar;
	private Transform vPanelNumber;
	private Transform vPanelTime;
	private Transform vPanel;
	private Transform vAgreeButton;
	private Transform vDeclineButton;
	public AudioSource vAudioSource1;
	public AudioSource vAudioSource2;
	public AudioSource vAudioSource3;

	private List<Levels> DrawingLevels; 			//keep a list of the new level we are drawing a line to it
	private float vPercDrawing = 0f;				//hold the percentage of 
	public float vDrawingTime = 3f;					//how fast the new level will be drawed
	private bool ChangeScoreType = false;

	private Ray ray;
	private RaycastHit hit;


	//initialie the sprite to use
	private Sprite vLevelToDo;
	private Sprite vLevelDone;
	private Sprite vLevelNext;
	
	// Use this for initialization
	void Start () {

		//initialize buttons
		vLevelToDo = Resources.Load<Sprite> ("Map/Level/LevelTODO");
		vLevelDone = Resources.Load<Sprite> ("Map/Level/LevelDONE");
		vLevelNext = Resources.Load<Sprite> ("Map/Level/LevelNEXT");

		//get the level panel object
		LevelPanel = GameObject.Find ("LevelPanel");
		
		//get the Panel with a smaller path for coding purpose
		vPanel = LevelPanel.transform.FindChild("FrontPanel");
		
		//define the score type
		vPanelStar = vPanel.FindChild("LevelScore").FindChild("StarsType");
		vPanelNumber = vPanel.FindChild("LevelScore").FindChild("NumberType");
		vPanelTime = vPanel.FindChild("LevelScore").FindChild("TimeType");
		vAgreeButton = vPanel.FindChild ("AgreeButton");
		vDeclineButton = vPanel.FindChild ("DeclineButton");

		LevelPanel.SetActive (false); //disable it by default

		DrawChapter (0);
	}

	//play the selected audioclip
	void PlaySound(AudioClip vClip)
	{
		bool FoundAudioSource = false;
		if (vAudioSource1 != null) {
			if (!vAudioSource1.isPlaying) {
				FoundAudioSource = true;
				vAudioSource1.Stop ();
				vAudioSource1.clip = vClip;
				vAudioSource1.Play ();
			}
		}
			
		if (vAudioSource2 != null && !FoundAudioSource) {
			if (!vAudioSource2.isPlaying) {
				vAudioSource2.Stop ();
				vAudioSource2.clip = vClip;
				vAudioSource2.Play ();
			}
		}

		//if AudioSource1 is already playing, just play it on AudioSource3
		if (vAudioSource3 != null && !FoundAudioSource) {
			vAudioSource3.Stop ();
			vAudioSource3.clip = vClip;
			vAudioSource3.Play ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			//if mobile
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				
				// Get movement of the finger since last frame
				Vector2 touchDeltaPosition= Input.GetTouch(0).deltaPosition;
				
				// Move object across XY plane
				Camera.main.transform.Translate (-touchDeltaPosition.x * Time.deltaTime * (speed/2) , 
				                                 -touchDeltaPosition.y * Time.deltaTime * (speed /2), 0);
			}
			else if (Input.touchCount <= 0) //if PC
			{
				float vMoveX = Input.GetAxisRaw("Mouse X");
				float vMoveY = Input.GetAxisRaw("Mouse Y"); 
				vMoveX *= Time.deltaTime * speed;
				vMoveY *= Time.deltaTime * speed;
				Camera.main.transform.Translate (-vMoveX * speed, -vMoveY * speed, 0);
			}
		}

		//check if we completed a level to show the animation of the road being completed 
		if (DrawingLevels != null)
		if (DrawingLevels.Count() > 0)
		{
			//add time to the perc.
			vPercDrawing += Time.deltaTime;

			//calculate the perc to draw the line correctly
			float vPerc = (float)(vPercDrawing/vDrawingTime);

			foreach (Levels vCurLevels in DrawingLevels)
			{
				//redraw pos 1 correctly with the perc
				foreach (LevelsDest vCurDest in vCurLevels.vNextLevel) {
					vCurDest.vLineRenderer.SetPosition (0, vCurLevels.vObject.transform.position);
					vCurDest.vLineRenderer.SetPosition (1, Vector3.Lerp(vCurLevels.vObject.transform.position, vCurDest.vNextPosition, vPerc));
				}
			}

			if (vPerc >= 1f) {
				//change the levels sprite renderer for the new one!
				foreach (Levels vCurLevels in DrawingLevels)
					foreach (LevelsDest vCurDest in vCurLevels.vNextLevel) {

						//show Unlocked animation
						GameObject vNewObj = (GameObject)Instantiate(Resources.Load("NewLevelParticle"));
						vNewObj.transform.position = vCurDest.vNextPosition;

						//change sprite
						vCurDest.vNextLevel.vLevelSprite.sprite = vLevelNext;

						//has been shown
						vCurDest.vNextLevel.Shown = true;
					}

				//play the sound of a unlocked level!
				PlaySound (vUnlockedLevel);

				//stop moving bars
				DrawingLevels = null;
			}
		}

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0) && vCurrentSelected == null)  //can only get a new level if we have closed the level selector
		{
			Levels vLevelSelected = GetLevelByObject(hit.collider.gameObject);

			//can only click on the LevelNext Sprite
			if (vLevelSelected.vObject.GetComponent<SpriteRenderer>().sprite.name == "LevelNEXT")
			{
				//play the sound of the opening level
				PlaySound (vClickLevel);

				vCurrentSelected = vLevelSelected;

				//show the right level with the right score system
				LevelPanel.SetActive(true);

				//get the level title, name and description
				vPanel.FindChild("LevelNbr").FindChild("Text").GetComponent<Text>().text = vLevelSelected.Chapter.ToString() + " - " + vLevelSelected.order.ToString();
				vPanel.FindChild("LevelInfo").FindChild("Name").GetComponent<Text>().text = vLevelSelected.name;
				vPanel.FindChild("LevelInfo").FindChild("Descr").GetComponent<Text>().text = vLevelSelected.description;


				//check out the score that we want
				switch (vStoryMode.vScoreType)
				{
					//show the good number of stars
					case ScoreType.Stars :
						//hide other tab
						vPanelStar.gameObject.SetActive(true);
						vPanelNumber.gameObject.SetActive(false);
						vPanelTime.gameObject.SetActive(false);

						//hide agree and cancel button
						vAgreeButton.gameObject.SetActive(false);
						vDeclineButton.gameObject.SetActive(false);
					break;

					//show the good number of stars
					case ScoreType.Numbers :
						vPanelNumber.gameObject.SetActive(true);
						vAgreeButton.gameObject.SetActive(true);
						vDeclineButton.gameObject.SetActive(true);

						//hide other tab
						vPanelStar.gameObject.SetActive(false);
						vPanelTime.gameObject.SetActive(false);
						vPanelNumber.FindChild("InputField").FindChild("Text").GetComponent<Text>().text = "";
					break;


					//show the good number of stars
					case ScoreType.Times :
						vPanelTime.gameObject.SetActive(true);
						vAgreeButton.gameObject.SetActive(true);
						vDeclineButton.gameObject.SetActive(true);

						//hide other tab
						vPanelStar.gameObject.SetActive(false);
						vPanelNumber.gameObject.SetActive(false);
						vPanelTime.FindChild("MinField").FindChild("Text").GetComponent<Text>().text = "";
						vPanelTime.FindChild("SecField").FindChild("Text").GetComponent<Text>().text = "";
					break;
				}
			}
		}
	} 

	public void CloseLevel()
	{
		//close the level panel
		LevelPanel.SetActive (false);
		vCurrentSelected = null;

		//play the sound of the closing level
		PlaySound (vClosedevel);
	}

	public void CompleteLevel(string vScore)
	{
		switch (vStoryMode.vScoreType)
		{
			case ScoreType.Numbers :
				//get the score in the score text field
				vScore = vPanelNumber.FindChild("InputField").FindChild("Text").GetComponent<Text>().text;
			break;

			case ScoreType.Times :
				//get the score in 2x time field
				string vstr1 = vPanelTime.FindChild("MinField").FindChild("Text").GetComponent<Text>().text;
				string vstr2 = vPanelTime.FindChild("SecField").FindChild("Text").GetComponent<Text>().text;

				if (vstr1 == "")
					vstr1 = "00";

				if (vstr2 == "")
					vstr2 = "00";

				vScore = vstr1 + ":" + vstr2;
			break;
		}
		
		//close the level panel
		LevelPanel.SetActive (false);
		vCurrentSelected.Completed = true;
		vCurrentSelected.score = vScore;
		vCurrentSelected = null;

		//draw the chapter correctly
		DrawChapter(1);

		//play the sound of the closing level
		PlaySound (vClosedevel);
	}


	Levels GetLevelByObject(GameObject vLevelObject)
	{
		//initialize the variable
		Levels vLevelToReturn = null;

		foreach (Chapters CChapter in vStoryMode.vChapter)
		{
			foreach (Levels cLevels in CChapter.vLevels)
			{
				//get the right level
				if (cLevels.vObject == vLevelObject)
				{
					vLevelToReturn = cLevels;
					vLevelToReturn.Chapter = CChapter.order; //save the chapter in the level
				}
			}
		}

		//get the level to return
		return vLevelToReturn;
	}

	//modify the score type
	public void ModifyScoreType(string newscoretype)
	{
		if (newscoretype == "Stars")
			vStoryMode.vScoreType = ScoreType.Stars;
		else if (newscoretype == "Numbers")
			vStoryMode.vScoreType = ScoreType.Numbers;
		else 
			vStoryMode.vScoreType = ScoreType.Times;

		ChangeScoreType = true;

		//redraw everything
		DrawChapter (1);

		ChangeScoreType = false;
	}

	//get the next level position to make a line between them
	public NextPosLevels[] GetNextLevelPosition(Levels CuLevel, Chapters CuChapter)
	{
		NextPosLevels[] vNewNextPosition = new NextPosLevels[0];

		//check if we are at the last level of the chapter to make a connection between chapters and is completed
		if ((CuLevel.order == vStoryMode.vChapter[CuChapter.order-1].vLevels.Count()) && (CuLevel.Completed))
		{
			vNewNextPosition = new NextPosLevels[vStoryMode.vChapter[CuChapter.order-1].UnlockNextChapter.Count()];
			int i = 0;
			
			foreach(string vUnlockNextChapter in vStoryMode.vChapter[CuChapter.order-1].UnlockNextChapter)
			{
				//get the next position
				if (vUnlockNextChapter.Trim() != "")
				{
					NextPosLevels vNewPosL = new NextPosLevels();
					vNewPosL.vNextPosition = vStoryMode.vChapter[int.Parse(vUnlockNextChapter)-1].vLevels[0].vObject.transform.position;
					vNewPosL.vNextLevel = vStoryMode.vChapter [int.Parse (vUnlockNextChapter) - 1].vLevels [0];;
					vNewNextPosition [i] = vNewPosL;

					vStoryMode.vChapter[int.Parse(vUnlockNextChapter)-1].vLevels[0].CanShow = true; //make sure we can see the other chapter when unlocked
					i++;
				}
			}
		}
		else
		{
			//check all the level object and make sure the current level icon is showed
			foreach (Chapters CChapter in vStoryMode.vChapter)
			{
				//only get the current chapters
				if (CChapter.order == CuChapter.order)
					foreach (Levels cLevels in CChapter.vLevels)
					{
					    //if we get the next levels, then we get the current position
						if (cLevels.order == CuLevel.order+1)
						{
							vNewNextPosition = new NextPosLevels[1];
							NextPosLevels vNewPosL = new NextPosLevels();
							vNewPosL.vNextPosition = cLevels.vObject.transform.position;
							vNewPosL.vNextLevel = cLevels;
							vNewNextPosition [0] = vNewPosL;
						}
					}
			}
		}
		
		//return the next position
		return vNewNextPosition;
	}

	public void ShowScore(Levels vLevels)
	{
		GameObject vScoreTab = (GameObject)Instantiate(Resources.Load("GUI/ScoreTab"));
		vScoreTab.transform.parent = vLevels.vObject.transform;
		vScoreTab.transform.localPosition = new Vector3 (-0.72f, 1.1f, 0f); 

		//show the Score above the level by the score type
		switch (vStoryMode.vScoreType)
		{
			//show the good number of stars
			case ScoreType.Stars :

				//disable the other
				vScoreTab.transform.FindChild("NumberType").gameObject.SetActive(false);
				vScoreTab.transform.FindChild("TimeType").gameObject.SetActive(false);

				//get full and empty star
				Sprite vSpriteFullS = Resources.Load<Sprite> ("GUI/Star");
				Sprite vSpriteEmptyS = Resources.Load<Sprite> ("GUI/StarEmpty");

				//empty them all by default
				vScoreTab.transform.FindChild("StarType").FindChild("Star1").GetComponent<SpriteRenderer>().sprite = vSpriteEmptyS;
				vScoreTab.transform.FindChild("StarType").FindChild("Star2").GetComponent<SpriteRenderer>().sprite = vSpriteEmptyS;
				vScoreTab.transform.FindChild("StarType").FindChild("Star3").GetComponent<SpriteRenderer>().sprite = vSpriteEmptyS;
				
				//by default, we put 0 
				if (vLevels.score == "")
					vLevels.score = "0";

				//when chaing score, we put value 1 by default
				if (ChangeScoreType)
					vLevels.score = "1";

				//get the full star 
				if (int.Parse(vLevels.score) > 0) vScoreTab.transform.FindChild("StarType").FindChild("Star1").GetComponent<SpriteRenderer>().sprite = vSpriteFullS;
				if (int.Parse(vLevels.score) > 1) vScoreTab.transform.FindChild("StarType").FindChild("Star2").GetComponent<SpriteRenderer>().sprite = vSpriteFullS;
				if (int.Parse(vLevels.score) > 2) vScoreTab.transform.FindChild("StarType").FindChild("Star3").GetComponent<SpriteRenderer>().sprite = vSpriteFullS;
				
			break;

			//show number
			case ScoreType.Numbers :
				//disable the other
				vScoreTab.transform.FindChild("StarType").gameObject.SetActive(false);
				vScoreTab.transform.FindChild("TimeType").gameObject.SetActive(false);

				//when chaing score, we put value 1 by default
				if (ChangeScoreType)
					vLevels.score = "1";

				//show the right score
				vScoreTab.transform.FindChild("NumberType").FindChild("NumberBack").FindChild("ScoreNumber").GetComponent<TextMesh>().text = vLevels.score.ToString();
				vScoreTab.transform.FindChild("NumberType").FindChild("NumberBack").FindChild("ScoreNumber").GetComponent<MeshRenderer>().sortingOrder = 1500;
			break;

			//show time
			case ScoreType.Times :
				//disable the other
				vScoreTab.transform.FindChild("NumberType").gameObject.SetActive(false);
				vScoreTab.transform.FindChild("StarType").gameObject.SetActive(false);

				//when chaing score, we put value 1 by default
				if (ChangeScoreType)
					vLevels.score = "1:00";

				//show the right score
				vScoreTab.transform.FindChild("TimeType").FindChild("NumberBack").FindChild("ScoreNumber").GetComponent<TextMesh>().text = vLevels.score.ToString();
				vScoreTab.transform.FindChild("TimeType").FindChild("NumberBack").FindChild("ScoreNumber").GetComponent<MeshRenderer>().sortingOrder = 1500;
			break;
		}

	}

	public void DrawChapter(int vcpt)	//if vcpt = 0, it mean we just draw them all without next level animation. 1 = with animation
	{
		//initialise the list
		DrawingLevels = new List<Levels> ();
		vPercDrawing = 0f;

		//before drawing the chapter, we will remove all the previous objects. ONLY usefull when we redraw.
		foreach (Chapters CChapter in vStoryMode.vChapter)
		{
			foreach (Levels cLevels in CChapter.vLevels)
			{
				//destroy child
				foreach (Transform child in cLevels.vObject.transform) {
					if (cLevels.vObject.transform != child.transform)
						GameObject.Destroy(child.gameObject);

					foreach (LevelsDest vCurDest in cLevels.vNextLevel)
					{
						//destroy gameobject
						if (vCurDest.vCurObj != null)
							GameObject.Destroy (vCurDest.vCurObj);

						//destroy linerenderer
						if (vCurDest.vLineRenderer != null)
							GameObject.Destroy (vCurDest.vLineRenderer);
					}	

					//reinitialise it
					cLevels.vNextLevel = new List<LevelsDest> ();
				}
			}
		}
			
		Levels vLastLevel = null;

		//check all the level object and make sure the current level icon is showed
		foreach (Chapters CChapter in vStoryMode.vChapter)
		{
			//put a different icon for the first not completed levels by chapters
			bool firststage = true;
			bool ShowChapter = false;

			//get all the levels in this chapters
			foreach (Levels cLevels in CChapter.vLevels)
			{
				cLevels.vObject.SetActive(true);
				SpriteRenderer vSpriteRenderer = cLevels.vObject.GetComponent<SpriteRenderer>();

				//if there is a level we can show, we show the entire chapter
				if (cLevels.CanShow)
					ShowChapter = true;

				//show the current level done and todo
				if (cLevels.Completed)
				{
					//ONLY show the score if completed
					ShowScore(cLevels);
					vSpriteRenderer.sprite = vLevelDone;
					ShowChapter = true; //if at least 1 level is completed in the chapter, we show the rest. if not, we hide it
				}
				else
				{
					//ONLY show in RED the next level to do 
					if (firststage)
					{
						firststage = false;
						vSpriteRenderer.sprite = vLevelNext;

						//add the next levels in RED on the drawing list
						if (vcpt == 1 && !cLevels.Shown) {
							vSpriteRenderer.sprite = vLevelToDo;

							//get the next lvl if 
							foreach (Levels vCurLevels in GetLastLevel(cLevels))
								DrawingLevels.Add (vCurLevels);
						}
					}
					else
						vSpriteRenderer.sprite = vLevelToDo;
				}

				if (!ShowChapter && !cLevels.CanShow)
				{
					vSpriteRenderer.gameObject.SetActive(false);
				}

				//save the sprite renderer
				cLevels.vLevelSprite = vSpriteRenderer;

				//keep in memory the last levels
				vLastLevel = cLevels;
				
				//draw the line between the 2 points
				NextPosLevels[] vNextPosition = GetNextLevelPosition(cLevels, CChapter);

				if (vNextPosition.Count() > 0)
				{
					//int i = 0;
					foreach (NextPosLevels vUnlockNextChapterPos in vNextPosition)
					{
						if (vUnlockNextChapterPos.vNextPosition != Vector3.zero)
						{
							//get the current line renderer
							LineRenderer vCurrentLine = GetComponent<LineRenderer>();
							if (CChapter.vLevels.Count() == cLevels.order)
								vCurrentLine = LineBTWChapter.GetComponent<LineRenderer>();
							
							/*if (i > 0)
							{*/
								//create a empty game object
								GameObject vEmptyGameObject =  (GameObject)Instantiate(EmptyGameObject, cLevels.vObject.transform.position,Quaternion.identity);
								vEmptyGameObject.AddComponent<LineRenderer>();
								LineRenderer vLineRenderer = vEmptyGameObject.GetComponent<LineRenderer>();
								vLineRenderer.material = vCurrentLine.material;
								vLineRenderer.SetWidth(.45f, .45f);
								
								//if current level is completed
								if (CChapter.vLevels.Count() == cLevels.order)
									vLineRenderer.SetColors(vRoadBtwChapter, vRoadBtwChapter);
								else if (cLevels.Completed)
									vLineRenderer.SetColors(vRoadDone, vRoadDone);
								else
									vLineRenderer.SetColors(vRoadNotDone, vRoadNotDone);
								
								vLineRenderer.SetPosition(0, cLevels.vObject.transform.position);
								vLineRenderer.SetPosition(1, vUnlockNextChapterPos.vNextPosition);

								//associate the empty gameobject to it's parent so in the hierarchy view, we don't see useless emptygameobject there
								vEmptyGameObject.transform.parent = cLevels.vObject.transform;

								//creaet the new destination!
								LevelsDest vNewLevelDest = new LevelsDest();
								vNewLevelDest.vCurObj = vEmptyGameObject; 			//hold the next obj if it move or we can delete it later
								vNewLevelDest.vLineRenderer = vLineRenderer;
								vNewLevelDest.vNextPosition = vUnlockNextChapterPos.vNextPosition;//keep the next position for the road
								vNewLevelDest.vNextLevel = vUnlockNextChapterPos.vNextLevel;

								//now attach the next levels on this next level
								cLevels.vNextLevel.Add(vNewLevelDest);


							//}
							/*else
							{
								if (cLevels.vObject.GetComponent<LineRenderer>() == null)
								{
									cLevels.vObject.AddComponent<LineRenderer>();
									cLevels.vLineRenderer = cLevels.vObject.GetComponent<LineRenderer>();
								}
								cLevels.vLineRenderer.material = vCurrentLine.material;
								cLevels.vLineRenderer.SetWidth(.45f, .45f);


								//if current level is completed
								if (CChapter.vLevels.Count() == cLevels.order)
									cLevels.vLineRenderer.SetColors(vRoadBtwChapter, vRoadBtwChapter);
								else if (cLevels.Completed)
									cLevels.vLineRenderer.SetColors(vRoadDone, vRoadDone);
								else
									cLevels.vLineRenderer.SetColors(vRoadNotDone, vRoadNotDone);
								
								cLevels.vLineRenderer.SetPosition(0, cLevels.vObject.transform.position);
								cLevels.vLineRenderer.SetPosition(1, vUnlockNextChapterPos);
							}
							i++;*/
						}
					}
				}
			}
		}
	}

	//get the last level even if it's another chapter
	List<Levels> GetLastLevel(Levels vVLevels)
	{
		//make sure we found a level
		List<Levels> vLevelFound = new List<Levels>();
		foreach (Chapters vCurChapters in vStoryMode.vChapter)
			foreach (Levels vCurLevels in vCurChapters.vLevels)
				foreach (LevelsDest vCurDest in vCurLevels.vNextLevel)
					if (vCurDest.vNextLevel == vVLevels)
						vLevelFound.Add (vCurLevels);

		//return the found level
		return vLevelFound;				
	}

	public enum ScoreType{Stars, Numbers, Times};

	[System.Serializable]
	public class StoryMode
	{
		public Chapters[] vChapter;
		public ScoreType vScoreType = ScoreType.Stars; //here you can define how you will calculate the score on a level
	}

	[System.Serializable]
	public class Chapters
	{
		public int order = 1;
		public string name = "";
		public Levels[] vLevels = new Levels[0];
		public string[] UnlockNextChapter = new string[0];
	}

	[System.Serializable]
	public class NextPosLevels
	{
		public Vector3 vNextPosition = Vector3.zero;
		public Levels vNextLevel = null;
	}

	//class which hold the LineRenderer + Position of the next target without calculating it everytime
	[System.Serializable]
	public class LevelsDest
	{
		public LineRenderer vLineRenderer = null;
		public GameObject vCurObj = null;
		public Vector3 vNextPosition = Vector3.zero;
		public Levels vNextLevel = null;
	}
	
	[System.Serializable]
	public class Levels
	{
		public int order = 0;
		public string name = "";
		public string description = "";
		public string score = "0";
		public GameObject vObject = null;
		public List<LevelsDest> vNextLevel = null;
		public SpriteRenderer vLevelSprite = null;
		public bool Completed = false;
		public bool CanShow = false;
		public int Chapter = 0;
		public bool Shown = false;
	}
}
