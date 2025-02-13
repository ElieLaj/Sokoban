using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color color;
    public SpriteRenderer _renderer;

    public bool isWalkable;
    public BaseEntity entity;
    public BaseEntity floorEntity;

    public bool walkable => isWalkable && (entity == null || entity.isSteppable);

    virtual public void init(bool isPrimary)
    {
    }

    public void SetEntity(BaseEntity newEntity)
    {
        Debug.Log($"Going in {gameObject.transform.position.x}, {gameObject.transform.position.y}");
        if (newEntity.occupiedTile != null) newEntity.occupiedTile.entity = null;
        if (entity != null && entity.isSteppable) { floorEntity = entity; }

        if ((entity is GoalEntity || floorEntity is GoalEntity) && newEntity is CrateEntity)
        {
            GameManager.Instance.ChangeState(GameState.WIN);
        }

        newEntity.transform.position = new Vector3 (transform.position.x, transform.position.y, newEntity is GoalEntity ? -1 : -2);
        entity = newEntity;
        newEntity.occupiedTile = this;
    }


}
