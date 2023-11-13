using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MessageHandler : MonoBehaviour
{
    public TMPro.TextMeshPro userNameTMP;
    public TMPro.TextMeshPro bossHealthTMP;
    public TMPro.TextMeshPro attackPowerTMP;
    public GameObject selectorParent;
    public Camera m_Camera;
    public GameObject fx;
    private Vector3 originalPos;
    public Vector3 selectorTargetPosition = Vector3.zero;
    public GameObject[] targets;
    public float Duration = 1;
    private float curretAttackPoints;
    private float currentHealthPoints;

    void Awake()
    {
        m_Camera = Camera.main;
        originalPos = m_Camera.transform.localPosition;

    }

    // Use this for initialization
    void Start()
    {
        userNameTMP.text = "--";
        attackPowerTMP.text = "--";
        bossHealthTMP.text = "--";
        selectorParent.transform.DOMove(new Vector3(0,0,0), 0.5f).SetDelay(1f).SetEase(Ease.InOutElastic);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateUserName(string name)
    {
        userNameTMP.text = name;  
    }

    public void UpdateState(string jsonData)
    {
        var state = JsonUtility.FromJson<GameState>(jsonData);
        userNameTMP.text = state.userName;
        var bh = 0f;
        DOTween.To(() => bh, x => bh = x, state.bossHealth, 0.5f).OnUpdate(() => bossHealthTMP.text = bh.ToString());
        var at = 0f;
        DOTween.To(() => at, x => at = x, state.attackPower, 0.5f).OnUpdate(() => attackPowerTMP.text = at.ToString());

        if (state.targetIdx < 0 || state.targetIdx > targets.Length)
        {

            Debug.LogError("incorrect target");
        }
        else {
            GameObject t = targets[state.targetIdx];
            selectorParent.transform.DOMove(t.transform.position, 0.5f).SetEase(Ease.InOutElastic).OnComplete(()=> {
                StartCoroutine(SpawnExplosions(6, t.transform.position));
            });
            
            //StartCoroutine(LerpPosition(selectorParent, t.transform.position, Duration));
        }



    }

    public void ExecuteExplosion(string jsonData) {
        var state = JsonUtility.FromJson<ExplosionState>(jsonData);
        var t = targets[state.targetIdx];
        var origin = t.transform.position;
        for (int i=0; i < state.numExplosions; i++) {
            var r = Random.insideUnitCircle;
            var f = Object.Instantiate(fx);
            f.transform.localPosition = new Vector3(r.x, r.y, 0);
            StartCoroutine("ShakeCamera");
        }
    }

    IEnumerator SpawnExplosions(int count, Vector3 p)
    {
        for (int i = 0; i < count; i++)
        {
            var r = Random.insideUnitCircle;
            r.Scale(new Vector2(1.2f, 1.3f));
            var f = Object.Instantiate(fx);
            f.transform.localPosition = new Vector3(p.x+r.x, p.y+r.y, -1);
            StartCoroutine("ShakeCamera");
            yield return new WaitForSeconds(.12f);
        }
    }

    public void UpdateBossHealth(string jsonData)
    {
        var state = JsonUtility.FromJson<GameState>(jsonData);
        userNameTMP.text = state.userName;
        attackPowerTMP.text = state.attackPower.ToString();
        bossHealthTMP.text = state.bossHealth.ToString();
        if (state.targetIdx < 0 || state.targetIdx > targets.Length)
        {

            Debug.LogError("incorrect target");
        }
        else
        {
            GameObject t = targets[state.targetIdx];
            selectorParent.transform.DOMove(t.transform.position, 0.5f).SetEase(Ease.InOutElastic);

            //StartCoroutine(LerpPosition(selectorParent, t.transform.position, Duration));
        }

    }

    IEnumerator LerpPosition(GameObject obj, Vector3 targetPosition, float duration)
    {
        
        float time = 0;
        Vector3 startPosition = obj.transform.position;
        while (time < duration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = targetPosition;
        
    }

    [SerializeField] private float cameraShakeDuration = 0.5f;
    [SerializeField] private float cameraShakeDecreaseFactor = 1f;
    [SerializeField] private float cameraShakeAmount = 1f;
    IEnumerator ShakeCamera()
    {
        var currentPosition = m_Camera.transform.localPosition;
        var duration = cameraShakeDuration;
        while (duration > 0)
        {
            m_Camera.transform.localPosition = currentPosition + Random.insideUnitSphere * cameraShakeAmount;
            duration -= Time.deltaTime * cameraShakeDecreaseFactor;
            yield return null;
        }
        m_Camera.transform.localPosition = originalPos;
    }
}
