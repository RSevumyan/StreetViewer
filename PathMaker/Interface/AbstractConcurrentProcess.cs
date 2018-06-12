using PathFinder.Core;

namespace PathFinder.Interface
{
    internal abstract class AbstractConcurrentProcess
    {
        protected Controller controller;
        protected MainForm mainForm;

        protected AbstractConcurrentProcess(MainForm mainForm, Controller controller)
        {
            this.mainForm = mainForm;
            this.controller = controller;
        }

        internal abstract void Process();

        protected abstract void UpdateStatus();
    }
}
