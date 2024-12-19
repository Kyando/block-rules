using UnityEngine;

public class CellGridView : MonoBehaviour
{
    public CellGridModel cellModel;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(CellGridModel cellModel)
    {
        this.cellModel = cellModel;
        this.transform.position = new Vector3(cellModel.gridPosition.x, cellModel.gridPosition.y, 0);
        this.spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        UpdateView();
    }

    // Update is called once per frame
    void UpdateView()
    {
        this.gameObject.SetActive(cellModel.isEnabled);
        if (cellModel.isEmpty)
        {
            spriteRenderer.color = Color.gray;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }
    }
}