using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
    public UnityEvent OnReceivePiece;
    
    public TaskCard Task => _task;
    public bool IsClosed { get; private set; }
    public DragablePiece Piece { get; private set; }
    public bool Empty() => Piece == null;
    public bool IsHomeSlot => _homePiece != null;

    [SerializeField] private Transform _slotCenter;
    [SerializeField] private TaskCard _task;
    private DragablePiece _homePiece;

    private void Awake()
    {
        _slotCenter ??= transform;
    }

    public void AssignAsHome(DragablePiece piece)
    {
        if (_homePiece != null) return;
        _homePiece = piece;
    }

    public void Clear()
    {
        Piece = null;
    }

    public bool CanPutPiece(DragablePiece piece)
    {
        if (_homePiece != null && piece != _homePiece)
            return false;
        
        /*if (_task != null &&
            _task.Suite != MinisterSuite.None &&    //Suite requirement is absolute
            _task.Suite != piece.Minister.Suite)
            return false;*/
        
        return Empty() && !IsClosed;
    }

    public void PutPieceIn(DragablePiece piece, bool instant = true)
    {
        Piece = piece;
        PutInOriginalPosition(instant);
        
        OnReceivePiece.Invoke();
    }

    public void PutInOriginalPosition(bool instant = true)
    {
        Piece.transform.SetParent(this.transform);

        if (instant)
            Piece.transform.position = _slotCenter.position;
        else
            Piece.transform.DOMove(_slotCenter.position, 0.1f);
    }

    public void Close()
    {
        IsClosed = true;
    }
}
