using UnityEngine;

public class PlayerEntity : BaseEntity
{
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.WIN) { return; }
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W)) input = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) input = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) input = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) input = Vector2.right;

        if (input != Vector2.zero)
        {
            MoveOrPush(input);
        }
    }

    void MoveOrPush(Vector2 direction)
    {
        Vector2 targetPosition = (Vector2)occupiedTile.transform.position + direction;

        if (GridManager.Instance.tiles.TryGetValue(targetPosition, out Tile targetTile))
        {
            if (targetTile.walkable)
            {
                targetTile.SetEntity(this);
            }
            else if (targetTile.entity is CrateEntity)
            {
                Vector2 crateTargetPosition = targetPosition + direction;
                if (GridManager.Instance.tiles.TryGetValue(crateTargetPosition, out Tile crateTargetTile) && crateTargetTile.walkable || crateTargetTile.entity is GoalEntity)
                {
                    targetTile.entity.transform.position = crateTargetPosition;
                    crateTargetTile.SetEntity(targetTile.entity);

                    targetTile.SetEntity(this);
                }
            }
        }
    }

}
