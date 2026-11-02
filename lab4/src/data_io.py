import pandas as pd
import os

from src.errors import FileFormatError
from src.models import (
    CSV_DELIMITER,
    FILE_REQUIREMENTS,
    CSV_EXTENSION,
    MAX_FILE_SIZE_BYTES,
)

def repeat() -> bool:
    raw = ""
    flag = True

    while flag:
        raw = input("Сделать еще прогноз?[y/n]: ").strip().lower()
        if raw not in ("y", "n"):
            print("Выбери только между y и n")
            continue
        flag = False

    return raw == "y"

def show_predict(gpm: float):
    print(f"Прогноз золота в минуту: {gpm:.2f}")

def get_krip_stat() -> int:
    number = 0
    flag = True

    while flag:
        raw = input("Введи количество добитых крипов: ").strip()
        try:
            number = int(raw)
        except ValueError:
            print("Необходимо ввести целое число")
            continue
        if number < 0:
            print("Количество крипов не может быть отрицательным")
            continue
        flag = False

    return number

def read_file(path: str) -> pd.DataFrame:
    if not path.lower().endswith(CSV_EXTENSION):
        raise FileFormatError(FILE_REQUIREMENTS)

    size = os.path.getsize(path)
    if size > MAX_FILE_SIZE_BYTES:
        max_size_mb = MAX_FILE_SIZE_BYTES /1024/1024
        raise FileFormatError(f"Слишком большой размер файла. Максимум - {max_size_mb}Mb")

    data = pd.read_csv(path, usecols={"gold_per_min", "last_hits", "match_id"}, sep=CSV_DELIMITER)

    if data.empty:
        raise FileFormatError("Файл пустой")

    return data
