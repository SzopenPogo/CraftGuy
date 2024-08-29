using UnityEngine;

public class UiWelcomeController : UiToolkitWindow
{
    protected override void ApplyOnEnable()
    {
        base.ApplyOnEnable();

        UiHeaderCloseButton closeButton = new(this);
    }
}
