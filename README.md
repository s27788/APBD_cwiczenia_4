# 🐾 KlinikaAPI 🐾 

REST API, która pozwala zarządzać danymi zwierząt i ich wizyt w klinice weterynaryjnej.

## RUN

1. Otwórz projekt w IDE i uruchom `Program.cs`
3. Przejdź do [http://localhost:5019/swagger](http://localhost:5019/swagger)

## Endpointy

### 🐶 🐶 🐶 Zwierzęta 🐶 🐶 🐶 

#### ✅ `GET /animals`
Zwraca listę wszystkich zwierząt.

#### ✅ `GET /animals/{id}`
Zwraca jedno zwierzę po ID.

#### ✅ `POST /animals`

Dodaje nowe zwierzę:

```json
{
  "name": "Luna",
  "category": "kot",
  "weight": 3.8,
  "furColor": "biały"
}
```

#### ✅ `PUT /animals/{id}`

Aktualizuje dane zwierzęcia:

```json
{
  "id": 2,
  "name": "Mruczek",
  "category": "kot",
  "weight": 4.5,
  "furColor": "czarny"
}
```

#### ✅ `DELETE /animals/{id}`
Usuwa zwierzę oraz jego wizyty.

---

### 🩺 Wizyty

#### ✅ `GET /animals/{id}/visits`
Zwraca listę wizyt danego zwierzęcia.

#### ✅ `POST /animals/{id}/visits`

Dodaje wizytę dla zwierzęcia:

```json
{
  "date": "2025-05-10T09:00:00",
  "description": "Szczepienie",
  "price": 120.0
}
```

---

## dane testowe

Dla testów aplikacja zawiera już kilka zwierząt i wizyt w pamięci.

---
