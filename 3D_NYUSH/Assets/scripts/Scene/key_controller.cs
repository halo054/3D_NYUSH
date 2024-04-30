using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KeyInteraction : MonoBehaviour
{
    public float interactionRange = 5f; // 视线检测的范围
    public KeyCode interactionKey = KeyCode.E; // 交互键

    public Camera mainCamera;
    public TextMeshProUGUI interactionText;
    private bool isLookingAtKey = false;
    public bool isKeyPickedUp = false; // 添加一个布尔变量来表示钥匙是否被捡起
    public static KeyInteraction instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        interactionText.gameObject.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 先检查对象是否已经被销毁
        if (this != null)
        {
            // 如果当前场景是"start Scene"，则销毁自身
            if (scene.name == "start Scene")
            {
                Destroy(gameObject);
            }
            else if (scene.name != "transition scene" && scene.name != "Ending bad" && scene.name != "Ending good" && scene.name != "Ending leave")
            {
                // 其他场景的处理逻辑
                if (interactionText == null)
                {
                    GameObject textObject = GameObject.FindGameObjectWithTag("Key");
                    if (textObject != null)
                    {
                        interactionText = textObject.GetComponent<TextMeshProUGUI>();
                    }
                }

                if (mainCamera == null)
                {
                    mainCamera = Camera.main;
                    if (mainCamera == null)
                    {
                        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
                        if (cameraObject != null)
                        {
                            mainCamera = cameraObject.GetComponent<Camera>();
                        }
                    }
                }
            }
        }
    }


    void Update()
    {


        if (!isKeyPickedUp && mainCamera != null) // 如果钥匙未被捡起
        {
            // 发射一条射线从摄像机正前方，检测是否击中了钥匙
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // 玩家正在看着钥匙
                    isLookingAtKey = true;

                    // 检查是否按下了交互键
                    if (Input.GetKeyDown(interactionKey))
                    {

                        // 隐藏钥匙而不是销毁它
                        transform.position -= new Vector3(0f, 5f, 0f);
                        isKeyPickedUp = true;


                    }
                }
                else
                {
                    isLookingAtKey = false;
                }
            }
            else
            {
                isLookingAtKey = false;
            }
        }

        if (interactionText != null)
        {


            // 更新UI提示信息
            if (isLookingAtKey && !isKeyPickedUp)
            {
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    // 获取isKeyPickedUp的值的公共方法
    public bool IsKeyPickedUp()
    {
        return isKeyPickedUp;
    }
}
