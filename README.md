# Moth Flight Simulator – opis skryptów

Symulator lotu ćmy w Unity. Ćma wykrywa źródła światła w scenie i leci ku nim po zawężającej się spirali.

---

## CameraControl.cs

Obsługuje sterowanie kamerą przez gracza — pozwala swobodnie obserwować scenę.

---

## LightSource.cs

Komponent przypisywany do obiektów pełniących rolę źródła światła (żarówek). Przechowuje referencję do `Light` z Unity oraz flagę `isActive`. Udostępnia metody `GetIntensity()` i `SetIntensity()`, z których korzystają pozostałe skrypty.

---

## MothController.cs

Steruje ruchem ćmy. W każdej klatce skrypt:

1. Przeszukuje scenę w poszukiwaniu wszystkich aktywnych `LightSource`.
2. Wybiera ten o największym *wpływie*, obliczanym wzorem **I = jasność / dystans²** (analogia do prawa odwrotnych kwadratów).
3. Leci w jego kierunku po zawężającej się spirali Archimedesa — parametr `spiralAngle` kontroluje, jak mocno ćma "wkręca się" w ogień (85° to prawie czysta orbita, 0° to lot na wprost).

Każda ćma otrzymuje przy starcie losowy offset prędkości, dzięki czemu wiele instancji zachowuje się nieco inaczej.

---

## UIController.cs

Zarządza interfejsem użytkownika. Naciśnięcie **spacji** odblokowuje kursor myszy, umożliwiając interakcję ze suwakami na ekranie. Suwaki sterują natężeniem (`intensity`) poszczególnych żarówek w scenie w czasie rzeczywistym.
