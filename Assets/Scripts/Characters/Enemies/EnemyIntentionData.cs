using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyIntentionData", menuName = "ScriptableObjects/EnemyIntention", order = 0)]
public class EnemyIntentionData : ScriptableObject
{
    [SerializeField] private EnemyIntentionType enemyIntentionType;
    [SerializeField] private Sprite intentionSprite;

    public EnemyIntentionType EnemyIntentionType => enemyIntentionType;

    public Sprite IntentionSprite => intentionSprite;
}