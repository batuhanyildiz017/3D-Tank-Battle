using UnityEngine;

namespace Scriptableobject
{
    [CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/BulletData")]
    public class BulletSO : ScriptableObject
    {
	    public float Damege;
	    public float deathTime;

    }

}
