```mermaid
classDiagram

GameManager --> ResultPageManager
GameManager --> ResultPageManager
SoundManager <-- ScoringManager
LightEstimate <-- ScoringManager
ResultPageManager --> ScoringManager
```