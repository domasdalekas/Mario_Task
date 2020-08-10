# Changelog

- Completed the tilemap (using Tilepallet, effort: 10min)
- Added sounds (finding appropriate sounds to match exact effects, effort: 1h)
- Added Green mushroom (for more complete representation of the game, effort: 15min)
- Added Big Mario, Fire Mario, (extra animation for these types: Crouching) (referencing basic mario implementation, to mimic behavior of Big/Fire Mario, effort: 2h)
- Brick destroying fixed (finding the bug by exploring the gameplay, effort: 5min)
- Implemented flag going down and mario sliding down the pole at game end (Flag down was implemented using Timeline as requested, Mario sliding down  - with manipulating rigid body's position with Vector2d, effort: 4h)
- Invisible block implementation (used RayCast to track collision downwards of the invisible block to detect a player layer, effort: 3h)
- Mario runs to the castle at game end, flag raisal in the castle + fireworks added. (Using AddForce for Mario to run to target x position (Castle's entrance), flag is raised with increasing y pos of the game object with Vector2d, fireworks - rendering sprites when Mario enters the castle, effort: 4h)
- UI added with required labels (World label, score label, time label, coins collected label) (Using White Text labels to add to Canvas and using Rectangle Transforms to apply Anchor presets for scaling and positions for different screen sizes, effort: 2h)
- Minor bugs in the code (game crashing when the score is over 2000, syntax errors), (exploring the code and finding unexpected behavior, effort: 1h)
