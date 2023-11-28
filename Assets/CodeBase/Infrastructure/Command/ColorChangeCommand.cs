using UnityEngine;

namespace CodeBase.Infrastructure.Command
{
    public class ColorChangeCommand : IUndoCommand
    {
        private Color _previousColor;
        private MeshRenderer _meshRenderer;

        public ColorChangeCommand(MeshRenderer meshRenderer)
        {
            _meshRenderer = meshRenderer;
            _previousColor = _meshRenderer.material.color;
        }

        public void Execute()
        {
            _meshRenderer.sharedMaterial.color = Color.yellow;
        }

        public void Undo()
        {
            _meshRenderer.sharedMaterial.color = _previousColor;
        }
    }
}