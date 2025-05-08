# 🐾 KlinikaAPI 🐾 

REST API, która pozwala zarządzać danymi zwierząt i ich wizyt w klinice weterynaryjnej.

## 🔧 Technologia 🔧

- ASP.NET Core 9
- Entity Framework Core + SQLite
- Swagger UI

## RUN

1. Uruchom projekt
2. Przejdź w przeglądarce na: 👉 `http://localhost:5019/swagger`

## Endpointy i przykłady

---

### 🐶 Zwierzęta 🐶

#### `POST /api/Zwierze`

Dodaje nowe zwierzę **razem z wizytami**.

```json
{
  "imie": "Lok",
  "gatunek": "Pies",
  "wiek": 5,
  "wizyty": [
    {
      "data": "2025-05-08T14:00:00",
      "opis": "Kontrola"
    }
  ]
}
```

#### `GET /api/Zwierze`

Zwraca listę wszystkich zwierząt z wizytami.

#### `GET /api/Zwierze/{id}`

Zwraca jedno zwierzę po ID.

#### ✏`PUT /api/Zwierze/{id}`

Aktualizuje zwierzę (bez wizyt):

```json
{
  "id": 1,
  "imie": "NoweImie",
  "gatunek": "Kot",
  "wiek": 7,
  "wizyty": []
}
```

#### `DELETE /api/Zwierze/{id}`

Usuwa zwierzę i wizyty.

---

### 📝 Wizyty

#### ➕ `POST /api/Wizyta`

Dodaje wizytę do istniejącego.

```json
{
  "data": "2025-05-08T14:00:00",
  "opis": "Szczepienie",
  "zwierzeId": 1
}
```

#### `GET /api/Wizyta`

Zwraca listę wszystkich wizyt.

#### `GET /api/Wizyta/{id}`

Zwraca konkretną wizytę.

#### `PUT /api/Wizyta/{id}`

Aktualizuje wizytę:

```json
{
  "id": 2,
  "data": "2025-05-10T10:00:00",
  "opis": "Wizyta kontrolna",
  "zwierzeId": 1
}
```

#### ❌ `DELETE /api/Wizyta/{id}`

Usuwa wizytę.

---

