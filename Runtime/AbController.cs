using System.Collections;
using Firebase.Analytics;
using pow.addy;
using pow.hermes;
using UnityEngine;

namespace pow.athena
{
    public class AbController : MonoBehaviour
    {
        [SerializeField] private AbTestDataHandler abTestDataHandler;

        // Called from game event listener, listen OnSetUserVariant game event
        public void OnSetUserVariant()
        {
            StartCoroutine(WaitFirebaseSDKInitializedForSendUserVariant());
            StartCoroutine(WaitAppplovinSDKInitializedForSendUserSegment());
        }

        // This controller must be wait firebase initialized
        private IEnumerator WaitFirebaseSDKInitializedForSendUserVariant()
        {
            yield return new WaitUntil(() => FirebaseInit.Instance.isFirebaseInitialized);
            EventSender.SetUserProperty(abTestDataHandler.Key, abTestDataHandler.Value);
            EventSender.LogFirebaseEvent(
                abTestDataHandler.Key,
                FirebaseAnalytics.ParameterValue,
                abTestDataHandler.Value
            );
        }

        private IEnumerator WaitAppplovinSDKInitializedForSendUserSegment()
        {
            yield return new WaitUntil(() => AppLovinMaxManager.Instance.IsInitialized());
            AppLovinMaxManager.Instance.SetUserSegment(abTestDataHandler.Value);
        }
    }
}