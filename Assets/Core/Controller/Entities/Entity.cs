using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.Controller.Entities
{
    [RequireComponent(typeof(Image), typeof(Sprite))]
    public abstract class Entity : MonoBehaviour
    {
        public Image image;
        public int speed;
        public Sprite sprite;

        [Header("Monitoring"), SerializeField] protected int currentSpeed;
        [SerializeField] protected MoveState moveState = MoveState.Idle;

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        private void Awake()
        {
            image.sprite = sprite;
            image.color = Color.white;

            OnAwake();
        }

        private bool IsMoving() =>
            moveState != MoveState.Idle;

        private void Update() => OnUpdate();
    }
}
