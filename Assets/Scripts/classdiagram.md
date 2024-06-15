```mermaid
classDiagram

GameManager --> ResultPageManager
GameManager --> SearchPageManager
SoundManager <-- ScoringManager
LightEstimate <-- ScoringManager
ResultPageManager --> ScoringManager
```