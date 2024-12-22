using System;

namespace Editor._Scripts._View
{
    public class LateBindingException : InvalidOperationException
    {
        public LateBindingException() 
            : base($"Guaranteed binding of a VisualElement to InspectorGUI is only possible before the first usage of {nameof(TerrainBuilderView.Root)}. " +
                   "This error occurs because the initialization phase has passed, and late binding is not supported in the current context. " +
                   "Ensure that all required elements are added to the root during the setup phase to avoid this issue.") 
        { }
    }
}