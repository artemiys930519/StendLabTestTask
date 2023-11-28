using UnityEngine;

namespace CodeBase.Infrastructure.Command
{
    public class RightMoveCommand : ICommand
    {
        private Rigidbody _rigidbody;

        public RightMoveCommand(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Execute()
        {
            _rigidbody.velocity = Vector3.right + Vector3.up;
        }
    }
}