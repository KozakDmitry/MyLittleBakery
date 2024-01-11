
using UnityEngine;

namespace Assets.CodeBase.Infostructure
{
    public class GameRunner : MonoBehaviour
    {
        public Bootstrapper bootstrapperPrefab;

        private void Awake()
        {
            Bootstrapper bootstrapper = FindObjectOfType<Bootstrapper>();
            if(bootstrapper == null)
            {
                Instantiate(bootstrapperPrefab);
            }
        }
    }
}
