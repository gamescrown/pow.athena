using System.IO;
using pow.aidkit;
using UnityEngine;

namespace pow.athena
{
    [CreateAssetMenu(fileName = "AbTestDataHandler", menuName = "POW_SDK/Athena/AbTestDataHandler", order = 0)]
    public class AbTestDataHandler : StoredScriptableObject
    {
        [SerializeField] private string key;
        [SerializeField] private string value;
        [SerializeField] private string defaultValue;
        [SerializeField] private bool isValueAlreadySet;
        [SerializeField] private bool haveToSendDataToAnalytics;
        [SerializeField] private GameEvent onSetUserVariant;

        public string Key => key;
        public string DefaultValue => defaultValue;
        public bool IsValueAlreadySet => isValueAlreadySet;
        public bool HaveToSendDataToAnalytics => haveToSendDataToAnalytics;

        public string Value
        {
            get => value;
            set
            {
                if (isValueAlreadySet) return;
                this.value = value;
                isValueAlreadySet = true;
                haveToSendDataToAnalytics = true;
                Save();
            }
        }

        protected override void Write(BinaryWriter writer)
        {
            writer.Write(value);
            writer.Write(isValueAlreadySet);
        }

        protected override void Read(BinaryReader reader)
        {
            value = reader.ReadString();
            isValueAlreadySet = reader.ReadBoolean();
        }
    }
}