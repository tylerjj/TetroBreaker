# TetroBreaker

Game Description:
  My take on Gamedev.TV's Block Breaker game prototype.
  Control paddle with mouse. Left click to launch ball from paddle. 
  Destroy all the breakable objects in the scene in order to move to the next level. 


TetroBreaker Playable Demo:
https://sharemygame.com/@Tyler_Johnston/tetro-breaker

TetroBreaker AutoPlay Demo:
https://www.youtube.com/watch?v=yVNDW59J7mE

GameDev.tv BlockBreaker Demo:
https://www.youtube.com/watch?v=duJqqQl-IRs

Known Bugs: 
  Ball Loops at edge of screen,
  Erratic Velocity when dynamic rigidbodies (balls, yellow squares) collide, 
  Failure to carry over a Multi-Ball that is unlaunched when a level is cleared to the next level. 

Known Design Flaw:
  Failed to make use of Object Pooling in the background Tetromino Spawner that is on the Start Menu, Game Over, and Game Won screens. 

Final Commentary: 
  Every feature (assets not included) that is not part of the base Block Breaker course, I coded from scratch. 
  Jank and all, these were my attempts at implementation. 
  I'm sure if I came back to this project with more experience, I could find ways to fix my bugs, but for now its time for me to move on.
