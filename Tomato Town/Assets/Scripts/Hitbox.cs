using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hitbox
{

    // ---Fields---
    public Vector2 pos1, pos2;
    private int flipConstant;
    private Vector2 shift;

    // ---Constructors---
    public Hitbox() {
        flipConstant = 1;
        pos1 = Vector2.zero;
        pos2 = Vector2.zero;
        shift = Vector2.zero;
    }

    public Hitbox(float xPos1,float yPos1,float xPos2,float yPos2) {
        flipConstant = 1;
        pos1.x = Mathf.Max(xPos1,xPos2);
        pos1.y = Mathf.Max(yPos1,yPos2);
        pos2.x = Mathf.Min(xPos1,xPos2);
        pos2.y = Mathf.Min(yPos1,yPos2);
    }

    public Hitbox(BoxCollider2D box,Vector2 scale) {
        var offset = box.offset;
        var size = box.size;
        pos1 = (offset + size * 0.5f) * scale;
        pos2 = (offset - size * 0.5f) * scale;
    }

    // ---Public Functions---
    public void UpdateBox(Vector2 shiftBox,int flip) {
        flipConstant = flip;
        shift = shiftBox;
    }

    public void ResetBox() {
        flipConstant = 1;
        shift = Vector2.zero;
    }

    public float GetWidth() { return Mathf.Abs(pos1.x - pos2.x); }
    public float GetHeight() { return Mathf.Abs(pos1.y - pos2.y); }

    // ---Access Functions---

    private float GetXMax() { return Mathf.Max(GetPointA().x,GetPointB().x); }
    private float GetXMin() { return Mathf.Min(GetPointA().x,GetPointB().x); }
    private float GetYMax() { return Mathf.Max(GetPointA().y,GetPointB().y); }
    private float GetYMin() { return Mathf.Min(GetPointA().y,GetPointB().y); }

    public Vector2 GetPointA() { return pos1 * new Vector2(flipConstant,1) + shift; }
    public Vector2 GetPointB() { return pos2 * new Vector2(flipConstant,1) + shift; }

    // ---Other Functions---
    public bool CheckHitX(Hitbox h) { return !(GetXMax() < h.GetXMin() || GetXMin() > h.GetXMax()); }
    public bool CheckHitY(Hitbox h) { return !(GetYMax() < h.GetYMin() || GetYMin() > h.GetYMax()); }
    public bool CheckHit(Hitbox h) {
        return CheckHitX(h) && CheckHitY(h);
    }

    public void DrawBox(Color c) {
        Debug.DrawLine(new Vector2(GetXMax(),GetYMax()),new Vector2(GetXMin(),GetYMax()),c);
        Debug.DrawLine(new Vector2(GetXMax(),GetYMin()),new Vector2(GetXMin(),GetYMin()),c);
        Debug.DrawLine(new Vector2(GetXMax(),GetYMax()),new Vector2(GetXMax(),GetYMin()),c);
        Debug.DrawLine(new Vector2(GetXMin(),GetYMax()),new Vector2(GetXMin(),GetYMin()),c);
    }

    #region Line Renderer Box

    public void RenderBox(LineRenderer lineRenderer) {
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount += 8;
        var i = lineRenderer.positionCount - 8;
        lineRenderer.SetPosition(i + 0,new Vector2(GetXMax(),GetYMax()));
        lineRenderer.SetPosition(i + 1,new Vector2(GetXMin(),GetYMax()));
        lineRenderer.SetPosition(i + 2,new Vector2(GetXMax(),GetYMin()));
        lineRenderer.SetPosition(i + 3,new Vector2(GetXMin(),GetYMin()));
        lineRenderer.SetPosition(i + 4,new Vector2(GetXMax(),GetYMax()));
        lineRenderer.SetPosition(i + 5,new Vector2(GetXMax(),GetYMin()));
        lineRenderer.SetPosition(i + 6,new Vector2(GetXMin(),GetYMax()));
        lineRenderer.SetPosition(i + 7,new Vector2(GetXMin(),GetYMin()));
    }
    #endregion


}