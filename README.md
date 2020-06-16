# UnityStealthEngine
Basic concepts of stealth with multiple gadgets and showcases and an FP-Multiplayer-Catch-Game as demo  

Inspiration for this idea was: https://www.youtube.com/watch?v=Ay-5g36oFfc  
Inspiration on gadgets: https://www.youtube.com/watch?v=QLWC081dDpc

## Szenario
- Indoor von A nach B kommen ohne entdeckt zu werden
- Wenn entdeckt --> Szenario vorbei

## Komponenten
- Gegner `(branch feature/enemy)`
    - [ ] Patrullierende Gegner die sehen und hören
         
            "Sehen" wird über Vision-Cone dargestellt
            Hören über Vectorberechnung + Collider & Navmash (https://www.youtube.com/watch?v=mBGUY7EUxXQ)
            Patrullie per Navmash + feste Punkte, Statemachine für "Chase", "Search" und "Patroul"
            "Sichtbarkeit" über zentralen GameManager
            
    - [X] Gegnerische Kameras die sehen `(branch feature/camera)`
            
            "Sehen" wird über Vision-Cone dargestellt
            "Sichtbarkeit" über zentralen GameManager
            
- Basic Stealth Komponenten
    - [ ] Schleichen `(branch feature/sneak)`
        - Wenn Player in Hörweite von Gegnern muss er schleichen   
                
                Via Statemachine --> wenn {Taste} gedrückt reduziere Geschwindigkeit
                Geschwindigkeit wird an zentralen GameManager geliefert
                Gegner fragen Geschwindigkeit ab
                                    
    - [X] Verstecken `(branch feature/hide)`
        -  Bereiche definieren, in denen Player nicht gesehen wird (Schatten)
                
                Collider + Visualisierung (schwarze Plane)
                Gegner fragt bei Sicht/Hören den Satus des Player ab (hidden vs. nicht hidden)    
    
    - [X] Gegner ablenken `(branch feature/distract)`
        - Stein werfen (Optional: Flugbahn visualisieren, Gegner in "Range" visualisieren)        
        - Gegner hören den Stein aufprallen
        - Gegner laufen zu Stein
        - Nach x Zeit gehen Gegner zu Ausgangssituation zurück
        
                GameObject das man wirft (Aktuell "Projektil")
                Wenn Triggered liefert Position --> "Hören via NavMesh + Vektor nutzen
           
    - [X] "Sichtbarkeits" Visualisierung `(branch feature/ui)`
        - Anzeige im UI die zeigt wie sichbar man ist   
                
                UI Anzeicge "Sichtbar X von 100" & "Hörbar Y von 100"
                Werte aus zentralem GameManager
                Optional: "Healthbar"-like
          
    - Gadgets `(branch develop)`
        - [X] Ping/Ping-Block
        - [X] Markieren