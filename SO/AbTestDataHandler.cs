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
        [SerializeField] private bool isVariantAlreadySet;
        [SerializeField] private GameEvent onSetUserVariant;

        public string Key => key;
        public string DefaultValue => defaultValue;

        public string Variant
        {
            get => value;
            set
            {
                this.value = value;
                isVariantAlreadySet = true;
                onSetUserVariant?.Invoke();
                Save(Write);
            }
        }


        private void OnEnable()
        {
            string encryptedName = TextEncryption.Encrypt(name, Password);
            FilePath = Path.Combine(Application.persistentDataPath, encryptedName);
            TempFilePath = Path.Combine(Application.persistentDataPath, $"temp{encryptedName}");
            Load(reader =>
            {
                value = reader.ReadString();
                isVariantAlreadySet = reader.ReadBoolean();
            });
        }

        private void Write(BinaryWriter writer)
        {
            writer.Write(value);
            writer.Write(isVariantAlreadySet);
        }
    }
}