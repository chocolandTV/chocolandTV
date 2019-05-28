using UnityEngine;
using LightBuzz.Vitruvius;

public class CharacterController : MonoBehaviour
{
    SensorAdapter adapter = null;

    public SensorType sensorType = SensorType.Kinect2;
   
    public Model model1 = null;
    public Camera cameraObject;
    private Vector3 cameradefaultPos;
    // Start is called before the first frame update
    private void Start()
    {
        cameradefaultPos = cameraObject.transform.position;

    }
    void OnEnable()
    {
        if (GlobalSensorController.WasSetFromLoader)
        {
            sensorType = GlobalSensorController.StartWithSensor;
        }

        adapter = new SensorAdapter(sensorType)
        {
            OnChangedAvailabilityEventHandler = (sender, args) =>
            {
                Debug.Log(args.SensorType + " is connected: " + args.IsConnected);
            }
        };

        

        model1.Initialize();
       
    }
    void OnDisable()
    {
        if (adapter != null)
        {
            adapter.Close();
            adapter = null;
        }



        cameraObject.transform.position = cameradefaultPos;
        model1.Dispose();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

            return;
        }

        if (adapter == null) return;

        if (adapter.SensorType != sensorType)
        {
            adapter.SensorType = sensorType;
        }

        Frame frame = adapter.UpdateFrame();

        if (frame != null)
        {
            if (frame.ImageData != null)
            {
            }


            Body body = frame.GetClosestBody();

            if (body != null)
            {
               
                    model1.DoAvateering(body);
                  
               
            }
        }
    }

}
