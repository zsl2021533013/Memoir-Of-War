using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace KaelmixStudioGameAssets.PolygonCharactersEffectsURP {
    public class DemoManager : MonoBehaviour {

        // STRUCT
        [System.Serializable]
        private struct Effect {
            public string name;
            public Material material;
            public bool hasEffectValue;
            public bool impactAnimation;
        }


        // PARAMETERS
        [Header("Parameters")]
        [SerializeField] private bool isChar = true;
        [SerializeField] private Animator chrAnimator;
        [SerializeField] private SkinnedMeshRenderer chrMesh;
        [SerializeField] private MeshRenderer propMesh;
        [SerializeField] private float effectSpeed = 1f;
        [SerializeField] float delayBetweenEffects = 1f;
        [SerializeField] private List<Effect> listEffects = new List<Effect>();

        [Header("UI")]
        [SerializeField] private Text effectName;
        [SerializeField] private Image effectFill;

        [Header("CaptureScreenshot")]
        [SerializeField] private bool doScreenshots = false;
        [SerializeField] private string screenshotPath = "Recordings/";
        [SerializeField] private string screenshotName = "img-";



        // VARIABLES
        [Header("Variables")]
        [SerializeField] private float effectValue = 0f;
        //private int iValue = 0;



        // FUNCTIONS
        void Start () {
            StartCoroutine("Process");
        }



        IEnumerator Process () {
            bool screenshotCreated = false;

            yield return new WaitForSeconds(delayBetweenEffects * 0.5f);

            for (int i = 0; i < listEffects.Count; i++) {
                if (isChar) { chrMesh.material = listEffects[i].material; }
                else { propMesh.material = listEffects[i].material; }
                effectName.text = listEffects[i].name;
                screenshotCreated = false;

                // Effect has an effect value (slider)
                if (listEffects[i].hasEffectValue) {
                    effectValue = 0f;
                    effectFill.fillAmount = effectValue;
                    chrAnimator.SetFloat("AnimationSpeed", 1f);

                    // Increase value
                    yield return new WaitForSeconds(0.1f);
                    while (effectValue < 1f) {
                        effectValue += Time.deltaTime * effectSpeed;
                        effectValue = effectValue > 1f ? 1f : effectValue;
                        effectFill.fillAmount = effectValue;
                        if (isChar) { chrMesh.material.SetFloat("_EffectValue", effectValue); }
                        else { propMesh.material.SetFloat("_EffectValue", effectValue); }
                        chrAnimator.SetFloat("AnimationSpeed", 1 - effectValue);

                        if (!screenshotCreated && effectValue >= 0.5f) {
                            screenshotCreated = true;
                            CreateScreenshot(i);
                        }
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(delayBetweenEffects);

                    // Decrease value
                    yield return new WaitForSeconds(0.1f);
                    while (effectValue > 0f) {
                        effectValue -= Time.deltaTime * effectSpeed;
                        effectValue = effectValue < 0f ? 0f : effectValue;
                        effectFill.fillAmount = effectValue;
                        if (isChar) { chrMesh.material.SetFloat("_EffectValue", effectValue); }
                        else { propMesh.material.SetFloat("_EffectValue", effectValue); }
                        chrAnimator.SetFloat("AnimationSpeed", 1 - effectValue);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(delayBetweenEffects);
                }
                // Effect has no effect value
                else {
                    yield return new WaitForSeconds(delayBetweenEffects * 1f);
                    CreateScreenshot(i);
                    yield return new WaitForSeconds(delayBetweenEffects * 2f);
                }

            }

            yield return null;
        }



        private void CreateScreenshot (int iValue) {
            if (!doScreenshots) return;
            
            iValue++;
            string iValueStr = iValue < 10 ? "0" + iValue.ToString() : iValue.ToString();
            ScreenCapture.CaptureScreenshot(screenshotPath + screenshotName + iValueStr + ".png");
        }
    }
}
