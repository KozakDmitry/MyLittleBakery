using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Infostructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}