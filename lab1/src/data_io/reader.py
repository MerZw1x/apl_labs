import csv
import os

from src.errors import FileFormatError
from src.models import COLUMNS, EXPECTED_HEADER, HEADER_ROW_INDEX

MAX_FILE_SIZE_BYTES = 10*1024*1024

def read_path() -> str:
    while True:
        path = input(
            "Привет, чтобы узнать метрики, отправь сюда путь до файла:\n"
        ).strip()
        if not path:
            print("Путь не может быть пустым")
            continue
        return path


def read_file(path: str) -> list[list[str]]:
    size = os.path.getsize(path)
    if size > MAX_FILE_SIZE_BYTES:
        max_size_mb = MAX_FILE_SIZE_BYTES /1024/1024
        raise FileFormatError(f"Слишком большой размер файла. Максимум - {max_size_mb}Mb")

    with open(path, "r", encoding="utf-8", newline="") as f:
        data = list(csv.reader(f))

    if not data:
        raise FileFormatError("Файл пустой")

    header = data[HEADER_ROW_INDEX]
    if header != EXPECTED_HEADER:
        raise FileFormatError(
            f"Неожиданные колонки: {header} Ожидались - {EXPECTED_HEADER}"
        )

    for row in data[HEADER_ROW_INDEX + 1 :]:
        if len(row) != len(EXPECTED_HEADER):
            raise FileFormatError(f"Некорректная строка данных: {row}")

    return data


def get_region_name(regions: set[str]) -> str:
    while True:
        raw = input("Введи номер региона: ").strip()
        try:
            index = int(raw)
        except ValueError:
            print("Это не число, попробуй ещё раз")
            continue
        if not 1 <= index <= len(regions):
            print(f"Номер должен быть от 1 до {len(regions)}")
            continue
        return sorted(regions)[index - 1]


def get_stat_number() -> int:
    while True:
        raw = input("По какому критерию хочешь посчитать метрики: ").strip()
        try:
            number = int(raw)
        except ValueError:
            print("Это не число, попробуй ещё раз")
            continue
        if not 1 <= number <= len(COLUMNS):
            print(f"Номер метрики должен быть от 1 до {len(COLUMNS)}")
            continue
        return number


def repeat() -> bool:
    while True:
        raw = input("\nПродолжаем?[y/n]: ").strip().lower()
        if raw not in ("y", "n"):
            print("Выбери только между y и n")
            continue
        return raw == "y"
