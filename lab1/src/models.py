from statistics import mean, median
from typing import NamedTuple


class Column(NamedTuple):
    key: str
    title: str
    index: int


class Statistics:
    minimum: float
    maximum: float
    mean: float
    median: float

    def __init__(self, values: list[float]):
        self.minimum = min(values)
        self.maximum = max(values)
        self.mean = mean(values)
        self.median = median(values)


HEADER_ROW_INDEX = 0
REGION_INDEX_IN_ROW = 1

CSV_EXTENSION = ".csv"
CSV_DELIMITER = ","

EXPECTED_HEADER = [
    "year",
    "region",
    "npg",
    "birth_rate",
    "death_rate",
    "gdw",
    "urbanization",
]

FILE_REQUIREMENTS = (
    f"Нужен {CSV_EXTENSION} файл в кодировке utf-8 "
    f"с разделителем '{CSV_DELIMITER}' и заголовком: "
    f"{CSV_DELIMITER.join(EXPECTED_HEADER)}"
)

COLUMNS = (
    Column("npg", "natural population growth", 2),
    Column("birth_rate", "birth rate", 3),
    Column("death_rate", "death rate", 4),
    Column("gdw", "gdw", 5),
    Column("urbanization", "urbanization", 6),
)
