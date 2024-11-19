
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class CarEngine : MonoBehaviour
{
    public Transform[] allPath = null;
    private int numberOfPath;
    private Transform path;
    private Dictionary<int, Transform> pathList;
    public float turnSpeed = 30f;
    private float timer;
    private int timeWhenWin = 0;

    //set wheel angle
    private int correctNode = 0;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    //speed of car
    public float maxMotorTouqe = 100f;
    public float currentSpeed;
    public float maxSpeed = 150f;


    //braking
    public bool isBraking = false;
    public float maxBrakeTouqe = 150f;

    [Header("Sensors")]
    public float sensorLength = 1f;
    public Vector3 frontSensorVector = new Vector3(0, 0.2f, 0.5f);
    public float frontSenSor = 0.5f;
    public float sideSenSor = 0.5f;
    public float sensorAngle = 30;
    private bool avoiding = false;
    //private float targetSteerAngle = 0;

    //Item
    private bool item = false;

    //Check Player
    private GameObject realPlayer;
    private bool isReturn = true;
    public int[] requirePoint = null;

    //Check Round
    public int roundToWin;
    private int countCheckReturn = 0;
    private int countCheckpoint = 0;
    private int countAllCheckpointInPath;
    private bool isEnd = false;
    //int stopCount = 0;
    //public Lap lapOfRealPlayer;

    //Character
    public CharacterCreation characterDB;
    public Character character;
    public Sprite artworkSprite;
    public Image charImage;
    public int randomOption = 0;
    private WeatherManage weather;

    void Start()
    {

        //set Time
        timer = 0f;
        //set character
        weather = FindObjectOfType<WeatherManage>();
        setCharacter();
   
        //set Path
        numberOfPath = Random.Range(0, allPath.Length );
        Debug.Log(numberOfPath);
        realPlayer = GameObject.FindGameObjectWithTag("Player");
        path = allPath[numberOfPath];
        
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        countAllCheckpointInPath = pathTransforms.Length;
        pathList = new Dictionary<int, Transform>();
        for (int i = 0; i < pathTransforms.Length - 1; i += 1)
        {
            if (i == pathTransforms.Length - 1)
            {
                pathList.Add(i, pathTransforms[0]);
            }
            else
            {
                pathList.Add(i, pathTransforms[i + 1]);
            }

        }

    }
    private void Update() {
        timer += Time.deltaTime;
        


    }
    private void FixedUpdate()
    {
        Sensors();
        ApplySteer();
        Drive();
        CheckPlayer();
        CheckWaypointDistance();
        Braking();
        CheckLapCount();
        respawnBot();

        //LerpToSteerAngle();
    }

    public void setCharacter() {
        randomOption = Random.Range(0, characterDB.characterCount());
        updateCharacter(randomOption);
    }

    public void updateCharacter(int selectOption)
    {
        character = characterDB.getCharacter(selectOption);
        artworkSprite = character.imageCharacter;
        charImage.sprite = artworkSprite;
        setAbility(character);

        //Debug.Log("name" + weather.getWeatherName());

    }

    public void setAbility(Character character)
    {
        //Debug.Log(character.abilityCode);
        switch (character.abilityCode)
        {
            case EnumAbilityCode.UNSPECIFIED:
                
                break;

            case EnumAbilityCode.MAX_SPEED:
                maxSpeed += character.value; 
                break;

            case EnumAbilityCode.WEATHER_RAIN:
                maxSpeed += weather.checkAbilityWeatherForAI(character.abilityCode);
                break;

            case EnumAbilityCode.WEATHER_WIND:
                turnSpeed += weather.checkAbilityWeatherForAI(character.abilityCode);
                break;

            case EnumAbilityCode.WEATHER_SMOKE:
                maxSpeed -= weather.checkAbilityWeatherForAI(character.abilityCode);
                break;

            case EnumAbilityCode.WEATHER_SNOW:
                turnSpeed -= weather.checkAbilityWeatherForAI(character.abilityCode);
                break;

            case EnumAbilityCode.ADD_TIME:
                maxSpeed += 50;
                break;

            case EnumAbilityCode.SUB_TIME:
                maxSpeed += 50;
                break;

        }

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CheckReturn"))
        {
            isReturn = !isReturn;
            countCheckReturn += 1;
            //Debug.Log("return!");
            //Debug.Log("Return =" + countCheckReturn);
        }
        if (collider.CompareTag("Stop"))
        {
            //stopCount += 1;
            //Debug.Log("stopCount = "+ stopCount);
            if (isEnd == true) {
                timeWhenWin = (int)timer;
                //Debug.Log("Win at "+ timeWhenWin);
                Destroy(GameObject.FindGameObjectWithTag("Bot"), 10);
                //SceneManager.LoadScene("UniversalMap");
            }
        }
        if (collider.CompareTag("Item"))
        {
            Destroy(collider.gameObject);
        }
    }

    private void Sensors()
    {
        RaycastHit hit; // laser
        //Vector3 sensorsStartPos = transform.position + frontSensorVector;
        Vector3 sensorsStartPos = transform.position;
        sensorsStartPos += transform.forward * frontSensorVector.z;
        sensorsStartPos += transform.up * frontSensorVector.y;
        float avoidMultiplyer = 0;
        avoiding = false;

        //Right
        sensorsStartPos += transform.right * sideSenSor;
        if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
        {//show sensor
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                //เลี้ยวหลบ
                avoidMultiplyer -= 1f;
            }
        }
        //Right angle sensor
        else if (Physics.Raycast(sensorsStartPos, Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidMultiplyer -= 0.5f;
            }
        }


        //Left
        sensorsStartPos -= transform.right * sideSenSor * 2;
        if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
        { //show sensor
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidMultiplyer += 1f;
            }
        }
        //Left angle sensor
        else if (Physics.Raycast(sensorsStartPos, Quaternion.AngleAxis(-sensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidMultiplyer += 0.5f;
            }
        }

        //fornt center sensor
        if (avoidMultiplyer == 0) {
            //Center
            if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
            {   //show sensor
                if (hit.collider.CompareTag("Obstacle"))
                {
                    Debug.DrawLine(sensorsStartPos, hit.point);
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplyer = -1;
                    }
                    else {
                        avoidMultiplyer = 1;
                    }
                }
            }

        }


        if (avoiding)
        {
            //smooth turn
            //targetSteerAngle = maxSteerAngle * avoidMultiplyer;
            wheelFL.steerAngle = maxSteerAngle * avoidMultiplyer;
            wheelFR.steerAngle = maxSteerAngle * avoidMultiplyer;

        }

    }

    private void ApplySteer() {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(pathList[correctNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        //targetSteerAngle = newSteer;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;

    }

    private void Drive() {
        currentSpeed = 4 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (0 < currentSpeed && currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = maxMotorTouqe;
            wheelFR.motorTorque = maxMotorTouqe;

        }
        else if (item)
        {
            wheelFL.motorTorque = maxMotorTouqe * 2;
            wheelFR.motorTorque = maxMotorTouqe * 2;


        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }

    }

    private void CheckWaypointDistance() {

        if (pathList.ContainsKey(correctNode) && Vector3.Distance(transform.position, pathList[correctNode].position) < 10f) {
            if (correctNode == pathList.Count - 1)
            {
                correctNode = 0;
                countCheckpoint += 1;

            }

            else {
                correctNode++;
                countCheckpoint += 1;
            }
            //Debug.Log("corectNode current = " + correctNode);
            //Debug.Log("allCheckpoint =" + countCheckpoint);
        }
    }

    private void Braking() {
        if (isBraking)
        {
            wheelRL.brakeTorque = maxBrakeTouqe;
            wheelRR.brakeTorque = maxBrakeTouqe;
        }
        else {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;

        }
    }

    //private void LerpToSteerAngle() {
    //  wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime*turnSpeed);
    //  wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime*turnSpeed);

    //}

    private void CheckPlayer() {

        if (correctNode == requirePoint[numberOfPath] + 1 || correctNode == 0)
        {
            Vector3 positionPlayer = realPlayer.transform.position;
            Vector3 positionAI = transform.position;
            Vector3 trackDir;

            // ระยะห่างระหว่างจุด checkpoint ปัจจุบันกับอันถัดไป
            if (pathList.Count < correctNode + 1)
            {
                trackDir = pathList[correctNode].position - pathList[correctNode + 1].position;
            }
            else
            {
                trackDir = pathList[correctNode].position - pathList[0].position;
            }
            Vector3 distancePosition = positionAI - positionPlayer;

            // ระยะห่างระหว่างจุด checkpoint ปัจจุบันกับผู้เล่น หรือ AI
            float distance = Vector3.Dot(distancePosition, trackDir);

            //Debug.Log(positionPlayer);
            //Debug.Log(positionAI);

            bool isInsideRadiusOfSphere = gameObject.GetComponentInChildren<CarRadius>().isInsideRadiusSphere;

            if (!isInsideRadiusOfSphere) { //ไม่อยู่รอบ ai จะเข้าเงื่อนไขนี้
                // 3 case
                // ผู้เล่นตาม AI
                if (distance > 0)
                {
                    if ((correctNode != 0 || correctNode != requirePoint[numberOfPath] || correctNode + 1 != requirePoint[numberOfPath]))
                    {
                        Debug.Log("วัตถุ A อยู่ด้านหน้าวัตถุ B (AI นำ)");

                        //ของเก่า
                        //path = allPath[1];

                        //ของใหม่
                        for (int i = 0; i < allPath.Length; i++) {
                            if (allPath[i].name == "hardPath") { 
                                path = allPath[i];
                            }
                        }

                        Transform[] pathTransformsNew = path.GetComponentsInChildren<Transform>();
                        //Debug.Log(path.GetComponent<Transform>().name);
                        for (int i = correctNode; i < pathTransformsNew.Length; i += 1)
                        {
                            if (pathTransformsNew[i] != path.transform)
                            {
                                pathList[i] = pathTransformsNew[i];
                            }
                        }

                        //Debug.Log("corectNode = " + correctNode);
                        //Debug.Log("countCheckpoint = " + countCheckpoint);


                        //Debug.Log("corectNode = " + correctNode);
                    }
                }
                // AI ตามผู้เล่น
                else if (distance < 0)
                {
                    if ((correctNode != 0 || correctNode != requirePoint[numberOfPath] || correctNode + 1 != requirePoint[numberOfPath]))
                    {
                        Debug.Log("วัตถุ A อยู่ด้านหลังวัตถุ B (AI ตาม)"); // 
                        //correctNode += 1;
                        //countCheckpoint += 1;
                        //Debug.Log("countCheckpoint = " + countCheckpoint);
                        //path = allPath[2];

                        //ของใหม่
                        for (int i = 0; i < allPath.Length; i++)
                        {
                            if (allPath[i].name == "easyPath")
                            {
                                path = allPath[i];
                            }
                        }
                        Transform[] pathTransformsNew = path.GetComponentsInChildren<Transform>();
                        //Debug.Log(path.GetComponent<Transform>().name);
                        for (int i = correctNode; i < pathTransformsNew.Length; i += 1)
                        {
                            if (pathTransformsNew[i] != path.transform)
                            {
                                pathList[i] = pathTransformsNew[i];
                            }
                        }

                    }
                }
                
                //Debug.Log("allCheckpoint =" + countCheckpoint);
            }
            
        }


    }

    private void CheckLapCount() {
        int allCheckpoint = (roundToWin * countAllCheckpointInPath) - (roundToWin * 1);
        // AI ใช้การนับรอบเพื่อคำนวณจุด return
        int allReturn = (roundToWin * 2) + 1;

        //Debug.Log(allCheckpoint);
        for (int j = 3; j < allReturn; j += 2) {
            if (countCheckReturn == j) {
                path = allPath[0];
                Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
                pathList = new Dictionary<int, Transform>();
                for (int i = 0; i < pathTransforms.Length - 1; i += 1)
                {
                    if (i == pathTransforms.Length - 1)
                    {
                        pathList.Add(i, pathTransforms[0]);
                    }
                    else {
                        pathList.Add(i, pathTransforms[i + 1]);
                    }
                }
            }

        }
        if (countCheckpoint >= allCheckpoint && countCheckReturn >= allReturn) {
            isEnd = true;
            //Debug.Log("WIN");
        }
    }


    public int getTimeAIWhenWin()
    {
         return timeWhenWin;
        
    }

    public void respawnBot()
    {
        // ดึงมุมการหมุนในรูปแบบ Vector3 เพื่อตรวจสอบการเอียง
        Vector3 rotationAngles = transform.eulerAngles;

        // ตรวจสอบว่ารถคว่ำในแกน X หรือ Z (เช่น เอียงมากกว่า 80 องศา)
        if ((rotationAngles.x >= 80 && rotationAngles.x <= 280) ||
            (rotationAngles.z >= 80 && rotationAngles.z <= 280))
        {
            // รีเซ็ตการหมุนให้ Y ตามเดิม แต่ตั้ง X และ Z เป็น 0 เพื่อให้รถอยู่ในแนวตรง
            transform.rotation = Quaternion.Euler(0, rotationAngles.y, 0);
            Debug.Log("Flip Bot");
        }
    }

}
