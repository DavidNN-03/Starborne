namespace Starborne.Saving
{
    public class SceneData /*Class that contains all the data of a level. This data will be used by the SceneBuilder to instantiate GameObjects in the game scene.*/
    {
        public string sceneName; /*Name of the level.*/
        public string skyboxPath; /*Path of the skybox that should be used.*/
        public AssignmentsContainer assignments; /*Level's optional assignments.*/
        public ObjectsContainer[] objectContainers; /*Array of ObjectsContainers that store the data of the GameObjects that should be instantiated.*/
    }
}