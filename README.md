# SliceItAllLightneer2019
2019.4.17f

<h3>Slice It All Game Replica</h3>

The game containts 1 test level in the LevelListSO. GameManager contains this level list as a reference from the SO. An example level can contain these objects:

<h6>SpeedBoost</h6>
This boost type adds a heavy z-axis force to the character. These boosts can be modified with a multiplier value.

![speed](https://i.ibb.co/st942PZ/speedboost.png)

<h6>TrampolineBoost</h6>
This object adds a heavy y-axis force to the character on first trigger contact.

![trampoline](https://i.ibb.co/dmgZ5Y9/trampolineboost.png)

<h5>Obstacles</h5>
There are 3 types of obstacles in the game which are contains ObstacleController(With different types of ObstacleSOs) or NonBreakableObstacleController. The objects are scaleable-friendly
and can be placed from the LevelEditor as well. An obstacle should contain a specific Score holder, the force Vector3 which will be applied to the pieces after the cut. Unbreakable
Obstacle can be seperated from these type of obstacle, because it is uncutable and makes the user fail.

![obstacles](https://i.ibb.co/9vvKgKG/obstacles.png)

<h5>Platforms</h5>
There are 3 types of platforms which can be used while creating a level from the level editor. Brown Platform is safe, Blue Platform is Water Ground which is a fail collider on contact
and EndLevelPlatform completes level on collision based on multiply hit. These Platforms have their own behaviors.

![platforms](https://i.ibb.co/7rKqDQx/platforms.png)

<h4>Design Overview</h4>
The game has been built on 3 different managers. GameManager controls LevelInitialize, LevelStart, LevelFail and LevelComplete. It calls MapManager and CharacterManager on LevelInitialize.
MapManager creates the map, CharacterManager creates the Character and gives a reference to use it from other scripts. Level behaviors have been controlled with event Actions.

The character containts SwordController for the movement physics, HitController for the collider seperations of the sword top, bottom and whole. The layering has been used for
physics seperations. InputController checks for Input Down, PolishController controls Score Polish on hit and Color change on Sword bottom hit. ScoreController controls the level score
with delegates.

UI is based on CanvasController behavior which contains Hide() and Show() functions. These functions are called based on the state of the game level from the game manager.

The scripts have Singleton script and IActiveSetter interface for the inheritance.

Level Editor: Can be found from SliceItAll/LevelEditor on top bar.
Create Level to start. After that, a level will be placed to the scene, adding platforms will match the grid based on scale and positions. Objects can be
selected from the inspector and drag to the scene, drag to the obstacles parent in the level. Adding end level platform and Saving prefab will create the level in the Prefab/Levels.
