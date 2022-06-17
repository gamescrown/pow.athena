using System.IO;
using pow.aidkit;
using UnityEngine;

namespace pow.athena
{
    [CreateAssetMenu(fileName = "AbTestDataHandler", menuName = "POW_SDK/Athena/AbTestDataHandler", order = 0)]
    public class AbTestDataHandler : StoredScriptableObject
    {
        [SerializeField] private string key;
        [SerializeField] private string variant;
        [SerializeField] private bool isVariantAlreadySet;
        [SerializeField] private GameEvent onSetUserVariant;

        public string Key => key;

        public string Variant
        {
            get => variant;
            set
            {
                variant = value;
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
                variant = reader.ReadString();
                isVariantAlreadySet = reader.ReadBoolean();
            });
        }

        private void Write(BinaryWriter writer)
        {
            writer.Write(variant);
            writer.Write(isVariantAlreadySet);
        }
    }
}