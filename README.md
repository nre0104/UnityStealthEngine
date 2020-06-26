# Unity Stealth Engine
26. Juni 2020

## Team
 - Isidoros Arvanitidis [197024] @Isibisi93
 - Nicolas Resch [197035] @nre0104

## Inspiration
Hauptsächlich haben wir uns von der Videoreihe "School of Stealth" von Game Maker's Toolkit inspirieren lassen.
 - [1. Teil](https://www.youtube.com/watch?v=Ay-5g36oFfc)
 - [2. Teil](https://www.youtube.com/watch?v=QLWC081dDpc&t=12s)
 - [3. Teil](https://www.youtube.com/watch?v=uF6c8KJuuEk) (Wurde weniger genutzt)

Dabei haben wir uns das Ziel gesetzt, bei den Tools und Features unserer Projekts an [Metal Gear](https://en.wikipedia.org/wiki/Metal_Gear) zu orientieren, 
optisch aber in Richtung von [Volume](https://en.wikipedia.org/wiki/Volume_(video_game)) zu gehen [1 Teil, 7:34 Min](https://youtu.be/Ay-5g36oFfc?t=454).

## Projektidee
Als Basiskomponenten unserer Unity Stealth Engine haben wir einige Komponenten festgelegt. 
Diese sind:
 - Ping
 - Sehen
 - Hören

Außerdem haben wir festgestellt, dass ein grundlegendes Konzept von Stealth-Games die Visualisierung der eigenen Sichtbarkeit, des Gegnerischen Views, etc. ist.
Daher haben wir einen ViewVisualizer erstellt, mit dem es, sowohl im Editor als auch im Spiel, möglich ist, die Sicht der Gegner zu visualisieren.

Auf den Komponenten aufbauend war unser Ziel eine Scenario zu entwickeln, in dem wir einige Gadgets und Tools aus den Basiskomponenten gebaut haben.
Ziel dieses Szenarios ist es einen konkreten Punkt auf der Map zu erreichen, ohne dabei von Gegnern "erwischt" zu werden.
Weiter haben wir im UI der Spieler*innen Anzeigen erstellt, welche die eigene Sichbarkeit/Hörbarkeit veranschaulichen. 
Aus dieser Veranschaulichung ergiebt sich auch, dass das Szenario vorbei ist, wenn die Slider voll sind.

## Besonderheiten
 - Im EnemyController arbeiten wir mit States
 - ViewVisualization arbeitet mit UnityEvents
 - Um den View auch im Editor zu visualisieren, gibt es zwei Editor Skripte (Path ../Editor)
 - Die Skripte/Komponenten sind in namespaces und Verzeichnisse unterteilt
 - Die Steuerung innerhalb des Szenarios ist im Play-Mode oben links eingeblendet

## Assets/Tutorials/Code Dritter
 - ViewVisualizier wurde mit Hilfe folgender Ressourcen erstellt:
	[Teil 1](https://www.youtube.com/watch?v=rQG9aUWarwE)
	[Teil 2](https://www.youtube.com/watch?v=73Dc5JTCmKI)
 - Minimap wurde mit Hilfe folgender Ressourcen erstellt:
	[Teil 1](https://www.youtube.com/watch?v=kWhOMJMihC0)
	[Teil 2](https://www.youtube.com/watch?v=TOygeraCrEQ)
 - Der EnemyController wurde mit Hilfe folgender Ressourcen erstellt:
	[Unity Doc](https://docs.unity3d.com/Manual/nav-AgentPatrol.html)
	[Video](https://www.youtube.com/watch?v=db0KWYaWfeM)
 - Assets/Packages/Imports
	- [Standard Assets](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2017-3-32351)
	- [Invector 3rd Person Controller LITE](https://assetstore.unity.com/packages/tools/utilities/third-person-controller-basic-locomotion-free-82048)
	- ProGrids
	- ProBuilder
	- Icons von Icons8.de [icons8-hören-100](https://img.icons8.com/officel/16/000000/hearing.png) & [icons8-sichtbar-100](https://img.icons8.com/officel/16/000000/visible.png)

## Links
### Projektvideo
https://www.dropbox.com/t/QDLUX5XQvP3PrnRN [YouTube]

### Link zum Projekt
https://github.com/nre0104/UnityStealthEngine (master) [GitHub]

## Hinweis
	Die hier aufgestellten Assets, Sounds und anderen Inhalte sind Teilweise oder ganz von dritten erstellt worden. 
	Soweit von diesen nicht anders angegeben liegt das Urheberrecht bei diesen und wir erheben keinerlei Ansprüche darauf. 
	Das Repo dient lediglich zur Projektverwaltung und Projektabgabe für ein internes Hochschulprojekt.