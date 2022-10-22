public interface ILateInit /*ILateInit offers alternative functions to Awake and Start. These functions will be called after the scene has been build by the SceneBuilder. This interface requires that the GameObject it belongs to has a LateInitObject component or that the GameObject is a descendant of a GameObject that does.*/
{
    public void LateAwake(); /*LateAwake will be called immediatly after the scene has been built.*/
    public void LateStart(); /*LateStart will be called immediatly after all LateAwake functions have been executed.*/
}
