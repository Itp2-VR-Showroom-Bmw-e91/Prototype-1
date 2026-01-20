Project VR-Showroom for a bmw e91
3d objekt of the car https://3dmdb.com/en/3d-model/2009-bmw-3-series-e91-touring/12032193/?q=e91
Prototyp Dokumentation:
# VR-Projekt-Dokumentation: BMW E92 318d

## Ziel

Ein realistisches VR-Modell eines **BMW E92 318d**, bei dem der Nutzer:

* sich frei um das Fahrzeug bewegen kann
* die **Fahrertür öffnen** kann
* **ins Auto einsteigen** und sich auf den Fahrersitz setzen kann

---

## 1. Projektübersicht

**Anwendungsfall:**

* VR-Erlebnis (z. B. Oculus / SteamVR)
* Realistische Fahrzeuginteraktion

**Kernfunktionen:**

* 3D-Fahrzeugmodell
* Interaktive Türen
* Sitzposition & Kamerawechsel
* Physik & Animation

---

## 2. Modellierungsprozess (3D)

### 2.1 Fahrzeugmodell

* Basis: BMW E92 318d
* Einzelne Meshes:

  * Karosserie
  * Fahrertür (separates Objekt)
  * Innenraum
  * Sitze
  * Lenkrad

**Wichtig:**

* Tür **muss ein eigenes Objekt** sein
* Pivot-Punkt der Tür auf das **Türscharnier** setzen

### 2.2 Maßstab

* Maßstab: **1:1 (Realgröße)**
* Einheit: Meter

---

## 3. Import in Game Engine (z. B. Unity / Unreal)

### 3.1 Import-Schritte

1. 3D-Modell importieren (FBX/GLTF)
2. Materialien & Texturen prüfen
3. Kollisionskörper (Colliders) hinzufügen

### 3.2 Kollisionslogik

* Karosserie: Box/Mesh Collider
* Tür: eigener Collider
* Innenraum: Trigger-Zonen

---

## 4. Tür-Interaktion

### 4.1 Tür öffnen

**Ablauf:**

1. Nutzer nähert sich der Tür
2. Türgriff wird fokussiert
3. Interaktion (Trigger / Button / Handtracking)
4. Tür rotiert um Scharnierachse

**Technik:**

* Rotation um Y- oder Z-Achse (je nach Modell)
* Maximalwinkel: ca. **65–75°**

### 4.2 Tür-Animation

* Entweder:

  * Keyframe-Animation
  * Physik-gesteuerte Rotation (Hinge Joint)

---

## 5. Einsteigen ins Fahrzeug

### 5.1 Einsteigepunkt

* Trigger-Zone neben Fahrersitz
* Aktiv nur, wenn:

  * Tür offen
  * Nutzer nah genug

### 5.2 Kamera-Übergang

**Ablauf:**

1. Nutzer aktiviert Einsteigen
2. Kamera blendet kurz (Fade)
3. Kamera bewegt sich auf Sitzposition
4. Kamera wird fixiert (Headset-Tracking aktiv)

### 5.3 Sitzposition

* Kamera-Position:

  * Augenhöhe ≈ 120–130 cm
  * Leicht nach vorne versetzt
* Blickrichtung: Lenkrad / Straße

---

## 6. Zustandssystem (States)

**Fahrzeug-Zustände:**

* Tür geschlossen
* Tür geöffnet
* Nutzer draußen
* Nutzer sitzt im Auto

**Logik:**

* Tür kann nicht geschlossen werden, wenn Nutzer im Weg steht
* Einsteigen nur bei offener Tür

---

## 7. VR-spezifische Aspekte

### 7.1 Motion Sickness vermeiden

* Sanfte Kamerabewegungen
* Keine plötzlichen Rotationen
* Optional: Teleport statt Bewegung

### 7.2 Interaktion

* Controller-Trigger oder Handtracking
* Visuelles Feedback (Highlight, Sound)

---

## 8. Sound & Feedback

* Tür öffnen/schließen (Original BMW-Sound optional)
* Sitzbewegung (leichtes Geräusch)
* Klick beim Einrasten der Sitzposition

---

## 9. Tests & Feinschliff

* Tür öffnet korrekt aus jeder Position
* Kamera clippt nicht durch Geometrie
* Innenraum wirkt realistisch
* Performance-Test (FPS stabil)

---

## 10. Erweiterungsmöglichkeiten

* Motor starten
* Lenkrad drehen
* Spiegel einstellen
* Beifahrertür & Rücksitze
* Cockpit-Interaktion (Blinker, Display)

---

**Ergebnis:**
Ein immersives VR-Erlebnis, bei dem der Nutzer realistisch einen BMW E92 318d öffnet, einsteigt und im Fahrersitz Platz nimmt.
