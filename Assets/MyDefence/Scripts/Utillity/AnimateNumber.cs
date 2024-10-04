using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MyDefence
{
    public class AnimateNumber : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI roundsText;
        #endregion

        void Start()
        {
            StartCoroutine(AnimationNumber(PlayerStats.Rounds));
        }

        IEnumerator AnimationNumber(int targetNumber)
        {
            int aniNumber = 0;
            roundsText.text = aniNumber.ToString();
            yield return new WaitForSeconds(0.1f);

            while (aniNumber < targetNumber)
            {
                aniNumber++;
                roundsText.text = aniNumber.ToString();
                
                yield return new WaitForSeconds(0.1f);
            }
            
        }

    }



        // public void SetValue(int value)
        // {
        //     roundsText.text = value.ToString();
        // }

}
