namespace CodeBase.Infrastructure.Command
{
    public interface IUndoCommand : ICommand
    {
        public void Undo();
    }
}