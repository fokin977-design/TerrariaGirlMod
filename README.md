# Terraria Girl Mod — спрайты и подключение к tModLoader

Пиксель-арт персонаж в стиле Terraria: девушка с высоким хвостом, синяя куртка.
Все кадры имеют игровой размер **40×56 px** (размер кадра игрока Terraria).

## Состав

| Файл | Описание |
|---|---|
| `b64/Girl_Spritesheet.png.b64` | Лист всех 14 кадров подряд (base64) |
| `GirlLayer.cs` | Готовый слой отрисовки для tModLoader |
| `decode.ps1` | Скрипт декодирования base64 → PNG |

Почему base64: GitHub API в этом клиенте принимает только текстовое содержимое.

## Быстрый старт

```powershell
git clone https://github.com/fokin977-design/TerrariaGirlMod.git
cd TerrariaGirlMod
powershell -File decode.ps1   # появится Girl_Spritesheet.png
```

## Раскадровка (индекс кадра → состояние)

```
0-2   idle  — стояние (цикл дыхания)
3-6   walk  — ходьба (4 кадра цикла)
7-10  run   — бег (4 кадра цикла)
11    jump  — прыжок
12    fall  — падение
13    sit   — сидение
```

Каждый кадр — ровно 40×56 px, кадр N начинается с x = N*40.

## Подключение к tModLoader

1. Создай мод: `dotnet new terrariamod -n GirlMod`.
2. Положи декодированный `Girl_Spritesheet.png` в папку `GirlMod/`.
3. Добавь `GirlLayer.cs` из этого репозитория в проект.
4. `dotnet build`, включи мод в меню Mods.

Логика кадров в `GirlLayer.cs`:
- игрок падает → кадр 12, прыгает → 11,
- стоит → цикл 0–2, идёт → цикл 3–6, бежит → цикл 7–10,
- сидит на объекте → кадр 13.

## Альтернатива: ванильный формат брони (vanity set)

Чтобы сделать из девушки **сет брони** (экипировка в слоты),
нужно порезать спрайт на три текстуры по шаблонам tModLoader:
голова, тело, ноги (свои сетки кадров).
Шаблоны: https://github.com/tModLoader/tModLoader/wiki/Basic-Armor —
возьми `Armor_Head_x.png`, `Armor_Body_x.png`, `Armor_Legs_x.png`
из игры как основу и перерисуй персонажа по ним.
